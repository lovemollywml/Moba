using UnityEngine;
using System.Collections;
using System;

public abstract class AbstractPoolBase : IPool {
    private bool isInit = false;
    private PoolList poolList=null;
    protected abstract object CreateObj();
    protected virtual void DestroyObj(object obj)
    {
       // obj.ClearActiveData();
    }
    protected virtual void OnRemoveCompleted(object obj) { }
    public AbstractPoolBase(int initSize, int maxSize)
    {
        ActivatedCount = 0;
        NotActiveCount = 0;
        poolList = new PoolList(initSize, maxSize);
    }
    protected void InitPool()
    {
        if (isInit) return;
        for (int i = 0; i < poolList.InitSize; i++)
        {
            object obj = CreateObj();
            if (obj == null)
                throw new Exception("对象创建失败");
            obj.Sleep();
            poolList.AddToLast(obj);
            NotActiveCount++;
        }
        isInit = true;
    }
    protected object GetObj()
    {
        object obj = poolList.Get();
        if (obj == null || obj.IsActive())
        {
            obj = CreateObj();
        }
        else
        {
            NotActiveCount--;
        }
        if (poolList.AddToLast(obj))
        {
            obj.WakeUp();
        }
        else
        {
            throw new Exception("Pool Fulled !");
        }
        ActivatedCount++;
        return obj;
    }
    protected void Remove(object obj)
    {
        obj.Sleep();
        if (poolList.AddToFrist(obj))
        {
            OnRemoveCompleted(obj);
        }
        else
        {
            throw new Exception("Pool Fulled！");
        }
        ActivatedCount--;
        NotActiveCount++;
    }
    public void ClearAll()
    {
        int count = poolList.Count;
        object tempObj;
        for (int i = 0; i < count; i++)
        {
            tempObj = poolList.GetObjectBy(0);
            poolList.Remove(tempObj);
            DestroyObj(tempObj);
        }
        poolList.Clear();
        ActivatedCount = 0;
        NotActiveCount = 0;
    }

    public void ClearKeepActive()
    {
        object obj;
        while (true)
        {
            obj = poolList.GetObjectBy(0);
            if (obj == null || obj.IsActive())
            {
                NotActiveCount = 0;
                return;
            }
            poolList.Remove(obj);
            DestroyObj(obj);
        }
    }


    public void ReSize(int initSize, int maxSize)
    {
        poolList.Resize(initSize, maxSize);
        object obj;
        while (poolList.Count > poolList.MaxSize)
        {
            obj = poolList.GetObjectBy(0);
            if (obj.IsActive())
            {
                return;
            }
            poolList.Remove(obj);
            DestroyObj(obj);
            NotActiveCount--;
        }
    }

    public int ActivatedCount
    {
        get;
        private set;
    }

    public int NotActiveCount
    {
        get;
        private set;
    }
}

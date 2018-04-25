using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PoolList  {

    private int initSize;
    private int maxSize;
    private List<object> poolList;
    public PoolList(int initSize, int maxSize)
    {
        this.initSize = initSize;
        this.maxSize = maxSize;
        poolList = new List<object>();
    }
    public void Resize(int initSize,int maxSize) 
    {
        this.initSize = initSize;
        this.maxSize = maxSize;
    }
    public bool AddToFrist(object obj)
    { 
         if(poolList.Contains(obj)||poolList.Count<maxSize)
         {
             MoveToFrist(obj);
             return true;
         }
         return false;
    }
    public bool AddToLast(object obj)
    {
        if (poolList.Contains(obj) || poolList.Count < maxSize)
        {
            MoveToLast(obj);
            return true;
        }
        return false;
    }
    public void Clear()
    {
        poolList.Clear();
    }
    public void Remove(object obj)
    {
        poolList.Remove(obj);
    }
    public int Count
    {
        get { return poolList.Count; }
    }
    public object GetObjectBy(int index)
    {
        if (poolList.Count > index)
            return poolList[index];
        return null;
    }
    public object Get()
    {
        if (poolList.Count > 0)
        {
            return poolList[0];
        }
        return null;
    }
    public int InitSize
    {
        get { return initSize; }
    }
    public int MaxSize
    {
        get { return maxSize; }
    }
    public object this[int index] {
        get {
            return poolList[index];
        }
    }
    public object MoveToFrist(object obj)
    {
        poolList.Remove(obj);
        poolList.Insert(0, obj);
        return obj;
    }
    public object MoveToLast(object obj)
    {
        poolList.Remove(obj);
        poolList.Add(obj);
        return obj;
    }
}

using UnityEngine;
using System.Collections;

public class ObjectPool<T>:AbstractPoolBase,IObjectPool where T:class {

    private object[] createArgs;
    public ObjectPool(int initSize = 0, int maxSize = 10, params object[] args):
        base(initSize,maxSize)
    {
        this.createArgs = args;
        InitPool();
    }
    protected override object CreateObj()
    {
        if (createArgs != null && createArgs.Length > 0)
        {
            return System.Activator.CreateInstance(typeof(T),createArgs);
        }
        return System.Activator.CreateInstance<T>();
    }
    public T Get<T>() where T : class
    {
        return base.GetObj() as T;
    }
    public void Remove<T>(T obj)
    {
        base.Remove(obj);
    }
}

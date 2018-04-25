using UnityEngine;
using System.Collections;

public interface IObjectPoolManager  {

    IObjectPool CreateObjectPool<T>(int initSize , int maxSize ) where T : class;
    IObjectPool CreateObjectPool<T>(int initSize , int maxSize , params object[] args) where T : class;
    IObjectPool GetObjectPool<T>() where T : class;
    IObjectPool GetObjectPool<T>(params object[] args) where T : class;
}

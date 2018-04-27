using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public static class ObjectEx  {

    private static Dictionary<int, int> activeDic = new Dictionary<int, int>();
    public static void WakeUp(this object obj)
    {
        int key = obj.GetHashCode();
        if (!activeDic.ContainsKey(key))
        {
            activeDic.Add(key, 0);
        }
        else
        {
            activeDic[key] = 0;
        }
    }
    public static void Sleep(this object obj)
    {
        int key = obj.GetHashCode();
        if (!activeDic.ContainsKey(key))
        {
            activeDic.Add(key, DateTime.Now.ToTimeStamp());
        }
        else
        {
            activeDic[key] = DateTime.Now.ToTimeStamp();
        }
    }
    public static bool IsActive(this object obj)
    {
        int key = obj.GetHashCode();
        if (activeDic.ContainsKey(key))
        {
            return activeDic[key] == 0;
        }
        return false;
    }
    public static int GetDeathTime(this object obj)
    {
        int key = obj.GetHashCode();
        if (activeDic.ContainsKey(key))
        {
            return activeDic[key];
        }
        return 0;
    }
    public static bool ClearActiveData(this object obj)
    {
          return activeDic.Remove(obj.GetHashCode());
    }

}

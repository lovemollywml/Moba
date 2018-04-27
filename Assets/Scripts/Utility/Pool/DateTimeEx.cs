using UnityEngine;
using System.Collections;
using System;

public static class DateTimeEx {

    private static DateTime _1970 = new DateTime(1970, 1, 1, 0, 0, 0, 0);
    public static int ToTimeStamp(this DateTime dt)
    {
        TimeSpan ts = DateTime.UtcNow - _1970;
        return Convert.ToInt32(ts.TotalSeconds);
    }
}

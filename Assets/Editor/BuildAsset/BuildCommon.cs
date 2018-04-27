using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using System.IO;
using System;

public class BuildCommon  {


    public static int getAssetLevel(string filePath)
    {
        string[] depencys = AssetDatabase.GetDependencies(new string[] { filePath });
        List<string> deps = new List<string>();
        foreach (string file in depencys)
        {
            //排除关联脚本
            string suffix = Path.GetExtension(file);
            if (suffix.Equals("dll"))
            {
                continue;
            }
            deps.Add(file);
        }

        if (deps.Count == 1)
            return 1;
        int maxLevel = 0;
        foreach (string file in deps)
        {
            if (file == filePath)
                continue;
            int level = getAssetLevel(file);
            maxLevel = maxLevel > level + 1 ? maxLevel : level + 1;
        }
        return maxLevel;
    }
    public static void CheckFolder(string path)
    {
        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);
    }
    public static string getPath(string filePath)
    {
        string path = filePath.Replace("\\", "/");
        int index = path.LastIndexOf("/");
        if (-1 == index)
            throw new Exception("can not find");
        return path.Substring(0, index);
    }
}

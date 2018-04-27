using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;

public class ResourceCommon : MonoBehaviour {

    public static string textFilePath = Application.streamingAssetsPath;
    public static string assetbundleFilePath = Application.dataPath + "/assetbundles/";
    public static string assetbundleFileSuffix = ".bytes";
    public static string DEBUGTYPENAME = "Resource";

    //public static string mServerAssetPath="http://192.168.0.1/assetbundles/;
    public static string mServerAssetPath = "file:///c:/";

    public static void Log(string fileName)
    {
#if ORGDEBUG
        Debug.Log(fileName, DEBUGTYPENAME, true);
#endif
    }
    public static string getResourceName(string resPathName)
    {
        var name = Path.GetFileName(resPathName);
        return name;
        //int index = resPathName.LastIndexOf("/");
        //if (index == -1)
        //{
        //    return resPathName;
        //}
        //else
        //{
        //    return resPathName.Substring(index + 1, resPathName.Length - index - 1);
        //}
    }
  
}

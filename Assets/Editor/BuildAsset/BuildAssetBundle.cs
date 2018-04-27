using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using System.IO;
using System.Xml;

public class BuildAssetBundle  {
    //AssetBundle 资源
    private static string assetFilePath = "/assetbundles";
    private static string buildCfgFilePath = Application.dataPath + "/Editor/Build/build.txt";
    private static string levelCgfFilePath = Application.dataPath + "/Editor/Build/level.txt";
    private static string buildResourceCfg = Application.dataPath + "/Editor/Build/Resource.txt";

    //打包环境设置
    private static BuildAssetBundleOptions option = BuildAssetBundleOptions.DeterministicAssetBundle;
    private static BuildTarget buildPlatform = BuildTarget.StandaloneWindows;

    //保存所有的scene信息
    private static List<string> mScenes = new List<string>();

    //保存所有Resource信息
    private static List<string> mResources = new List<string>();

    //保存所有的Asset信息，场景+Resource
    private static Dictionary<int, Dictionary<string, AssetUnit>> allLevelAssets = new Dictionary<int, Dictionary<string, AssetUnit>>();

    [MenuItem("Build/BuildAndroid")]
    public static void BuildAndroid()
    {
        buildPlatform = BuildTarget.Android;
       //TODO
        BuildResourceFromUnityRule();
    }
    [MenuItem("Build/BuildIOS")]
    public static void BuildIOS()
    {
        buildPlatform = BuildTarget.iOS;
        //TODO
        BuildResourceFromUnityRule();
    }
    [MenuItem("Build/BuildWindows")]
    public static void BuildWindows()
    {
        buildPlatform = BuildTarget.StandaloneWindows;
        //TODO
        BuildResourceFromUnityRule();
    }
    public static void BuildResourceFromUnityRule()
    { 
       

    }
}

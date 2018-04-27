using UnityEngine;
using System.Collections;

public class GameObjectPool : AbstractPoolBase,IGameObjectPool {
    private Transform goParent;
    private GameObject prefab;
    public GameObjectPool(GameObject prefab, Transform parent, int initSize = 0, int maxSize = 10)
        : base(initSize, maxSize)
    {
        this.goParent = parent;
        this.prefab = prefab;

        InitPool();
    }
    protected override object CreateObj()
    {
        GameObject go = Object.Instantiate(prefab, Vector3.zero, Quaternion.identity) as GameObject;
        go.transform.SetParent(goParent);
        go.SetActive(false);
        return go;
    }
    protected override void DestroyObj(object obj)
    {
        base.DestroyObj(obj);
        Object.Destroy(obj as GameObject);
    }
    protected override void OnRemoveCompleted(object obj)
    {
        base.OnRemoveCompleted(obj);
        GameObject go = obj as GameObject;
        go.SetActive(false);
        if (go.transform.parent != goParent)
        {
            go.transform.SetParent(goParent);
        }
        go.transform.localPosition = Vector3.zero;
    }
    public GameObject Get()
    {
        return GetObj() as GameObject;
    }

    public void Remove(GameObject obj)
    {
        base.Remove(obj);
    }
}

using UnityEngine;
using System.Collections;

public interface IGameObjectPool : IPool {

    GameObject Get();
    void Remove(GameObject obj);
}

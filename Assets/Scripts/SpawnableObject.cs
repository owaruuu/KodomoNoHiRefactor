using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpawnableObject : MonoBehaviour
{
    protected ObjectPool myPool;

    /// <summary>
    /// Set the object pool this object has to return to
    /// </summary>
    /// <param name="objectPool"></param>
    public void SetObjectPool(ObjectPool objectPool)
    {
        myPool = objectPool;
    }
}
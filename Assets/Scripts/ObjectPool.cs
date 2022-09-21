using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    //[HideInInspector]
    public Stack<GameObject> unusedObjectStack;
    //[HideInInspector]
    public List<GameObject> inUseObjectList;

    [Header("Setup")]
    public bool StartsPooled = true;
    public GameObject ObjectToPool;
    [SerializeField] int initialAmount;
    [SerializeField] int defaultCapacity;

    private int maxPoolSize;

    private void Awake()
    {
        unusedObjectStack = new Stack<GameObject>(defaultCapacity);

        if (StartsPooled)
        {
            for (int i = 0; i < initialAmount; i++)
            {
                GameObject obj = (GameObject)Instantiate(ObjectToPool);
                obj.SetActive(false);
                unusedObjectStack.Push(obj);
            }      
        }
        
        inUseObjectList = new List<GameObject>(defaultCapacity);
    }

    /// <summary>
    /// checks if object in pool is avaible and sets it active, it also calls SetObjectPool()
    /// </summary>
    /// <returns></returns>
    public GameObject GetObject()
    {
        if(unusedObjectStack.Count > 0)
        {
            var obj = unusedObjectStack.Pop();
            inUseObjectList.Add(obj);

            obj.SetActive(true);
            obj.GetComponent<SpawnableObject>().SetObjectPool(this);
            //obj.GetComponent<SpawnableObject>().Activate(this);

            return obj;
        }else
        {
            Debug.LogWarning("no tenia objetos en el pool y cree uno nuevo");
            var obj = (GameObject)Instantiate(ObjectToPool);
            inUseObjectList.Add(obj);
            obj.SetActive(true);
            obj.GetComponent<SpawnableObject>().SetObjectPool(this);
            //obj.GetComponent<SpawnableObject>().Activate(this);

            return obj;
        }
    }

    public void ReturnToPool(GameObject obj)
    {
        if(unusedObjectStack.Count >= defaultCapacity)
        {
            Debug.LogWarning("unused stack was full, needs more capacity");
            inUseObjectList.Remove(obj);
            obj.SetActive(false);
            Destroy(obj);
        }
        else
        {
            obj.SetActive(false);          
            obj.GetComponent<IResetable>()?.Reset();
            inUseObjectList.Remove(obj);
            unusedObjectStack.Push(obj);
        } 
    }
}

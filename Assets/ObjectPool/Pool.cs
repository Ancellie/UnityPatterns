using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class PoolItem{
    public GameObject prefab;
    public int amount;
    public bool expandable;
}

public class Pool : MonoBehaviour {
    public static Pool singleton;
    public List<PoolItem> items;
    
    private Dictionary<string, Queue<GameObject>> inactiveObjectsByTag;
    private Dictionary<string, PoolItem> itemsByTag;
    
    void Awake()
    {
        singleton = this;
    }
    
    public GameObject Get(string tag)
    {
        // Шукаємо неактивний об'єкт у черзі
        if (inactiveObjectsByTag.ContainsKey(tag) && inactiveObjectsByTag[tag].Count > 0)
        {
            return inactiveObjectsByTag[tag].Dequeue();
        }
        
        // Якщо немає доступних об'єктів, створюємо новий (якщо можливо)
        if (itemsByTag.ContainsKey(tag) && itemsByTag[tag].expandable)
        {
            GameObject obj = Instantiate(itemsByTag[tag].prefab);
            obj.SetActive(false);
            return obj;
        }
        
        return null;
    }
    
    public void ReturnToPool(GameObject obj)
    {
        string tag = obj.tag;
        obj.SetActive(false);
        
        if (!inactiveObjectsByTag.ContainsKey(tag))
        {
            inactiveObjectsByTag[tag] = new Queue<GameObject>();
        }
        
        inactiveObjectsByTag[tag].Enqueue(obj);
    }
    
    void Start()
    {
        inactiveObjectsByTag = new Dictionary<string, Queue<GameObject>>();
        itemsByTag = new Dictionary<string, PoolItem>();
        
        items.ForEach(item => itemsByTag[item.prefab.tag] = item);
        
        items.ForEach(item => {
            var objectsForTag = Enumerable.Range(0, item.amount)
                .Select(_ => {
                    GameObject obj = Instantiate(item.prefab);
                    obj.SetActive(false);
                    return obj;
                })
                .ToArray();
            
            inactiveObjectsByTag[item.prefab.tag] = new Queue<GameObject>(objectsForTag);
        });
    }
}

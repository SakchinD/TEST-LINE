using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : Singleton<ObjectPool>
{
    [Serializable]
    public struct KeyValue
    {
        public string ObjectName;
        public GameObject Object;
    }
    public List<KeyValue> objects;
    Dictionary<string, List<GameObject>> ammoDict = new Dictionary<string, List<GameObject>>();

    void CreateBullet(string ObjectName)
    {
        if (!ammoDict.ContainsKey(ObjectName))
        {
            ammoDict.Add(ObjectName, new List<GameObject>());
        }
        var obj = objects.Find(item => item.ObjectName == ObjectName);
        var item = Instantiate(obj.Object);
        item.transform.SetParent(transform);
        item.gameObject.SetActive(false);
        ammoDict[ObjectName].Add(item);
    }
    public GameObject GetPooledObject(string ObjectName)
    {
        if (ammoDict.ContainsKey(ObjectName))
        {
            for (int i = 0; i < ammoDict[ObjectName].Count; i++)
            {
                if (!ammoDict[ObjectName][i].gameObject.activeInHierarchy)
                {
                    return ammoDict[ObjectName][i];
                }
            }
        }
        CreateBullet(ObjectName);
        return ammoDict[ObjectName][ammoDict[ObjectName].Count - 1];
    }
}

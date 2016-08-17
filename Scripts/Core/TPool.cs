using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class TPool 
{
    private List<IPooledObject> m_poolObjects;
    private string m_nameResource;
    private GameObject m_resource; 

    public void CreatePool(string name, int init_size)
    {
        m_nameResource = name;
        m_resource = GetResource();
        m_poolObjects = new List<IPooledObject>();
        if (m_resource == null)
        {
            Debug.Log("Failed load resource: " + m_nameResource);
            return;
        }

        for (int i = 0; i < init_size; i++)
        {
            var new_po = CreatePooledObject();
            if (new_po != null)
            { 
                m_poolObjects.Add(new_po);
            }
        }

    }

    public GameObject GetObject()
    {

        for (int i = 0; i < m_poolObjects.Count; i++)
        {
            if (!m_poolObjects[i].gameObject.activeSelf)
            {
                var res = m_poolObjects[i];
                res.Create();
                return res.gameObject;
            }
        }
        var new_po = CreatePooledObject();
        m_poolObjects.Add(new_po);
        new_po.Create();
        return new_po.gameObject;
    }
    public void ClearPool()
    {
        foreach (var obj in m_poolObjects)
            obj.Release();
    }
    private GameObject GetResource()
    {
        if (m_resource == null)
        {
            m_resource = Resources.Load(m_nameResource) as GameObject;
        }
        return m_resource;
    }

    private IPooledObject CreatePooledObject()
    {
        GameObject obj = GameObject.Instantiate(m_resource) as GameObject;
        obj.SetActive(false);        
        return obj.GetComponent<IPooledObject>();
    }
}

using UnityEngine;

public class CacheObject : MonoBehaviour
{

    protected GameObject m_gameObject;
    protected Transform m_transform;

    protected void Cache()
    {
        if (m_gameObject == null) m_gameObject = gameObject;
        if (m_transform==null) m_transform = transform;
    }

    public void SetPosition(Vector3 position)
    {
        m_transform.position = position;        
    }
    public void AddPosition(Vector3 position)
    {
        m_transform.position += position;
    }
    public Vector3 GetPosition()
    {
        return m_transform.position;
    }
}

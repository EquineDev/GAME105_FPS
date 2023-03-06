using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    [SerializeField]
    private Points[] m_pathPoints;
    // Start is called before the first frame update
    public Transform GetWayPoint(int number)
    {
        return m_pathPoints[number].transform;
    }
    public int GetWayPointCount()
    {
        return m_pathPoints.Length - 1;
    }

#if UNITY_EDITOR

    public void OnDrawGizmosSelected()
    {
        if (m_pathPoints == null || m_pathPoints.Length < 2)
        {
            return;
        }
        if (m_pathPoints[0] != null)
        {

            Gizmos.DrawLine(this.transform.position, m_pathPoints[0].transform.position);

        }
        for (int i = 1; i < m_pathPoints.Length; i++)
        {
            if (m_pathPoints[i - 1] != null && m_pathPoints[i] != null)
            {
                Gizmos.DrawLine(m_pathPoints[i - 1].transform.position,m_pathPoints[i].transform.position);
            }
                
        }
    }
#endif
}

using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Points : MonoBehaviour
{
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        GUIStyle style = new GUIStyle();
        style.normal.textColor = Color.white;
        Gizmos.color = Color.red;
        Handles.Label(transform.position, transform.gameObject.name, style);
    }
#endif
}

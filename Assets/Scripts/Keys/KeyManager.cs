using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class KeyManager : MonoBehaviour
{
    private List<ColorKeys> m_keys = new List<ColorKeys>();
    public void AddKeyItem(ColorKeys KeyItem)
    {
        if(m_keys.Contains(KeyItem))
        {
            return;
        }
        else
        {
            m_keys.Add(KeyItem);
        }
    }

    public bool HoldsKey(ColorKeys colorKeys)
    {
        return m_keys.Contains(colorKeys);
    }
}


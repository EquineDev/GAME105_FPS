using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorLock : MonoBehaviour
{
    [SerializeField]
    private ColorKeys m_lock;

    [SerializeField] private Animator m_animator;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if (other.GetComponent<KeyManager>().HoldsKey(m_lock))
            {
                m_animator.SetBool("Islock", false);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            m_animator.SetBool("Islock", true);
        }
    }
}

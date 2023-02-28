using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyItemPickUp : MonoBehaviour
{
    [SerializeField]
    private ColorKeys m_keyItem; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<KeyManager>().AddKeyItem(m_keyItem);
            Destroy(this.gameObject);
        }
    }
}
public enum ColorKeys
{
    Red,
    Green,
}
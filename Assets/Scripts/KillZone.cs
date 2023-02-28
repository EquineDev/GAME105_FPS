using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZone : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("player has hit killzone");
          //  other.gameObject.GetComponent<PlayerActions>().ResetPlayer();
           // GameManager.Instance.UpdateLives(-1);
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float m_timerMax =10f;
    [SerializeField] private TextUI m_timerDisplay;
    [SerializeField] private TextUI m_status;
    private PlayerMovement m_player;
    private float m_timer;
    private Coroutine m_alertCounter;
    public static GameManager Instance { get; private set; }
    

    public Action<AIState> GuardStatusUpdate;

    private void Awake()
    {
       if(Instance != null && Instance != this)
    {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    private void Start()
    {
        m_status.UpdateUI("Safe ");
        m_player = FindObjectOfType<PlayerMovement>();
    }

    public void Caught()
    {

        m_status.UpdateUI("You been caught");
        GuardStatusUpdate.Invoke(AIState.Patrol);
        Invoke("Safe", 1f);
        m_player.GoTojail();
    }

    public void FoundInvader()
    {
        if (m_alertCounter != null)
        {
            StopCoroutine(m_alertCounter);
            m_alertCounter = null;
        }
        m_status.UpdateUI("You been Seen!");
        GuardStatusUpdate.Invoke(AIState.Chase);
    }
    public void Caution()
    {
        
        Debug.Log("Caution  State");
        m_status.UpdateUI("Caution! ");
        GuardStatusUpdate.Invoke(AIState.Investigate);
        m_alertCounter = StartCoroutine(CountDown());
    }

    public void Safe()
    {
        if (m_alertCounter != null)
        {
            StopCoroutine(m_alertCounter);
        }
     
        m_alertCounter = null;
        m_status.UpdateUI("Safe ");
        Debug.Log("SAFE Sate");
        GuardStatusUpdate.Invoke(AIState.Patrol);
    }

    IEnumerator CountDown()
    {
        m_timer = m_timerMax;
        while (m_timer > 0)
        {
            m_timer--;

            yield return new WaitForSecondsRealtime(1f);
            m_timerDisplay.UpdateUI(m_timer);
        }
        m_timer = 0;
        m_timerDisplay.UpdateUI("");
        Safe();
    }

}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float m_timerMax =10f;
    [SerializeField] private TextUI m_timerDisplay;
    [SerializeField] private TextUI m_status;
    private float m_timer;
    private Coroutine m_alertCounter;
    private static GameManager m_instance;
    public static GameManager Instance { get { return m_instance; } }

    public Action<AIState> GuardStatusUpdate;

    private void Awake()
    {
        if (Instance == null)
        {
            m_instance = this;
        
        }
        else
        {
            Destroy(this);

        }
    }
    private void Start()
    {
        m_status.UpdateUI("Safe ");
    }

    public void Caught()
    {

        m_status.UpdateUI("You been caught");
        GuardStatusUpdate.Invoke(AIState.Patrol);
        Invoke("Safe", 1f);
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
        if (m_alertCounter != null)
            return;
        m_status.UpdateUI("Caution! ");
        GuardStatusUpdate.Invoke(AIState.Investigate);
        m_alertCounter = StartCoroutine(CountDown());
    }

    public void Safe()
    {
        StopCoroutine(m_alertCounter);
        m_alertCounter = null;
        m_status.UpdateUI("Safe ");

        GuardStatusUpdate.Invoke(AIState.Patrol);
    }

    IEnumerator CountDown()
    {
        m_timer = m_timerMax;
        while (m_timer > 0)
        {
            m_timer--;
            yield return new WaitForSeconds(1f);
            m_timerDisplay.UpdateUI(m_timer);
        }
        m_timer = 0;
        m_timerDisplay.UpdateUI("");
        Safe();
    }

}

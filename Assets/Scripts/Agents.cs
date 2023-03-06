using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Agents : MonoBehaviour
{
    [SerializeField] private Path m_roamingPath;
    [SerializeField] private float m_chaseDistance = 10f;
    [SerializeField] private float m_attackDistance = 2f;
    [SerializeField] private float m_investigateDistance = 5f;
    [SerializeField] private float m_patrolSpeed = 1.5f;
    [SerializeField] private float m_chaseSpeed = 3f;
    [SerializeField] private float m_timeToLoseInterest = 5f;
    [SerializeField] private float m_timeToInvestigate = 3f;
    [SerializeField] private float m_coneOfVisionAngle = 60f;
    private NavMeshAgent m_navAgent;
   

    private float m_timeSinceLastSawPlayer = Mathf.Infinity;
    private float m_timeSinceLastInvestigated = Mathf.Infinity;
    private int m_currentWaypoint = 0;
    private int m_lastWaypoint = -1;
    private int m_investigationAttempts = 0;
    private Transform m_player;
    private AIState m_agentState = AIState.Patrol;
    // Start is called before the first frame update
    private void Awake()
    {
        m_player =  GameObject.FindGameObjectWithTag("Player").transform;
        m_navAgent = GetComponent<NavMeshAgent>();
        m_currentWaypoint = m_lastWaypoint;
    }
    private void OnEnable()
    {
        GameManager.Instance.GuardStatusUpdate += AlertStatusUpdate;
    }

    private void OnDisable()
    {
        GameManager.Instance.GuardStatusUpdate -= AlertStatusUpdate;
    }

    // Update is called once per frame
    void Update()
    {
        switch (m_agentState)
        {
            case AIState.Patrol:
                PatrolRoam();
                break;

            case AIState.Investigate:
                Investigate();
                break;

            case AIState.Chase:
                Chase();
                break;

            case AIState.Attack:
                Attack();
                break;
        }
    }

    private  void PatrolRoam()
    {
        if (m_navAgent.remainingDistance < 0.5f)
        {
            SetNextWaypoint();
        }
        if (CanSeePlayer())
        {
            m_timeSinceLastSawPlayer = 0f;
            m_agentState = AIState.Chase;
        }
      
    }

    private void Investigate()
    {
        if (m_navAgent.remainingDistance < 0.5f)
        {
            if (m_investigationAttempts >= m_roamingPath.GetWayPointCount())
            {
                m_investigationAttempts = 0;
                m_navAgent.speed = m_patrolSpeed;
                m_agentState = AIState.Patrol;
                GameManager.Instance.Safe();
            }
            else
            {
                SetNextWaypoint();
                m_timeSinceLastInvestigated = 0f;
                m_investigationAttempts++;
            }
        }

        if (CanSeePlayer())
        {
            m_timeSinceLastSawPlayer = 0f;
            m_agentState = AIState.Chase;
        }

        m_timeSinceLastInvestigated += Time.deltaTime;
        if (m_timeSinceLastInvestigated > m_timeToInvestigate)
        {
            m_investigationAttempts = 0;
            m_navAgent.speed = m_patrolSpeed;
            m_agentState = AIState.Patrol;
            GameManager.Instance.Safe();
        }
    }

    private void Chase()
    {
        m_navAgent.speed = m_chaseSpeed;
        m_navAgent.SetDestination(m_player.position);

        if (Vector3.Distance(transform.position, m_player.position) > m_chaseDistance)
        {
           m_timeSinceLastSawPlayer += Time.deltaTime;
            if (m_timeSinceLastSawPlayer > m_timeToLoseInterest)
            {
                m_agentState = AIState.Investigate;
                GameManager.Instance.Caution();
            }
        }
        else if (Vector3.Distance(transform.position, m_player.position) < m_attackDistance)
        {
            m_agentState = AIState.Attack;
        }
        else
        {
            m_timeSinceLastSawPlayer = 0f;
        }
    }

    private void Attack()
    {
        //ToDO put player Back to jail
        GameManager.Instance.Caught();
            
    }
    private void SetNextWaypoint()
    {
        m_currentWaypoint = (m_currentWaypoint + 1) % m_roamingPath.GetWayPointCount();
        m_navAgent.SetDestination(m_roamingPath.GetWayPoint(m_currentWaypoint).position);
    }
            
    private bool CanSeePlayer()
    {
        Vector3 directionToPlayer = m_player.position - transform.position;
        float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer);
  
        Debug.DrawRay(transform.position, directionToPlayer * 10f, Color.red);
      
        if (angleToPlayer <= m_coneOfVisionAngle)       
        {
            
                RaycastHit hit;
          
            if (Physics.Raycast(transform.position, directionToPlayer, out hit, m_chaseDistance))
                {
                    if (hit.transform == m_player)
                    {
                    Debug.Log("ALERT: WE SEE ARE OUR HERO");
                    GameManager.Instance.FoundInvader();
                    return true;
                    }
                }
            }

        return false;
    }

    private void AlertStatusUpdate(AIState state)
    {
        m_agentState = state;
        switch (m_agentState)
        {
            case AIState.Patrol:
                m_navAgent.speed = m_patrolSpeed;
                m_timeSinceLastSawPlayer = 0;
                m_investigationAttempts = 0;
                m_investigationAttempts = 0;
                break;

            case AIState.Investigate:
                m_navAgent.speed = m_chaseSpeed;
                break;

            case AIState.Chase:
                m_navAgent.speed = m_chaseSpeed;
                break;

        }
    }
}

public enum AIState { Patrol, Investigate, Chase, Attack };

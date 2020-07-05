using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Complete;
using System;

public class StateController : MonoBehaviour
{

    public State currentState;
    public Transform eyes;
    public State remainState;
    public TankStats enemyStats;
    public Material tankMaterial;
    public List<Transform> waypointList = new List<Transform>();

    [HideInInspector] public NavMeshAgent navMeshAgent;
    [HideInInspector] public EnemyTankShooting enemyTankShooting;
    [HideInInspector] public List<Transform> wayPointList;
    [HideInInspector] public int nextWayPoint;
    [HideInInspector] public Transform chaseTarget;
    [HideInInspector] public float stateTimeElapsed;

    private bool aiActive;


    void Awake()
    {
        enemyTankShooting = GetComponent<EnemyTankShooting>();
        navMeshAgent = GetComponent<NavMeshAgent>();

        SetupAI(true, waypointList);
        InitializeTank();
    }

    private void InitializeTank()
    {
        enemyTankShooting.m_MaxShellBounces = enemyStats.m_MaxShellBounces;
        enemyTankShooting.m_ProjectileSpeed = enemyStats.m_ProjectileSpeed;

        tankMaterial.color = enemyStats.m_TankColor;
    }

    private void SetupAI(bool aiActivationFromTankManager, List<Transform> wayPointsFromTankManager)
    {
        navMeshAgent.speed = enemyStats.m_MovementSpeed;

        wayPointList = wayPointsFromTankManager;
        aiActive = aiActivationFromTankManager;
        if (aiActive)
        {
            navMeshAgent.enabled = true;
        }
        else
        {
            navMeshAgent.enabled = false;
        }
    }

    void Update()
    {
        if (!aiActive)
            return;
        currentState.UpdateState(this);
    }

    public void TransitionToState(State nextState)
    {
        if (nextState != remainState)
        {
            currentState = nextState;
            OnExitState();
        }
    }

    public bool CheckIfCountDownElapsed(float duration)
    {
        if (stateTimeElapsed >= duration)
        {
            stateTimeElapsed = 0;
            return true;
        }
        else
        {
            stateTimeElapsed += Time.deltaTime;
            return false;
        }
    }

    private void OnExitState()
    {
        stateTimeElapsed = 0;
    }
}
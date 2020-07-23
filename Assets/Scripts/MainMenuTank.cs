using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MainMenuTank : MonoBehaviour
{
    public NavMeshAgent agent;

    public List<Transform> waypointList;

    private void Update()
    {
        if (agent.remainingDistance < 1)
        {
            agent.SetDestination(GetRandomWaypoint());
        }
    }

    private Vector3 GetRandomWaypoint()
    {
        return waypointList[UnityEngine.Random.Range(0, waypointList.Count)].transform.position;
    }
}

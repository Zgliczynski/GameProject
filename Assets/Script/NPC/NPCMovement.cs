using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCMovement : MonoBehaviour
{
    private NPCSpawner npcSpawner;
    //[SerializeField] private Transform targetPosition;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] private Vector3 target;


    private void Awake()
    {
        npcSpawner = GetComponent<NPCSpawner>();
    }

    private void Update()
    {
        agent.SetDestination(target);
    }
}

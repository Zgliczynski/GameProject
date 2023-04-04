using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCMovement : MonoBehaviour
{
    private NPCSpawner npcSpawner;
    [SerializeField] private Transform targetPosition;
    [SerializeField] NavMeshAgent agent;
    public bool isWalk;
    public bool isDancing;
    //[SerializeField] private Vector3 target;

    public float velocity;

    private void Awake()
    {
        npcSpawner = GetComponent<NPCSpawner>();
    }

    private void Update()
    {
        agent.destination = targetPosition.position;
        velocity = agent.speed;

        if(velocity > 0)
        {
            isWalk = true;
        }
        else if(velocity == 0)
        {
            isDancing = true;
            isWalk = false;
        }
    }
}

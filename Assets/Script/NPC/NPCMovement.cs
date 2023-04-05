using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCMovement : MonoBehaviour
{
    private NPCSpawner npcSpawner;
    [SerializeField] private Transform targetPosition;
    [SerializeField] private GameObject plane;
    [SerializeField] NavMeshAgent agent;
    public bool isWalk;
    public bool isDancing;
    

    public float velocity;

    private void Awake()
    {
        npcSpawner = GetComponent<NPCSpawner>();
        RandomTargetOnPlane();
        isDancing = false;

    }

    private void Update()
    {
        velocity = agent.speed;

        if(agent.remainingDistance == agent.stoppingDistance)
        {
            isDancing = true;
            isWalk = false;
        }
        else
        {
            isDancing = false;
            isWalk = true;
        }
    }

    private void RandomTargetOnPlane()
    {
        List<Vector3> VerticeList = new List<Vector3>(plane.GetComponent<MeshFilter>().sharedMesh.vertices);
        Vector3 leftTop = plane.transform.TransformPoint(VerticeList[0]);
        Vector3 rightTop = plane.transform.TransformPoint(VerticeList[10]);
        Vector3 leftBottom = plane.transform.TransformPoint(VerticeList[110]);
        Vector3 rightBottom = plane.transform.TransformPoint(VerticeList[120]);
        Vector3 XAxis = rightTop - leftTop;
        Vector3 ZAxis = leftBottom - leftTop;
        Vector3 RndPointonPlane = leftTop + XAxis * Random.value + ZAxis * Random.value;

        agent.destination = RndPointonPlane;
    }
}

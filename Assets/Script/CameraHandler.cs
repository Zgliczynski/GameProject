using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float smoothTime;
    private Vector3 offset;
    private Vector3 currentVelocity = Vector3.zero;



    private void Awake()
    {
        offset = transform.position - target.position;
    }

    private void LateUpdate()
    {
        var targetPosition = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, smoothTime);
    }
}

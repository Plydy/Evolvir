using System;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Follow Settings")]
    [SerializeField] private float smoothTime = 0.15f;
    
    private CameraController cameraController;
    
    private Vector3 velocity;

    private void Awake()
    {
        cameraController = GetComponent<CameraController>();
    }

    private void LateUpdate()
    {
        if (cameraController == null || cameraController.Target == null)
          return;

        transform.position = Vector3.SmoothDamp(
            transform.position,
            cameraController.Target.position,
            ref velocity,
            smoothTime
        );
    }
}

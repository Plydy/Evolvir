using System;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    [Header("Zoom Settings")]
    [SerializeField] private float zoomSpeed = 8f;
    [SerializeField] private float minDistance = 6f;
    [SerializeField] private float maxDistance = 14f;
    [SerializeField] private float zoomSmooth = 15f;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private PlayerInputHandler inputHandler;
    
    private CameraController cameraController;
    
    private Vector3 zoomDirection;
    private float currentDistance;
    private float targetDistance;

    private void Awake()
    {
        cameraController = GetComponent<CameraController>();
        
        if (cameraController == null)
            Debug.LogWarning("CameraController not found!", this);

        if (mainCamera == null)
        {
            Debug.LogError("Main Camera not found!", this);
            return;
        }

        zoomDirection = mainCamera.transform.localPosition.normalized;
        
        currentDistance = mainCamera.transform.localPosition.magnitude;
        targetDistance = currentDistance;
    }

    private void Update()
    {
        float scroll = inputHandler.Zoom;

        if (scroll != 0)
        {
            targetDistance -= scroll * zoomSpeed;

            targetDistance = Mathf.Clamp(
                targetDistance,
                minDistance,
                maxDistance);
        }
        
        currentDistance = Mathf.MoveTowards(
            currentDistance,
            targetDistance,
            Time.deltaTime * zoomSmooth);
        
        mainCamera.transform.localPosition = zoomDirection * currentDistance;
    }
}

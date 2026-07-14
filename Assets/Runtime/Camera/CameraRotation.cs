using System;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    [Header("Rotation Settings")]
    [SerializeField] private float rotationSpeed = 180f;
    [SerializeField] private PlayerInputHandler inputHandler;
    
    private CameraController cameraController;

    private float currentYaw;

    private void Awake()
    {
        cameraController = GetComponent<CameraController>();
    }

    private void Update()
    {
        if (cameraController == null ||
            cameraController.Pivot == null ||
            inputHandler == null)
            return;
        
        if (!inputHandler.CameraRotate)
            return;
        
        Vector2 look = inputHandler.Look;
        
        //Вращаем только когда есть движение мыши
        if (look.sqrMagnitude < 0.001f)
            return;
        
        currentYaw += look.x * rotationSpeed * Time.deltaTime;

        cameraController.Pivot.rotation =
            Quaternion.Euler(0f, currentYaw, 0f);
    }
}

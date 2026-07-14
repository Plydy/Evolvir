using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 12f;
    
    private CharacterController controller;
    
    [SerializeField] private PlayerInputHandler inputHandler;
    
    [SerializeField] private CameraController cameraController;
    
    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        
        if (inputHandler == null)
        {
            Debug.LogError("PlayerInputHandler is not assigned", this);
        }
    }
    
    private void Update()
    {
        if (inputHandler == null)
            return;
        
        Vector2 moveInput = inputHandler.Move;
        
        Transform cameraPivot = cameraController.Pivot;
        
        Vector3 forward = cameraPivot.forward;
        Vector3 right = cameraPivot.right;
        
        forward.y = 0f;
        right.y = 0f;
        
        forward.Normalize();
        right.Normalize();
        
        Vector3 direction =
            forward * moveInput.y + right * moveInput.x;

        if (direction.sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
        
        controller.Move(direction * moveSpeed * Time.deltaTime);
    }
}
using System;
using System.Collections;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    [Header("Dash Settings")]
    [SerializeField, Min(0.5f)] private float dashDistance = 4f;
    [SerializeField, Min(1f)] private float dashSpeed = 20f;
    [SerializeField, Min(0f)] private float dashCooldown = 1f;

    [Header("References")]
    [SerializeField] private PlayerInputHandler inputHandler;
    [SerializeField] private CameraController cameraController;

    private CharacterController controller;
    private PlayerController playerController;
    
    private bool isDashing;
    private bool canDash = true;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        playerController = GetComponent<PlayerController>();
        canDash = true;
        
        if (inputHandler == null)
            Debug.LogError("PlayerInputHandler is not assigned!", this);
        
        if (cameraController == null)
            Debug.LogError("PlayerCameraController is not assigned!", this);
    }

    private void OnEnable()
    {
        if (inputHandler != null)
            inputHandler.OnDash += Dash;
    }

    private void OnDisable()
    {
        if (inputHandler != null)
            inputHandler.OnDash -= Dash;
    }

    private void Dash()
    {
        if (inputHandler.Move == Vector2.zero)
            return;
        
        if (!canDash || isDashing)
            return;
        
        StartCoroutine(DashRoutine());
    }

    private IEnumerator DashRoutine()
    {
        isDashing = true;
        canDash = false;
        
        playerController.CanMove = false;

        Vector3 dashDirection = transform.forward;

        float dashDuration = dashDistance / dashSpeed;
        
        float elapsedTime = 0f;

        while (elapsedTime < dashDuration)
        {
            controller.Move(dashDirection * dashSpeed * Time.deltaTime);
            
            elapsedTime += Time.deltaTime;

            yield return null;
        }
        playerController.CanMove = true;
        
        isDashing = false;
        
        yield return new WaitForSeconds(dashCooldown);
        
        canDash = true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        
        Gizmos.DrawLine(
            transform.position,
            transform.position + transform.forward * dashDistance);
    }
}

using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public Vector2 Move { get; private set; }
    public Vector2 Look { get; private set; }
    
    public bool CameraRotate { get; private set; }

    private PlayerInputActions input;
    
    private void Awake()
    {
        input = new PlayerInputActions();

        BindMovement();
        BindLook();
        BindCamera();
    }

    private void BindMovement()
    {
        input.Player.Move.performed += ctx => Move = ctx.ReadValue<Vector2>();
        input.Player.Move.canceled += _ => Move = Vector2.zero;
    }

    private void BindLook()
    {
        input.Player.Look.performed += ctx => Look = ctx.ReadValue<Vector2>();
        input.Player.Look.canceled += _ => Look = Vector2.zero;
    }

    private void BindCamera()
    {
        input.Player.CameraRotate.performed += _ => CameraRotate = true;
        input.Player.CameraRotate.canceled += _ => CameraRotate = false;
    }

    private void OnEnable()
    {
        input.Enable();
    }

    private void OnDisable()
    {
        input.Disable();
    }
}

using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public Vector2 Move { get; private set; }
    public Vector2 Look { get; private set; }
    public float Zoom { get; private set; }
    
    public bool CameraRotate { get; private set; }
    public event System.Action OnDash;

    private PlayerInputActions input;
    
    private void Awake()
    {
        input = new PlayerInputActions();
        
        BindInputActions();
    }

    private void BindInputActions()
    {
        BindMovement();
        BindLook();
        BindCamera();
        BindDash();
        BindZoom();
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

    private void BindZoom()
    {
        input.Player.Zoom.performed += ctx =>
        {
            Zoom = ctx.ReadValue<float>();
        };
        
        input.Player.Zoom.canceled += _ =>
        {
            Zoom = 0f;
        };
    }

    private void BindDash()
    {
        input.Player.Dash.performed += _ =>
        {
            OnDash?.Invoke();
        };
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

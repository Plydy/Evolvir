using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform target;
    [SerializeField] private Transform pivot;
    [SerializeField] private Camera mainCamera;
    
    public Transform Target => target;
    public Transform Pivot => pivot;
    public Camera MainCamera => mainCamera;
}

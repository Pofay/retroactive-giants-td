using UnityEngine;
using UnityEngine.InputSystem;

public class CameraControl : MonoBehaviour
{
    [Header("Camera Movement Speed settings")]
    public float panSpeed = 30f;
    [Header("Camera Bounding Box Settings")]
    public float maxPanLimitX = 40f;
    public float minPanLimitX = 40f;
    public float maxPanLimitY = 40f;
    public float minPanLimitY = 40f;
    public float panBorderThickness = 10f;

    private Vector2 mousePosition;

    void Update()
    {
        MoveCamera(mousePosition);
    }

    public void PanCamera(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            mousePosition = context.ReadValue<Vector2>();
        }
    }

    private void MoveCamera(Vector2 mousePosition)
    {
        if (mousePosition.y >= Screen.height - panBorderThickness)
        {
            transform.Translate(Vector3.forward * panSpeed * Time.deltaTime, Space.World);
        }
        if (mousePosition.y <= panBorderThickness)
        {
            transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World);
        }
        if (mousePosition.x >= Screen.width - panBorderThickness)
        {
            transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);
        }
        if (mousePosition.x <= panBorderThickness)
        {
            transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);
        }
        var clampedPosition = ClampCurrentPosition();
        transform.position = clampedPosition;
    }

    private Vector3 ClampCurrentPosition()
    {
        var clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, -minPanLimitX, maxPanLimitX);
        clampedPosition.z = Mathf.Clamp(clampedPosition.z, -minPanLimitY, maxPanLimitY);
        return clampedPosition;
    }
}

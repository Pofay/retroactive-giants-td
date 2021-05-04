using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraControl : MonoBehaviour
{
    // Start is called before the first frame update
    public float panSpeed = 30f;
    public float panBorderThickness = 10f;
    public float scrollSpeed = 5f;
    public float maxY = 80f;
    public float minY = 10f;

    private bool doMovement = true;
    private Vector2 mousePosition;

    void Update()
    {
        MoveCamera(mousePosition);
    }

    private void GetInput()
    {
        ScrollCamera();
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
    }

    private void ScrollCamera()
    {
        var scroll = Input.GetAxis("Mouse ScrollWheel");
        var currentPos = transform.position;
        currentPos.y -= (scroll * 1000) * scrollSpeed * Time.deltaTime;
        currentPos.y = Mathf.Clamp(currentPos.y, minY, maxY);
        transform.position = currentPos;
    }
}

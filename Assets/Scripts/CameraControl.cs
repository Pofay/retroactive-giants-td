using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    // Start is called before the first frame update
    public float panSpeed = 30f;
    public float panBorderThickness = 10f;
    public float scrollSpeed = 5f;
    public float maxY = 80f;
    public float minY = 10f;

    private bool doMovement = true;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            doMovement = !doMovement;
        }
        if (!doMovement)
            return;

        GetInput();
    }

    private void GetInput()
    {
        MoveCamera();
        ScrollCamera();
    }

    private void MoveCamera()
    {
        if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panBorderThickness)
        {
            transform.Translate(Vector3.forward * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("s") || Input.mousePosition.y <= panBorderThickness)
        {
            transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panBorderThickness)
        {
            transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("a") || Input.mousePosition.x <= panBorderThickness)
        {
            transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);
        }
    }

    private void ScrollCamera()
    {
        var scroll = Input.GetAxis("Mouse ScrollWheel");
        var currentPos = transform.position;
        Debug.Log(scroll);

        currentPos.y -= (scroll * 1000) * scrollSpeed * Time.deltaTime;
        currentPos.y = Mathf.Clamp(currentPos.y, minY, maxY);
        transform.position = currentPos;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ControllCamera : MonoBehaviour
{
    public float CameraSpeed;
    public float ScrollSpeed;
    private Vector3 MouseCameraController;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveCamera();
        ScrollCamera();
    }

    void MoveCamera()
    {
        if (Input.mousePosition.x >= Screen.width || Input.mousePosition.x <= 0)
        {
            transform.position = transform.position + new Vector3((Screen.width / 2 - Input.mousePosition.x) / Mathf.Abs((Screen.width / 2 - Input.mousePosition.x)) * Time.deltaTime * CameraSpeed * -1, 0, 0);
        }
        if (Input.mousePosition.y >= Screen.height - 10 || Input.mousePosition.y <= 10)
        {
            transform.position = transform.position + new Vector3(0, 0, (Screen.height / 2 - Input.mousePosition.y) / Mathf.Abs((Screen.height / 2 - Input.mousePosition.y)) * Time.deltaTime * CameraSpeed * -1);
        }
    }

    void ScrollCamera()
    {
        transform.position = transform.position + new Vector3(0, Input.mouseScrollDelta.y * ScrollSpeed * Time.deltaTime * -1, 0);
    }
}

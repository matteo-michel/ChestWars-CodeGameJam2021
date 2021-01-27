using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public float mouseSensitivity = 20f;
    public float lerp = 3f;

    float xRotation = 0;
    float yRotation = 0;

    //private bool onPause = false;

    // Start is called before the first frame update
    void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {

        if(!PauseMenu.gameIsPaused)  
        {
            Cursor.visible = false;

            float x = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float y = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            xRotation += y;
            xRotation = Mathf.Clamp(xRotation, -40f, 40f);

            yRotation += x;

            Quaternion current = transform.rotation;

            Quaternion target = Quaternion.Euler(current.x - xRotation, current.y + yRotation, 0);

            transform.rotation = target;
        }
        else
        {
            Cursor.visible = true;
        }
        

    }
}

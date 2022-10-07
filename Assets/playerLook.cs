using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerLook : MonoBehaviour
{
    public float mouseSens = 100f;
    public Transform playerT;
    float xRotation = 0f;
    public hookShooting hs;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        mouseSens = PlayerPrefs.GetFloat("mouseSens", 100);
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSens * Time.smoothDeltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSens * Time.smoothDeltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        //if(hs.pullPlayer || hs.pullObject) xRotation = Mathf.Clamp(xRotation, -90f, 90f - 90*Mathf.Min(0.5f,hs.currPullTime));

        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        playerT.Rotate(Vector3.up * mouseX);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraScript : MonoBehaviour
{
    private Quaternion rotIni;
    public float velRotY;
    public float countY = 0;
    // Start is called before the first frame update
    void Start()
    {
        rotIni = transform.localRotation;
        velRotY = 10;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Cursor.lockState);
        countY += Input.GetAxisRaw("Mouse Y") * Time.deltaTime * velRotY;

        countY = Mathf.Clamp(countY, -60, 60);
        
        Quaternion rotY = Quaternion.AngleAxis(countY, Vector3.left);

        transform.localRotation = rotIni * rotY;
    }
}

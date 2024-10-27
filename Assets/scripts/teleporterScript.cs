using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teleporterScript : MonoBehaviour
{
    public int teleporter;
    public GameObject pc;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        if (teleporter == 1)
        {
            pc.transform.position = new Vector3(48, 1, 0);
        } 
        
        else if (teleporter == 2)
        {
            pc.transform.position = new Vector3(-48, 1, 0);
        }
    }
}

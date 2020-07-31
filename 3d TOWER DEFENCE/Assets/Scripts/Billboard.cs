using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    public Transform cam;
    
    private void Start()
    {
        
    }
    private void LateUpdate()
    {
        cam = Camera.main.transform;
        transform.LookAt(transform.position + cam.forward);
    }
}

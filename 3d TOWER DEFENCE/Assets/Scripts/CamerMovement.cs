using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA;

public class CamerMovement : MonoBehaviour
{
    [Header("Speeds:")]

    [SerializeField] float movementSpeed = 5;
    [SerializeField] float scrollSensitivity = 0.5f;
    
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            
            Vector3 newPosition = new Vector3(-movementSpeed * Time.deltaTime , 0, 0);
            transform.Translate(newPosition, Space.World);
        }
        if (Input.GetKey(KeyCode.D))
        {
            Vector3 newPosition = new Vector3(movementSpeed * Time.deltaTime, 0, 0);
            transform.Translate(newPosition, Space.World);
        }
        if (Input.GetKey(KeyCode.S))
        {
            Vector3 newPosition = new Vector3(0, 0, -movementSpeed * Time.deltaTime);
            transform.Translate(newPosition, Space.World);
        }
        if (Input.GetKey(KeyCode.W))
        {
            Vector3 newPosition = new Vector3(0, 0, movementSpeed * Time.deltaTime);
            transform.Translate(newPosition, Space.World);
        }
        if (Input.GetMouseButtonDown(2))
        {
            transform.position = new Vector3(transform.position.x, 18.3f, transform.position.z);
        }
        Vector3 newPos = new Vector3(0, Input.GetAxis("Mouse ScrollWheel") * scrollSensitivity * Time.deltaTime * -1, 0);
        transform.Translate(newPos, Space.World);

        if (transform.position.y < -2 || transform.position.y > 25)
        {
            transform.position = new Vector3(transform.position.x, 18.3f, transform.position.z);
        }
        if (transform.position.z < -55 || transform.position.z > 35)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 23.5f);
        }
    }
}

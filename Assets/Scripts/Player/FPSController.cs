using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSController : MonoBehaviour
{   
public CharacterController controller;
public InputActions controls;
public float mouseSensitivity;
private float mouseYvalue;
public float movementSpeed;
Vector3 movementInput;
public Transform cameratransform;
public GameObject coob;
bool isMoving;
bool isGrounded;
LayerMask groundingLayerMask;
    void Awake()
    {

    }
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        groundingLayerMask = LayerMask.NameToLayer("Default");
        
    }
    void Update()
    {
        MousingMoment();
        PlanarMovementChecking();
        GroundChecker();

        if (isMoving) MovementMain();
    }

    private void PlanarMovementChecking()
    {
        movementInput = transform.right*Input.GetAxisRaw("Horizontal")+transform.forward*Input.GetAxisRaw("Vertical");
        if (movementInput != Vector3.zero)
        {
            isMoving = true;
        }
        else { isMoving = false; }
    }

    void MovementMain(){
        controller.Move(movementSpeed*Time.deltaTime*movementInput.normalized);
    }
    void MousingMoment(){
        float mouseX=Input.GetAxisRaw("Mouse X")*mouseSensitivity*100f*Time.deltaTime;
        float mouseY=Input.GetAxisRaw("Mouse Y")*mouseSensitivity*100f*Time.deltaTime;
        transform.Rotate(Vector3.up,mouseX);
        mouseYvalue-=mouseY;
        mouseYvalue=Mathf.Clamp(mouseYvalue,-90,90);
        cameratransform.localRotation=Quaternion.Euler(mouseYvalue,cameratransform.localRotation.y,transform.localRotation.z);
    }
    void GroundChecker(){
        if(Physics.Raycast(transform.position,Vector3.down,.55f)){Debug.DrawLine(transform.position,transform.position+Vector3.down*.55f,Color.blue);}
    }

}

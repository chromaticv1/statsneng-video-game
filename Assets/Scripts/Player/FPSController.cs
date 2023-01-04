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
Vector3 velocity;
public float jumpHeight;
public float gravity;
public float thiccness;
public Transform cameratransform;
public GameObject coob;
bool isMoving;
bool isGrounded;
public LayerMask groundingLayerMask;
    void Awake()
    {

    }
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        
    }
    void Update()
    {
        MousingMoment();
        PlanarMovementChecking();
        GroundChecker();
        GravityAndOthers();

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
    void GravityAndOthers(){
        if(!isGrounded){
            velocity.y+=gravity*Time.deltaTime;}
        else if(isGrounded &&velocity.y>0){
            return;
        }
        else{velocity.y=0;}
        if(Input.GetKeyDown(KeyCode.Space)&&isGrounded){velocity.y=Mathf.Sqrt(2*-gravity*jumpHeight);}
        controller.Move(velocity*Time.deltaTime);
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
        Ray ray1 = new Ray (transform.position+thiccness*(transform.forward+transform.right),Vector3.down);
        Ray ray2 = new Ray (transform.position+thiccness*(transform.forward-transform.right),Vector3.down);
        Ray ray3 = new Ray (transform.position+thiccness*(-transform.forward+transform.right),Vector3.down);
        Ray ray4 = new Ray (transform.position+thiccness*(-transform.forward-transform.right),Vector3.down);

        if(Physics.Raycast(ray1,1.09f,groundingLayerMask)||Physics.Raycast(ray2,1.09f,groundingLayerMask)||Physics.Raycast(ray3,1.09f,groundingLayerMask)||Physics.Raycast(ray4,1.09f,groundingLayerMask)){
            isGrounded=true;
        }
        else{isGrounded=false;}
        print(isGrounded);
    
    }

}

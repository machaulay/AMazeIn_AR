using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{

    public GameObject Character;
    public float speed;
    public float gravity;
    public Camera _cam;

    private Vector3 moveDirection = Vector3.zero;
    private CharacterController controller;

    private Vector3 whereIs;
    private bool isNormal;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        isNormal = true;
    }


    void Update()
    {
        

        whereIsCamera();


        if (!isNormal)
        {
            //Move
            moveDirection = new Vector3(Input.GetAxis("Horizontal2"), 0.0f, Input.GetAxis("Vertical2"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;

        }
        else
        {
            //Move
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;
        }

    
            Direction();



        //Gravity
        moveDirection.y -= gravity * Time.deltaTime;

        //Apply Values in Character Controller(move and gravity)
        controller.Move(moveDirection * Time.deltaTime);

        if (isNormal)
        {
            print("Normal");
        }
        else
        {
            print("Not normal");
        }

    }

    void Direction()
    {

        if (Input.GetKey("left"))
        {
            Character.transform.rotation = Quaternion.Euler(0, 280, 0);
        }
        if (Input.GetKey("right"))
        {
            Character.transform.rotation = Quaternion.Euler(0, -280, 0);
        }
        if (Input.GetKey("up"))
        {
            Character.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        if (Input.GetKey("down"))
        {
            Character.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }

    void whereIsCamera()
    {
        whereIs = transform.position - _cam.transform.position;
        print(transform.position - _cam.transform.position);

        if (whereIs.x < 0 && whereIs.z < 0)
        {
            isNormal = false;
        }
        else if (whereIs.x > 0 && whereIs.z < 0)
        {
            isNormal = false;
        }
        else if (whereIs.x < 0 && whereIs.z > 0)
        {
            isNormal = true;
        }
        else if (whereIs.x > 0 && whereIs.z > 0)
        {
            isNormal = true;
        }
    }

   


}

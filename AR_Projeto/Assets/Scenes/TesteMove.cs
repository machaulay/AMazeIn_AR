using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TesteMove : MonoBehaviour
{
    private CharacterController charControl;
    public GameObject playerMesh;

    public float moveSpeed;
    public float rotationSpeed;

    //Camera
    [Header("Camera")]
    public Camera _camera;
    private Vector3 camf;
    private Vector3 camr;
    private Vector3 charMove;

    public Joystick joystick;

    // Start is called before the first frame update
    void Start()
    {
        charControl = gameObject.GetComponent<CharacterController>();

    }

    // Update is called once per frame
    void Update()
    {
        camf = Vector3.ProjectOnPlane(Camera.main.transform.forward, Vector3.up);
        camr = Vector3.ProjectOnPlane(Camera.main.transform.right, Vector3.down);

        camf.y = 0;
        camr.y = 0;
        camf = camf.normalized;
        camr = camr.normalized;




        
        charMove = (camf * joystick.Vertical + camr * joystick.Horizontal) * moveSpeed;
        charControl.Move(charMove);

        Vector3 lookDir = new Vector3(charMove.x, 0, charMove.z);
        playerMesh.transform.rotation = Quaternion.LookRotation(lookDir);

    }
}

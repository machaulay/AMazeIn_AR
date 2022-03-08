//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.XR.ARFoundation;
//using UnityEngine.XR.ARSubsystems;

//public class ARTapToInteract : MonoBehaviour
//{
//    public float rotateSpeed = 1000f;
//    private float _startingPosition;

//    public GameObject azulCitosina;
//    public GameObject amareloAdenina;
//    public GameObject vermelhoTimina;
//    public GameObject verdeGuanina;
    
//    public GameObject[] azul;
//    public GameObject[] amarelo;
//    public GameObject[] vermelho;
//    public GameObject[] verde;
    

//    public void Start() {
        

//        ARUserInterface.azulAnim = azulCitosina.GetComponent<Animator>();
//        ARUserInterface.amareloAnim = amareloAdenina.GetComponent<Animator>();
//        ARUserInterface.vermelhoAnim = vermelhoTimina.GetComponent<Animator>();
//        ARUserInterface.verdeAnim = verdeGuanina.GetComponent<Animator>();

//        for (int i = 0; i < azul.Length; i++)
//        {
//            ARUserInterface.azul[i] = azul[i];
//            print("Azul " + i);
//        }
//        for (int i = 0; i < amarelo.Length; i++)
//        {
//            ARUserInterface.amarelo[i] = amarelo[i];
//            print("Amarelo " + i);
//        }
//        for (int i = 0; i < vermelho.Length; i++)
//        {
//            ARUserInterface.vermelho[i] = vermelho[i];
//            print("Vermelho " + i);
//        }
//        for (int i = 0; i < verde.Length; i++)
//        {
//            ARUserInterface.verde[i] = verde[i];
//            print("Verde " + i);
//        }
//    }
//    void Update() {
//        if(Input.touchCount > 0) {

            

//            Touch touch = Input.GetTouch(0);
//            switch (touch.phase) {

//                case TouchPhase.Began:
//                    _startingPosition = touch.position.x;
//                    break;
//                case TouchPhase.Moved:
//                    if(_startingPosition > touch.position.x) {

//                        transform.Rotate(Vector3.up, -rotateSpeed * Time.deltaTime);                 
//                    }else if(_startingPosition < touch.position.x) {

//                        transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime);
//                    }
//                    break;
//                case TouchPhase.Ended:
//                    Debug.Log("Tirou o dedo da tela.");
//                    break;
//            }

//            if(Input.GetTouch(0).phase == TouchPhase.Began) {

//                Ray raycast = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);

//                RaycastHit raycastHit;

//                if (Physics.Raycast(raycast, out raycastHit)) {

//                    if(raycastHit.collider.CompareTag("dna")) {
                        
//                        ARUserInterface.dnaInterface = true;
//                    }
//                }
//            }
//        }


//    }
//}

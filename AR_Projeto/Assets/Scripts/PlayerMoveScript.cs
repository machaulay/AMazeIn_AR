using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



public class PlayerMoveScript : MonoBehaviour, IShopCustomer
{
    [Header("Inventário")]
    private Inventory inventory;
    public static String tagItem;
    public TextMeshProUGUI moedaInventario;

    [SerializeField] private UI_Inventory uiInventory;
    private LojaScript lojaScript;
    public event EventHandler MoedaQuantiaTroca;

    [Header("Controles e Animação")]
    public GameObject playerMesh;
    public float moveSpeed;
    public float rotationSpeed;
    public float gravity = 9.8f;
    private float vSpeed = 0;
    public float distanciaMovimento = 1.0f;
    public int pixeldistancetodetect = 20;
    //private Vector3 _startingPosition;
    //private Vector3 _currentPosition;
    //private bool fingerDown = false;
    //Vector2 pointA;
    //Vector2 pointB;
    public Transform circle;
    public Transform outerCircle;
    //private Vector3 direction;
    private Animator playeranim;

    public Joystick joystick;

    private CharacterController charControl;
    private Vector3 charMove;


    //Camera
    [Header("Camera")]
    public Camera _camera;
    private Vector3 camf;
    private Vector3 camr;

    private bool TemChave;

    [Header("Audios")]
    public AudioSource minerandoFx;
    public AudioSource comprandoFX;

    private void Awake() {

        charControl = gameObject.GetComponent<CharacterController>();

        lojaScript = GameObject.Find("UI_Shop").GetComponent<LojaScript>();
        lojaScript.Show(GameObject.Find("Player").GetComponent<IShopCustomer>());
        playeranim = playerMesh.GetComponent<Animator>();
        inventory = new Inventory();
        uiInventory.SetInventory(inventory);
    }
    void Start() {
        _camera = Camera.main;

        //this.transform.position = new Vector3(0, 0.23f, 0);

        TemChave = false;


    }

    void Update() {

        moedaInventario.text = LojaScript.totalMoeda.ToString();

        if (GameController.Instance.State != GameController.GameStates.PLAYING)
        {
            playeranim.SetBool("pMove", false);
            return;

        }

        camf = Vector3.ProjectOnPlane(Camera.main.transform.forward, Vector3.up);
        camr = Vector3.ProjectOnPlane(Camera.main.transform.right, Vector3.down);

        camf.y = 0;
        camr.y = 0;
        camf = camf.normalized;
        camr = camr.normalized;


        //TouchJoystick();
        //SwipeMove();

        //Detectar obstaculos
        Ray rayF = new Ray(playerMesh.transform.position, Vector3.forward);

        Ray rayB = new Ray(playerMesh.transform.position, Vector3.back);
        //Ray rayL = new Ray(transform.position, Vector3.left);
        //Ray rayR = new Ray(transform.position, Vector3.right);
        Debug.DrawRay(rayF.origin, rayF.direction * .08f);
        Debug.DrawRay(rayB.origin, rayB.direction * .08f);
        //Debug.DrawRay(rayL.origin, rayL.direction * .1f);
        //Debug.DrawRay(rayR.origin, rayR.direction * .1f);

        RaycastHit hitdata;
        if (Physics.Raycast(rayF, out hitdata, .08f))
        {
            if (hitdata.collider.tag == "Pedra")
            {
                Debug.Log("Bateu na pedra! Frente");

                tagItem = hitdata.collider.tag;
                hitdata.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().enabled = true;

            }
            else if (hitdata.collider.tag == "Chave")
            {
                tagItem = hitdata.collider.tag;

                if (TemChave)
                {
                    hitdata.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().enabled = false;
                }
                else
                {
                    hitdata.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().enabled = true;

                }
            }
            else if (hitdata.collider.tag == "ArcoPonte")
            {

                tagItem = hitdata.collider.tag;
                Debug.Log(tagItem);

                if (Ponte_Script.concertar)
                {
                    hitdata.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().enabled = true;

                }
                else
                {
                    hitdata.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().enabled = false;

                }

            }
            else if (hitdata.collider.tag == "Portal")
            {
                tagItem = hitdata.collider.tag;

            }
            else if (hitdata.collider.tag == "Respawn")
            {
                StartCoroutine(GameController.CarregaCena("GameplayScene"));

            }

        }



        if (Physics.Raycast(rayB, out hitdata, .08f))
        {
            if (hitdata.collider.tag == "Pedra")
            {
                Debug.Log("Bateu na pedra! Trás");

                tagItem = hitdata.collider.tag;
                hitdata.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().enabled = true;

            }
            else if (hitdata.collider.tag == "Chave")
            {
                tagItem = hitdata.collider.tag;

                if (TemChave)
                {
                    hitdata.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().enabled = false;
                }
                else
                {
                    hitdata.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().enabled = true;

                }
            }
            else if (hitdata.collider.tag == "ArcoPonte")
            {
                tagItem = hitdata.collider.tag;
                Debug.Log(tagItem);

                if (Ponte_Script.concertar)
                {
                    hitdata.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().enabled = true;

                }
                else
                {
                    hitdata.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().enabled = false;

                }

            }
            else if (hitdata.collider.tag == "Portal")
            {
                tagItem = hitdata.collider.tag;

            }
            else if (hitdata.collider.tag == "Respawn")
            {
                StartCoroutine(GameController.CarregaCena("GameplayScene"));

            }

        }
        

        //if (Physics.Raycast(rayL, out hitdata, .1f))
        //{

        //}

        //if (Physics.Raycast(rayR, out hitdata, .1f))
        //{

        //}



       

        //if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject(0))
        //{

        //}
        // Teste Controle
        ////transform.position += (camf * joystick.Vertical + camr * joystick.Horizontal) * moveSpeed * Time.deltaTime;
        ////Vector3 moveDir = new Vector3(joystick.Horizontal * rotationSpeed, 0, joystick.Vertical * rotationSpeed);
        ////Vector3 lookDir = new Vector3(moveDir.x * camf.x, 0, moveDir.z * camr.z);
        ////lookDir.Normalize();
        ///
        charMove = (camf * joystick.Vertical + camr * joystick.Horizontal) * moveSpeed * Time.deltaTime;
        Vector3 lookDir = new Vector3(charMove.x, 0.0f, charMove.z);
        vSpeed -= gravity * Time.deltaTime;
        charMove.y = vSpeed; // include vertical speed in vel
        charControl.Move(charMove);

        if (lookDir != Vector3.zero)
        {
            playeranim.SetBool("pMove", true);
            playerMesh.transform.rotation = Quaternion.LookRotation(lookDir);
        }
        else
        {
            playeranim.SetBool("pMove", false);

        }



    }

    private void FixedUpdate()
    {
        //    if (GameController.Instance.State != GameController.GameStates.PLAYING)
        //    {
        //        playeranim.SetBool("pMove", false);
        //    return;

        //    }

        //    //if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject(0))
        //    //{

        //    //}
        //    // Teste Controle
        //    ////transform.position += (camf * joystick.Vertical + camr * joystick.Horizontal) * moveSpeed * Time.deltaTime;
        //    ////Vector3 moveDir = new Vector3(joystick.Horizontal * rotationSpeed, 0, joystick.Vertical * rotationSpeed);
        //    ////Vector3 lookDir = new Vector3(moveDir.x * camf.x, 0, moveDir.z * camr.z);
        //    ////lookDir.Normalize();
        //    ///
        //    charMove = (camf * joystick.Vertical + camr * joystick.Horizontal) * moveSpeed * Time.deltaTime;
        //    Vector3 lookDir = new Vector3(charMove.x, 0.0f, charMove.z);

        //    charControl.Move(charMove);

        //    if (charMove != Vector3.zero)
        //    {
        //        playeranim.SetBool("pMove", true);
        //        playerMesh.transform.rotation = Quaternion.LookRotation(lookDir);
        //    }
        //    else
        //    {
        //        playeranim.SetBool("pMove", false);

        //    }



        //if (fingerDown)
        //{
        //    Vector2 offset = pointB - pointA;
        //    direction = Vector3.ClampMagnitude(offset, 1.0f);
        //    moveCharacterDirection(direction);

        //}
        //else
        //{
        //    circle.GetComponent<Image>().enabled = false;
        //    outerCircle.GetComponent<Image>().enabled = false;
        //}


    }

    //void moveCharacterDirection(Vector3 direction)
    //{
    //    transform.position += (camf * direction.y + camr * direction.x) * moveSpeed * Time.deltaTime;    
    //}



    //void TouchJoystick()
    //{
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        _startingPosition = Input.mousePosition;
    //        pointA = _camera.ScreenToViewportPoint(new Vector3(Input.mousePosition.x,
    //            Input.mousePosition.y, _camera.transform.position.z));

    //        circle.GetComponent<RectTransform>().position = Input.mousePosition;
    //        outerCircle.GetComponent<RectTransform>().position = Input.mousePosition;
    //        if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject(0))
    //        {
    //            circle.GetComponent<Image>().enabled = false;
    //            outerCircle.GetComponent<Image>().enabled = false;
    //        }
    //        else
    //        {
    //            circle.GetComponent<Image>().enabled = true;
    //            outerCircle.GetComponent<Image>().enabled = true;

    //        }
    //    }

    //    if (Input.GetMouseButton(0))
    //    {
    //        fingerDown = true;
    //        circle.GetComponent<RectTransform>().position = new Vector2(Input.mousePosition.x + direction.x, Input.mousePosition.y + direction.y);
            
    //        pointB = _camera.ScreenToViewportPoint(new Vector3(Input.mousePosition.x,
    //            Input.mousePosition.y, _camera.transform.position.z));
    //    }

    //    if (fingerDown && Input.GetMouseButtonUp(0))
    //    {
    //        fingerDown = false;
    //    }
    //}

    //void SwipeMove()
    //{
    //    if (Input.touchCount > 0)
    //    {
    //        Touch touch = Input.GetTouch(0);

    //        //Switch para detectar as fases do toque
    //        switch (touch.phase)
    //        {
    //            case TouchPhase.Began:

    //                fingerDown = true;
    //                pointA = _camera.ScreenToViewportPoint(new Vector3(touch.position.x,
    //                    touch.position.y, _camera.transform.position.z));

    //                circle.GetComponent<RectTransform>().position = touch.position;
    //                outerCircle.GetComponent<RectTransform>().position = touch.position;

    //                if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject(0))
    //                {
    //                    circle.GetComponent<Image>().enabled = false;
    //                    outerCircle.GetComponent<Image>().enabled = false;
    //                }
    //                else
    //                {
    //                    circle.GetComponent<Image>().enabled = true;
    //                    outerCircle.GetComponent<Image>().enabled = true;
    //                    circle.GetComponent<RectTransform>().position = new Vector2(touch.position.x + direction.x, touch.position.y + direction.y);

    //                }
    //                break;
    //            case TouchPhase.Moved:

    //                pointB = _camera.ScreenToViewportPoint(new Vector3(touch.position.x,
    //                    touch.position.y, _camera.transform.position.z));
    //                break;
    //            case TouchPhase.Ended:
    //                fingerDown = false;
    //                break;
    //        }
    //    }
    //}

    public void ComprarItem(Item.ItemType itemType)
    {
        Debug.Log("Comprado: " + itemType);
        comprandoFX.Play(0);

        //Grava qual item foi comprado
        Dictionary<string, object> eventData = new Dictionary<string, object>()
                {
                    {"ItemComprado", itemType}
                };
        PlayfabManager.instance.WritePlayerEvent("ItemComprado", eventData);

        switch (itemType)
        {

            case Item.ItemType.Madeira:
                inventory.AddItem(new Item {itemType = Item.ItemType.Madeira, amount = 1 });
                break;
            case Item.ItemType.Machado:
                inventory.AddItem(new Item { itemType = Item.ItemType.Machado, amount = 1 });
                break;
            case Item.ItemType.Picareta:
                inventory.AddItem(new Item { itemType = Item.ItemType.Picareta, amount = 1 });
                break;
        }
    }

    public bool TentarGastarMoeda(int quantiaMoeda)
    {
        if (LojaScript.totalMoeda >= quantiaMoeda)
        {
            LojaScript.totalMoeda -= quantiaMoeda;
            MoedaQuantiaTroca?.Invoke(this, EventArgs.Empty);
            return true;
        }
        else
        {
            return false;
        }
    }

    public void InteragirCom()
    {
        if (tagItem == "Pedra")
        {
            StartCoroutine(Minerar());
        }
        else if (tagItem == "ArcoPonte")
        {
            Debug.Log("Entrou");

            Item itemPedra = inventory.GetItemList().Find(x => x.itemType == Item.ItemType.Pedra);
            Item itemPicareta = inventory.GetItemList().Find(x => x.itemType == Item.ItemType.Picareta);

            if (itemPedra == null && itemPicareta == null && Ponte_Script.concertar == false)
            {
                //Não pode concertar
            }
            else if(itemPedra != null && itemPicareta != null && Ponte_Script.concertar == false)
            {
                Debug.Log("Ponte Concertada.");
                Ponte_Script.concertar = true;
                inventory.RemoveItem(new Item { itemType = Item.ItemType.Pedra, amount = 1 });

                tagItem = "";


            }
        }
        else if (tagItem == "Chave")
        {
            transform.GetChild(1).gameObject.GetComponent<MeshRenderer>().enabled = true;
            TemChave = true;
        }
        else if (tagItem == "Portal" && TemChave)
        {
            Debug.Log("Fase Concluída!");
            tagItem = "";
            StartCoroutine(GameController.CarregaCena("GameplayScene"));

        }
        else
        {
            Debug.Log(tagItem);

            Debug.Log("Nada.");
        }
    }

    IEnumerator Minerar()
    {
        playeranim.SetBool("isMinning", true);
        yield return new WaitForSeconds(.20f);
        inventory.AddItem(new Item { itemType = Item.ItemType.Pedra, amount = 1 });
        minerandoFx.Play(0);
        playeranim.SetBool("isMinning", false);
        tagItem = "";

    }

    //Colisões
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(tagItem);
        if (other.gameObject.CompareTag("Chave"))
        {
            tagItem = other.tag;

            if (TemChave)
            {
                other.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().enabled = false;
            }
            else
            {
                other.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().enabled = true;

            }
        }

        if (other.gameObject.CompareTag("ArcoPonte"))
        {
            tagItem = other.tag;

            if(Ponte_Script.concertar)
            {
                other.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().enabled = true;

            }
            else
            {
                other.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().enabled = false;

            }

        }

        if (other.gameObject.CompareTag("Pedra"))
        {
            tagItem = other.tag;
            other.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().enabled = true;

        }

        if (other.gameObject.CompareTag("Portal"))
        {
            tagItem = other.tag;

        }

    }
    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject.CompareTag("PonteQuebrada"))
        {
            Destroy(other.gameObject, .5f);
            Ponte_Script.ativarBloqueio = true;
            GameController.Instance.State = GameController.GameStates.PAUSED;
            GameController.dialogo = 2;
        }

        if (other.gameObject.CompareTag("Chave"))
        {
            other.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().enabled = false;
            tagItem = "";

        }

        if (other.gameObject.CompareTag("ArcoPonte"))
        {
            other.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().enabled = false;
            tagItem = "";

        }

        if (other.gameObject.CompareTag("Pedra"))
        {
            other.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().enabled = false;
            tagItem = "";
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Chave"))
        {
            if (TemChave)
            {
                other.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().enabled = false;
            }
            else
            {
                other.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().enabled = true;

            }
        }
    }
}

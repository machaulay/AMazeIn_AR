using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameController : MonoBehaviour
{
    private GameStates state = GameStates.WAIT_TO_START;

    //UI
    int countToStart = 15 ;
    public TextMeshProUGUI countDownTxt;
    public GameObject loja;
    public GameObject dialogueBox1;
    public GameObject dialogueBox2;
    public static int dialogo = 0;
    public static bool lojaFechada = false;

    [Header("Audios")]
    public AudioSource telaCompraFx;
    public AudioSource gameplayFx;

    private static GameController _instance;
    public static GameController Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = FindObjectOfType<GameController>();
            }
            return _instance;
        }
    }

    //Usar para determinar cada estado da gameplay
    public enum GameStates
    {
        WAIT_TO_START,
        GAMEOVER,
        PLAYING,
        PAUSED
    }

    public delegate void OnCountDownTick();
    public static OnCountDownTick OnCountDownTickEvent;

    public delegate void OnGameStart();
    public static OnGameStart OnGameStartEvent;
    public GameStates State { get => state; set => state = value; }

    //Diálogo

   
    IEnumerator Start()
    {
        yield return new WaitForSeconds(1);
        countToStart--;
        if (countToStart < -1 || lojaFechada)
        {
            telaCompraFx.Pause();
            gameplayFx.Play(0);
            Debug.Log("Começar!");
            //state = GameStates.PLAYING;
            countDownTxt.gameObject.SetActive(false);
            loja.gameObject.SetActive(false);
            dialogo = 1;
            dialogueBox1.SetActive(true);
            OnGameStartEvent?.Invoke();
        }
        else
        {
            Debug.Log("Menos 1");
            countDownTxt.text = countToStart.ToString();
            OnCountDownTickEvent?.Invoke();
            StartCoroutine(Start());
        }
    }
    private void Update()
    {
        if (GameController.Instance.State == GameController.GameStates.PAUSED && dialogo == 2)
        {
            dialogueBox2.SetActive(true);

        }

    }
    public static IEnumerator CarregaCena(string cena)
    {
        yield return new WaitForSeconds(3.0f);
        SceneManager.LoadScene(cena);
    }

}

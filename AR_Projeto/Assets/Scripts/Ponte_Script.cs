using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ponte_Script : MonoBehaviour
{
    public GameObject ponteInteira;
    public GameObject ponteQuebrada;

    //Bloqueio
    public Collider bloqueioCollider;
    public static bool ativarBloqueio;

    //Concertar ponte
    public static bool concertar;


    void Start()
    {
        bloqueioCollider.enabled = false;
    }

    private void Update()
    {
        PonteBuild();
    }

    private void PonteBuild()
    {
        if (ativarBloqueio)
        {
            bloqueioCollider.enabled = true;
            ativarBloqueio = false;
        }

        if (concertar)
        {
            ponteInteira.SetActive(true);
            bloqueioCollider.enabled = false;

        }
    }
}

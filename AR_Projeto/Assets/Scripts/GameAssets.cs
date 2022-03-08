 using UnityEngine;
using System.Reflection;

public class GameAssets : MonoBehaviour {

    public static GameAssets Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public Sprite s_Madeira;
    public Sprite s_Machado;
    public Sprite s_Pedra;
    public Sprite s_Picareta;

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item {

    public enum ItemType {
        Madeira,
        Machado,
        Pedra,
        Picareta
        
    }
    public ItemType itemType;
    public int amount;

    public static int GetCost(ItemType itemType)
    {
        switch (itemType)
        {
            default:
            case ItemType.Madeira: return 1;
            case ItemType.Pedra: return 2;
            case ItemType.Machado: return 10;
            case ItemType.Picareta: return 10;
        }
    }

    //public static Sprite GetSprite(ItemType itemType)
    //{
    //    switch (itemType)
    //    {
    //        default:
    //        case ItemType.Madeira: return GameAssets.Instance.s_Madeira;
    //        case ItemType.Machado: return GameAssets.Instance.s_Machado;

    //    }
    //}

    public static Sprite GetSprite(ItemType itemType)
    {
        switch (itemType)
        {
            default:
            case ItemType.Madeira: return GameAssets.Instance.s_Madeira;
            case ItemType.Machado: return GameAssets.Instance.s_Machado;
            case ItemType.Picareta: return GameAssets.Instance.s_Picareta;
            case ItemType.Pedra: return GameAssets.Instance.s_Pedra;

        }
    }

    public bool isStackable()
    {
        switch (itemType)
        {
            default:
            case ItemType.Madeira:
                return true;
            case ItemType.Machado:
                return false;
        }
    }

}

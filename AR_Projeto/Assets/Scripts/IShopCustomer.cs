using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShopCustomer {
    void ComprarItem(Item.ItemType itemType);
    bool TentarGastarMoeda(int quantiaMoeda);
}

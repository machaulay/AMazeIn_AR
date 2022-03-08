using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LojaScript : MonoBehaviour
{
    private Transform container;
    private Transform itemLojaTemplate;
    private IShopCustomer shopCustomer;

    private TextMeshProUGUI MoedaLojaTxt;
    public static int totalMoeda = 100;

    private void Awake()
    {
        container = transform.Find("container");
        itemLojaTemplate = container.Find("itemLojaTemplate");
        itemLojaTemplate.gameObject.SetActive(false);

        MoedaLojaTxt = container.Find("moedaLojaTxt").GetComponent<TextMeshProUGUI>();
        MoedaLojaTxt.text = totalMoeda.ToString();
    }

    private void Start()
    {
        CriarItemBotao(Item.ItemType.Madeira, Item.GetSprite(Item.ItemType.Madeira), "Madeira", Item.GetCost(Item.ItemType.Madeira), 0);
        CriarItemBotao(Item.ItemType.Machado, Item.GetSprite(Item.ItemType.Machado), "Machado", Item.GetCost(Item.ItemType.Machado), 1);
        CriarItemBotao(Item.ItemType.Picareta, Item.GetSprite(Item.ItemType.Picareta), "Picareta", Item.GetCost(Item.ItemType.Picareta), 2);

    }

    private void CriarItemBotao(Item.ItemType itemType, Sprite itemSprite, string itemNome, int itemCusto, int positionIndex)
    {
        Transform itemLojaTransform = Instantiate(itemLojaTemplate, container);
        itemLojaTransform.gameObject.SetActive(true);
        RectTransform itemLojaRectTransform = itemLojaTransform.GetComponent<RectTransform>();

        float itemLojaAltura = 210;
        itemLojaRectTransform.anchoredPosition = new Vector2(0, -itemLojaAltura * positionIndex);

        itemLojaTransform.Find("itemNome").GetComponent<TextMeshProUGUI>().text = itemNome;
        itemLojaTransform.Find("costText").GetComponent<TextMeshProUGUI>().text = itemCusto.ToString();

        itemLojaTransform.Find("itemImage").GetComponent<Image>().sprite = itemSprite;


        if ( itemType == Item.ItemType.Machado)
        {
            itemLojaTransform.GetComponent<Button>().onClick.AddListener(() => TryBuyItem(itemType));
            itemLojaTransform.GetComponent<Button>().onClick.AddListener(() => DisableItem(itemLojaTransform.GetComponent<Button>()));

        }
        else if (itemType == Item.ItemType.Picareta)
        {
            itemLojaTransform.GetComponent<Button>().onClick.AddListener(() => TryBuyItem(itemType));
            itemLojaTransform.GetComponent<Button>().onClick.AddListener(() => DisableItem(itemLojaTransform.GetComponent<Button>()));
        }
        else
        {
            itemLojaTransform.GetComponent<Button>().onClick.AddListener(() => TryBuyItem(itemType));

        }
    }

    private void TryBuyItem(Item.ItemType itemType)
    {
        if (shopCustomer.TentarGastarMoeda(Item.GetCost(itemType)))
        {
            shopCustomer.ComprarItem(itemType);
            MoedaLojaTxt.text = totalMoeda.ToString();
        }
        
    }

    private void DisableItem(Button btn)
    {
        btn.GetComponent<Button>().interactable = false;

    }


    public void Show(IShopCustomer shopCustomer)
    {
        this.shopCustomer = shopCustomer;

    }

    public void IniciarJogo()
    {
        GameController.lojaFechada = true;
    }

}

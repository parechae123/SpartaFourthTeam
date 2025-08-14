using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Inventory inventory { private get; set; } = null;
    IObtainable item;
    [SerializeField] TextMeshProUGUI quantityText;
    [SerializeField] Image itemIcon;

    //item in Inventory Info
    int quantity;


    // Start is called before the first frame update
    void Start()
    {
        quantityText.text = "";
        itemIcon.gameObject.SetActive(false);
    }

    //return true when success put item in inventory
    //return false when failure put itme in inventory
    public bool TryAddItem(IObtainable obtainable)
    {
        throw new System.NotImplementedException("Inventory Slot AddItem");    
        //Try if obtainable is null, if it is same, add item and return true

        //Try if obtainable is different, if it is different, return false

        //Try if obtainable is stackable, if not, return false

        //Try if quantity is smaller than maxquantity, if true, add item and return true
        //Else return false;
    }

    public void OnButtonPressed()
    {
        if (inventory == null)
        {
            throw new System.Exception("inventory is null");
        }
        if (item != null)
        {
            inventory.ShowItemInfo(item);
        }
    }
}

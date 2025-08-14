using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [Header("왼쪽 패널 정보")]
    [SerializeField] RectTransform leftPanel;
    [SerializeField] InventorySlot slotPrefab;
    const int slotnumber = 25;

    [Header("오른쪽 패널 정보")]
    [SerializeField] RectTransform rightPanel;
    [SerializeField] TextMeshProUGUI itemNameText;
    [SerializeField] TextMeshProUGUI itemInfoText;
    [SerializeField] Button useButton;
    [SerializeField] Button dropButton;

    [Header("아이템 저장 정보")]
    InventorySlot[] inventoryItems = new InventorySlot[slotnumber];

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < slotnumber; i++)
        {
            InventorySlot slot = Instantiate(slotPrefab, leftPanel);
            slot.inventory = this;
        }
        rightPanel.gameObject.SetActive(false);
    }

    public bool ObtainItem(IObtainable obtainable)
    {
        for (int i = 0; i < slotnumber; i++)
            if (inventoryItems[i].TryAddItem(obtainable))
            {
                //save iteminfo copy to slot
                //unactive obtainable
                
                return true;
            }
        return false;
    }

    public void ShowItemInfo(IObtainable obtainable)
    {
        if (obtainable == null) return;
        //Set Obtainable item name
        //Set Obtainable item info
        //Show button if useable
    }

    public void ActivateUI(bool isActivate)
    {
        this.gameObject.SetActive(isActivate);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shop : MonoBehaviour
{
    /*public Inventory inventoryPlayer;
    public GameObject shopPanel;


    public int idem1id;
    public int item2id;
    public int item3id;

    private int amountSlots;
    private int slotsChecked;
    private bool transactionDone;


    void Start()
    {
        shopPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void buyItem(int itemId)
    {
        amountSlots = inventoryPlayer.transform.GetChild(1).childCount;
        transactionDone = false;
        slotsChecked = 0;

        foreach (Transform child in inventoryPlayer.transform.GetChild(1))
        {
            if (child.childCount == 0)
            {
                inventoryPlayer.addItemToInventory(itemId);
                transactionDone = true;
                break;
            }
            slotsChecked++;
        }

        if ((slotsChecked == amountSlots) && (!transactionDone))
        {
            print("Plus de place dans l'inventaire !");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            shopPanel.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            shopPanel.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }*/
}

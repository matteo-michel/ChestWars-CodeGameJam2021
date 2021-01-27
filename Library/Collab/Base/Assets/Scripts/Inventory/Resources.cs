using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;
using TMPro;

public class Resources : NetworkBehaviour
{
    PlayerController playerController;

    public TextMeshProUGUI countWoodText;
    public TextMeshProUGUI countStoneText;


    private int wood = 100;
    private int stone = 100;

    void Update()
    {
        if(countWoodText != null)
            countWoodText.text = wood.ToString();
        if(countStoneText != null)
            countStoneText.text = stone.ToString();
    }

    public void AddResource(CollectType type, int amount)
    {
        switch(type)
        {
            case CollectType.Wood:
                wood += amount;
                playerController.audioSource.PlayOneShot((AudioClip)UnityEngine.Resources.Load("Audio/FX/bois"));
                break;
            case CollectType.Stone:
                stone += amount;        
                break;
        }

        Debug.Log("Ajout de " + amount + "x - " + type);
    }

    public void RemoveResource(CollectType type, int amount)
    {
        switch(type)
        {
            case CollectType.Wood:
                if (wood - amount >= 0) wood -= amount;
                break;
            case CollectType.Stone:
                if (stone - amount >= 0) stone -= amount;
                break;
        }
    }

    public bool TryBuySword()
    {
        if(wood >= 8 && stone >= 23)
        {
            wood -= 8;
            stone -= 23;
            playerController.unlockMap();
            return true;
        }
        return false;
    }

    public bool TryBuyBoat()
    {
        if(wood >= 34)
        {
            wood -= 34;
            return true;
        }
        Debug.Log("wood" + wood);
        Debug.Log("---------------------");
        return false;
    }
}

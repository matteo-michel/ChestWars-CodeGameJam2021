using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Resources : NetworkBehaviour
{
    private int wood = 0;

    public void AddResource(CollectType type, int amount)
    {
        switch(type)
        {
            case CollectType.Wood:
                this.wood += amount;
                break;
        }

        Debug.Log("Ajout de " + amount + "x - " + type);
    }
}

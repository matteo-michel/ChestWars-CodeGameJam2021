using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Market : NetworkBehaviour
{
    public Transform viewport;
    public PauseMenu menu;
    public Resources client;
    public Vector3 spawnPosition;

    public void BuyBoat()
    {
        if(client != null)
        {
            Debug.Log("Client is " + client);
            if(client.TryBuyBoat())
            {
                GameObject boat = (Instantiate(UnityEngine.Resources.Load("Prefabs/BoatObject")) as GameObject);
                boat.transform.position = spawnPosition;
                NetworkServer.Spawn(boat);
                NetworkServer.Update();
                NetworkClient.Update();
                Debug.Log("Bateau acheté");
            }
        }
    }

    public void BuyMap()
    {
        if (client != null)
            client.TryBuySword();
    }

    public void ShowViewport()
    {
        viewport.gameObject.SetActive(true);
        menu.Pause();
    }

    public void CloseViewport()
    {
        viewport.gameObject.SetActive(false);
        menu.Resume();
    }
}

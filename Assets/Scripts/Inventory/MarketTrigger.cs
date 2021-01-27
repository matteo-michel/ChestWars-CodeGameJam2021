using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class MarketTrigger : NetworkBehaviour
{
    public Market market;

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                market.client = other.GetComponent<Resources>();
                market.ShowViewport();
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                market.client = null;
                market.CloseViewport();
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }
}

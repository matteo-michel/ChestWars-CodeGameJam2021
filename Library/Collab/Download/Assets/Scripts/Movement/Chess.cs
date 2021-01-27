using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Chess : NetworkBehaviour
{

    private MeshRenderer affichage;

    void Start()
    {
        affichage = this.GetComponent<MeshRenderer>();
        affichage.enabled = false;
    }

    
    void Update()
    {
        if (Input.GetKey(KeyCode.G))
        {
            if (this.isLocalPlayer)
            {
                affichage.enabled = true;
                transform.parent = null;
            }
        }
    }
}

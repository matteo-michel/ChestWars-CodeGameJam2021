using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionCaller : MonoBehaviour
{

    private PlayerInteract playerInteract;

    private void Awake()
    {
        playerInteract = transform.parent.GetComponent<PlayerInteract>();
    }

    private void OnTriggerEnter(Collider other)
    {
        playerInteract.HandleTriggerEnter(other.transform);
    }

    private void OnTriggerExit(Collider other)
    {
        playerInteract.HandleTriggerExit(other.transform);
    }


    private void OnTriggerStay(Collider other)
    {
        playerInteract.HandleTriggerStay(other.transform);
    }
}

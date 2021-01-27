using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;

[RequireComponent(typeof(Animator))]
public class PlayerInteract : NetworkBehaviour
{

    public Transform inventoryPanel;
    public Transform interactPanel;
    private Animator animator;

    private bool canInteract = true;

    private void Start()
    {
        animator = GetComponent<PlayerController>().animator;
    }

    private void Update()
    {
        // INVENTORY
        if (inventoryPanel != null && Input.GetKeyDown(KeyCode.Escape) || (Input.GetKeyDown(KeyCode.E) && inventoryPanel.gameObject.activeInHierarchy))
        {
            inventoryPanel.gameObject.SetActive(false);
        }
        else if (inventoryPanel != null && Input.GetKeyDown(KeyCode.E))
        {
            inventoryPanel.gameObject.SetActive(true);
        }
    }

    public void HandleTriggerEnter(Transform other)
    {
        if (other.tag == "Collectible")
        {
            interactPanel.GetChild(0).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "F";
            interactPanel.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = "Appuyez pour collecter";
            interactPanel.gameObject.SetActive(true);
        }
    }

    public void HandleTriggerExit(Transform other)
    {
        if (other.tag == "Collectible")
        {
            interactPanel.gameObject.SetActive(false);
        } else if(other.tag == "Market")
        {
            interactPanel.gameObject.SetActive(false);
        }
    }

    IEnumerator Cooldown()
    {
        canInteract = false;
        yield return new WaitForSeconds(1.5f);
        canInteract = true;
    }

    public void HandleTriggerStay(Transform other)
    {
        if(other.tag == "Collectible")
        {
            RaycastHit hit;
            Vector3 dir = other.gameObject.transform.position - transform.position;
            if (Physics.Raycast(transform.position, dir, out hit))
            {
                interactPanel.transform.position = new Vector3(hit.point.x, transform.position.y + 2, hit.point.z);
                interactPanel.LookAt(transform);
                interactPanel.Rotate(new Vector3(0, 180, 0));
                Vector3 rot = interactPanel.rotation.eulerAngles;
                rot.x = Mathf.Clamp(rot.x, -20, 20);
                interactPanel.rotation = Quaternion.Euler(rot.x, rot.y, rot.z);
            }
            if(canInteract) { 
                if(Input.GetKeyDown(KeyCode.F))
                {
                    Collectable collectable = other.GetComponent<Collectable>();
                    collectable.Collect(GetComponent<Resources>());
                    animator.SetTrigger("hitWood");
                    StartCoroutine(Cooldown());
                }
            }
        }
    }
}

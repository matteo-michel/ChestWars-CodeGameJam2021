using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAttack : MonoBehaviour
{

    public PlayerController playerController;

    private bool isAttacking = false;

    private void OnTriggerStay(Collider other)
    {
        if (!isAttacking && other.gameObject != playerController.gameObject && other.tag == "Player")
        {
            Debug.Log("Ennemie");
            playerController.Attack(other.GetComponent<PlayerController>());
            StartCoroutine(Cooldown());
        }
    }

    IEnumerator Cooldown()
    {
        isAttacking = true;
        yield return new WaitForSeconds(2);
        isAttacking = false;
    }

}

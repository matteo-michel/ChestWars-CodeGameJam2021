using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;

    public PlayerController playerController;

    public void UpdateHealth()
    {
        slider.value = playerController.currentHealth / (float)playerController.maxHealth;
    }
}

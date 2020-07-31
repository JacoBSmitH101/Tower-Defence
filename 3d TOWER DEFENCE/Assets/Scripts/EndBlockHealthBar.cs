using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndBlockHealthBar : MonoBehaviour
{
    public Slider slider;

    private float maxHealth = GameMaster.maxHealth;
    private float currentHealth = GameMaster.baseHealth;

    private void Update()
    {
        //print(currentHealth);
        //print(maxHealth);
    }

    public void SetHealth(int health)
    {
        slider.value = health / maxHealth;
    }
}

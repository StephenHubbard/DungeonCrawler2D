using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int currentHealth;
    [SerializeField] private int startingHealth = 10;
    [SerializeField] private Slider healthSlider;

    private void Start()
    {
        currentHealth = startingHealth;
    }

    private void Update()
    {
        UpdateHealthSliderValue();
    }

    private void UpdateHealthSliderValue()
    {
        healthSlider.value = currentHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
            FindObjectOfType<TurnController>().isPlayerTurn = true;
        }
    }

}

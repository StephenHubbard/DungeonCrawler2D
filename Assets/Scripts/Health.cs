using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Health : MonoBehaviour
{
    [SerializeField] TMP_Text healthText;
    [SerializeField] private int currentHealth;
    [SerializeField] private int startingHealth = 10;

    private void Awake()
    {
    }

    private void Start()
    {
        currentHealth = startingHealth;
    }

    private void Update()
    {
        UpdateHeartsUI();
    }

    private void UpdateHeartsUI()
    {
        healthText.text = "Health: " + currentHealth.ToString();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
    }
}

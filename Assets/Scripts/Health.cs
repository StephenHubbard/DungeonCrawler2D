using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int startingHearts;
    [SerializeField] private int currentHearts;
    [SerializeField] private Transform heartsContainer;

    private void Awake()
    {
        currentHearts = startingHearts;
    }

    private void Start()
    {
        UpdateHeartsUI();
    }

    private void UpdateHeartsUI()
    {
        for (int i = 0; i < heartsContainer.childCount; i++)
        {
            heartsContainer.GetChild(i).gameObject.SetActive(false);
        }

        for (int i = 0; i < currentHearts; i++)
        {
            heartsContainer.GetChild(i).gameObject.SetActive(true);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHearts -= damage;

        UpdateHeartsUI();
    }
}

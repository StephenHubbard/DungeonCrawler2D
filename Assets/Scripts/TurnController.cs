using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TurnController : MonoBehaviour
{
    [SerializeField] public bool isPlayerTurn = true;
    [SerializeField] private TMP_Text playerTurnText;

    private PlayerMovement player;

    private void Awake()
    {
        player = FindObjectOfType<PlayerMovement>();
    }

    private void Update()
    {
        UpdatePlayerTurnText();
    }

    private void UpdatePlayerTurnText()
    {
        if (isPlayerTurn)
        {
            playerTurnText.text = "Player Turn: True";
        }
        if (!isPlayerTurn)
        {
            playerTurnText.text = "Player Turn: False";
        }
    }

    public void PlayerTurnTrue()
    {
        isPlayerTurn = true;
    }

    public void PlayerTurnFalse()
    {
        isPlayerTurn = false;
    }

    public void MoveEnemiesButton()
    {
        StartCoroutine(player.MoveEnemiesCo());
    }

    
}

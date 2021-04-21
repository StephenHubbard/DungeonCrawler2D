using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Transform movePoint;
    [SerializeField] LayerMask whatStopsMovement;
    [SerializeField] LayerMask playerMask;
    [SerializeField] private int powerLevel = 1;
    [SerializeField] GameObject targetBorder;

    private PlayerMovement player;
    private TurnController turnController;
    private Health health;

    [SerializeField] private float playerAggroDistance = 2.1f;
    [SerializeField] private bool isPlayerClose = false;

    [SerializeField] float distanceToHero;

    [SerializeField] Vector2 playerPos;
    [SerializeField] Vector2 enemyPos;

    [SerializeField] public bool isEnemyTurn = false;


    private void Awake()
    {
        player = FindObjectOfType<PlayerMovement>();
        turnController = FindObjectOfType<TurnController>();
        health = FindObjectOfType<Health>();
    }

    private void Start()
    {
        movePoint.parent = null;
        EnemyNextTurnLocation();
    }

    private void Update()
    {
        DetectIfPlayerClose();
    }

    public void EnemyNextTurnLocation()
    {
        int randomDir = Random.Range(0, 2) * 2 - 1;
        int randomAxis = Random.Range(0, 2) * 2 - 1;

        Vector3 originalPos = transform.position;

        if (randomAxis < 0)
        {
            if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(randomDir, 0f, 0f), .2f, whatStopsMovement))
            {
                movePoint.position = new Vector2(movePoint.position.x + randomDir, movePoint.position.y);
                if (player.targetEnemy == gameObject)
                {
                    player.AssignTargetEnemy(null);
                }
            }
        }
        else if (randomAxis > 0)
        {
            if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, randomDir, 0f), .2f, whatStopsMovement))
            {
                movePoint.position = new Vector2(movePoint.position.x, movePoint.position.y + randomDir);
                if (player.targetEnemy == gameObject)
                {
                    player.AssignTargetEnemy(null);
                }
            }
        }

        Vector3 moveDirection = movePoint.transform.position - originalPos;
        if (moveDirection != Vector3.zero)
        {
            movePoint.gameObject.SetActive(true);
            float angle = Mathf.Atan2(moveDirection.x, moveDirection.y) * Mathf.Rad2Deg;
            movePoint.transform.rotation = Quaternion.AngleAxis(angle, Vector3.back);
        }
        else
        {
            movePoint.gameObject.SetActive(false);
        }

        turnController.isPlayerTurn = true;
    }


    public void EnemyMove()
    {
        transform.position = movePoint.position;
        EnemyNextTurnLocation();
    }

    private void AttackPlayer()
    {
        player.AssignTargetEnemy(gameObject);
        health.TakeDamage(powerLevel);
    }

    private void DetectIfPlayerClose()
    {
        distanceToHero = Vector3.Distance(transform.position, player.transform.position);

        if (distanceToHero < playerAggroDistance)
        {
            isPlayerClose = true;
        }
        else
        {
            isPlayerClose = false;
        }

        playerPos = player.transform.position;
        enemyPos = transform.position;
    }
}

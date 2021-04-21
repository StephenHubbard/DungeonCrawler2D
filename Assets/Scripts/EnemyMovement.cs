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
    }

    private void Update()
    {
        DetectIfPlayerClose();
    }


    public void EnemyMove()
    {
        int randomDir = Random.Range(0, 2) * 2 - 1;
        int randomAxis = Random.Range(0, 2) * 2 - 1;

        if (randomAxis < 0)
        {
            if (Physics2D.OverlapCircle(movePoint.position + new Vector3(randomDir, 0f, 0f), .2f, playerMask))
            {
                player.AssignTargetEnemy(gameObject);
                health.TakeDamage(powerLevel);
                return;
            }

            else if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(randomDir, 0f, 0f), .2f, whatStopsMovement))
            {
                movePoint.position += new Vector3(randomDir, 0f, 0f);
                if (player.targetEnemy == gameObject)
                {
                    player.AssignTargetEnemy(null);
                }
            }
        }
        else if (randomAxis > 0)
        {
            if (Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, randomDir, 0f), .2f, playerMask))
            {
                player.AssignTargetEnemy(gameObject);
                health.TakeDamage(powerLevel);
                return;
            }

            else if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, randomDir, 0f), .2f, whatStopsMovement))
            {
                movePoint.position += new Vector3(0f, randomDir, 0f);
                if (player.targetEnemy == gameObject)
                {
                    player.AssignTargetEnemy(null);
                }
            }
        }

        transform.position = movePoint.position;
        turnController.isPlayerTurn = true;
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Player"))
    //    {
    //        collision.GetComponent<Health>().TakeDamage(powerLevel);
    //    }
    //}

    private void DetectIfPlayerClose()
    {
        distanceToHero = Vector3.Distance(transform.position, player.transform.position);
        //print(distanceToHero);

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

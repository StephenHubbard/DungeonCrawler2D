using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private int playerPower = 1;
    [SerializeField] private Transform movePoint;
    [SerializeField] LayerMask whatStopsMovement;
    [SerializeField] LayerMask enemyMask;
    [SerializeField] GameObject targetBorder;
    [SerializeField] Animator animator;

    private TurnController turnController;

    [SerializeField] public GameObject targetEnemy;


    private void Awake()
    {
        turnController = FindObjectOfType<TurnController>();
    }

    private void Start()
    {
        movePoint.parent = null;
    }

    private void Update()
    {
        TargetBorderActive();
        Move();
    }

    private void TargetBorderActive()
    {
        if (targetEnemy != null)
        {
            targetBorder.SetActive(true);
            targetBorder.transform.position = targetEnemy.transform.position;
        }
        else
        {
            targetBorder.SetActive(false);
        }
    }

    public void AssignTargetEnemy(GameObject enemy)
    {
        targetEnemy = enemy;
    }

    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, movePoint.position) <= .05f)
        {
            if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f && turnController.isPlayerTurn)
            {
                Collider2D[] result = new Collider2D[3];

                if (Physics2D.OverlapCircle(movePoint.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f), .2f, enemyMask))
                {
                    Physics2D.OverlapCircle(movePoint.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f), .2f, new ContactFilter2D(), result);
                    targetEnemy = result[0].gameObject;
                    turnController.isPlayerTurn = false;
                    animator.SetTrigger("SwordAttack");
                    targetEnemy.GetComponent<EnemyHealth>().TakeDamage(playerPower);
                    MoveEnemiesCo();
                    return;
                }

                if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f), .2f, whatStopsMovement))
                {
                    movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);
                    turnController.isPlayerTurn = false;
                    targetEnemy = null;
                    MoveEnemiesCo();
                }
            }
            else if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f && turnController.isPlayerTurn)
            {
                Collider2D[] result = new Collider2D[3];

                if (Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f), .2f, enemyMask))
                {
                    Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f), .2f, new ContactFilter2D(), result);
                    targetEnemy = result[0].gameObject;
                    turnController.isPlayerTurn = false;
                    animator.SetTrigger("SwordAttack");
                    targetEnemy.GetComponent<EnemyHealth>().TakeDamage(playerPower);
                    MoveEnemiesCo();
                    return;
                }

                if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f), .2f, whatStopsMovement))
                {
                    movePoint.position += new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f);
                    turnController.isPlayerTurn = false;
                    targetEnemy = null;
                    MoveEnemiesCo();
                }
            }
        }
    }


    public void MoveEnemiesCo()
    {
        var allEnemies = FindObjectsOfType<EnemyMovement>();

        foreach (var enemy in allEnemies)
        {
            enemy.EnemyMove();
        }
    }


}

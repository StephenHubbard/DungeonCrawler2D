using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Transform movePoint;
    [SerializeField] LayerMask whatStopsMovement;
    [SerializeField] private int powerLevel = 1;

    private PlayerMovement player;

    public bool isEnemyTurn = false;

    [SerializeField] private float playerAggroDistance = 2.1f;
    [SerializeField] private bool isPlayerClose = false;

    [SerializeField] float distanceToHero;

    private void Awake()
    {
        player = FindObjectOfType<PlayerMovement>();
    }

    private void Start()
    {
        movePoint.parent = null;
    }

    private void Update()
    {
        Move();
        DetectIfPlayerClose();
    }

    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);

        if (isEnemyTurn)
        {
            

            if (isPlayerClose)
            {
                Vector3 relativePos = player.transform.position - transform.position;

                bool isAbove = Vector3.Dot(transform.up, relativePos) > 0.0f;    // Otherwise is below
                bool isAtTheRight = Vector3.Dot(transform.right, relativePos) > 0.0f;    // Otherwise is at the left

                print("isAbove" + isAbove);
                print("isRight" + isAtTheRight);

                if (isAbove)
                {
                    if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, 1f, 0f), .2f, whatStopsMovement))
                    {
                        movePoint.position += new Vector3(0f, 1f, 0f);
                    }
                }
                else if (!isAbove)
                {
                    if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, -1f, 0f), .2f, whatStopsMovement))
                    {
                        movePoint.position += new Vector3(0f, -1f, 0f);
                    }
                }
                else if(isAtTheRight)
                {
                    if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(1f, 0f, 0f), .2f, whatStopsMovement))
                    {
                        movePoint.position += new Vector3(1f, 0f, 0f);
                    }
                }
                else if(!isAtTheRight)
                {
                    if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(-1f, 0f, 0f), .2f, whatStopsMovement))
                    {
                        movePoint.position += new Vector3(-1f, 0f, 0f);
                    }
                }

                isEnemyTurn = false;
                return;
            }

            if (Vector3.Distance(transform.position, movePoint.position) <= .05f)
            {
                int randomDir = Random.Range(-1, 2);
                int randomAxis = Random.Range(-1, 2);

                if (randomAxis <= 0)
                {
                    if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(randomDir, 0f, 0f), .2f, whatStopsMovement))
                    {
                        movePoint.position += new Vector3(randomDir, 0f, 0f);
                    }
                }
                else if (randomAxis > 0)
                {
                    if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, randomDir, 0f), .2f, whatStopsMovement))
                    {
                        movePoint.position += new Vector3(0f, randomDir, 0f);
                    }
                }

                isEnemyTurn = false;

            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.GetComponent<Health>().TakeDamage(powerLevel);
        }
    }

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
    }
}

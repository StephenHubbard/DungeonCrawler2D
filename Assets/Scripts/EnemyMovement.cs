using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Transform movePoint;
    [SerializeField] LayerMask whatStopsMovement;
    [SerializeField] private int powerLevel = 1;

    private void Start()
    {
        movePoint.parent = null;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);

        int randomDir = Random.Range(-1, 2);
        int randomAxis = Random.Range(-1, 2);

        if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f || Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)
        {
            if (Vector3.Distance(transform.position, movePoint.position) <= .05f)
            {
                if (randomAxis < 0)
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
}

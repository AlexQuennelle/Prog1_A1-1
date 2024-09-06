using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float targetRad = 1f;
    [SerializeField] float aggroRange = 5f;
    [SerializeField] Transform player;
    Vector2 targetPos;
    Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        targetPos = (new Vector2(Random.value * 2 - 1, Random.value * 2 - 1)) + (Vector2)transform.position;
    }
    private void Update()
    {
        transform.up = ((Vector3)targetPos - transform.position).normalized;
        if (Vector2.Distance(targetPos, transform.position) > targetRad)
        {
            rb.AddForce(transform.up * moveSpeed);
        }
        else
        {
            targetPos = (new Vector2(Random.value * 2 - 1, Random.value * 2 - 1) * 10) + (Vector2)transform.position;
        }
        if(Vector3.Distance(transform.position, player.position) < aggroRange)
        {
            targetPos = ((player.position - transform.position) * 2) + transform.position;
        }
        Wrapscreen();
    }
    void Wrapscreen()
    {
        Vector2 newPos = new Vector2(((transform.position.x + 48) % 32) - 16, ((transform.position.y + 27) % 18) - 9);
        transform.position = newPos;

        targetPos = new Vector2(targetPos.x + (Mathf.Round((transform.position.x - targetPos.x) / 32) * 32), targetPos.y + (Mathf.Round((transform.position.y - targetPos.y) / 18) * 18));
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(targetPos, 0.1f);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float rotateSpeed = 1f;
    Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            rb.AddForce(transform.up *  moveSpeed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            rb.AddForce(transform.up * -moveSpeed);
        }

        if (Input.GetKey(KeyCode.A))
        {
            rb.AddTorque(rotateSpeed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            rb.AddTorque(-rotateSpeed);
        }
        Wrapscreen();
    }
    void Wrapscreen()
    {
        Vector2 newPos = new Vector2(((transform.position.x + 48) % 32) - 16, ((transform.position.y + 27) % 18) - 9);
        transform.position = newPos;
    }
}

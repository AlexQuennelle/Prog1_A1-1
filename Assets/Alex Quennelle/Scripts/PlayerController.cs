using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//class for handling player input and movement.
public class PlayerController : MonoBehaviour
{
	//float values that  control the speed at which the player will move and rotate.
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float rotateSpeed = 1f;

	[SerializeField] Health health;

	//stores the player's rigidbody component to apply physics forces to
    Rigidbody2D rb;
	//method that is called at the start of the game
    private void Start()
    {
		//fetches the attached Rigidbody2D component and stores it in the rb variable for future reference
        rb = GetComponent<Rigidbody2D>();
    }
	//method that is called once every frame
    private void Update()
    {
		//tests if the player is pressing the W or S keys, and adds a force in the up direction of the player (which means forward in 2D) with a magnitude of moveSpeed for W and the inverse of moveSpeed for S
        if (Input.GetKey(KeyCode.W))
        {
            rb.AddForce(transform.up *  moveSpeed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            rb.AddForce(transform.up * -moveSpeed);
        }

		//tests if the player is pressing the A or D keys, and adds a Torque(rotational) force of rotateSpeed for A, and the inverse of rotateSpeed for D
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
	//adjusts the player's position to always be within the screen bounds by wrapping it to the oposite edge when it leaves the screen
    void Wrapscreen()
    {
		//adds 1.5 times the screen width to the player's x position and takes the remainder.Then subtracts half the screen width. these steps ensure the remainder operator behaves as expected when the player goes into the negative coordinates.
		//also does the same with the y position and the height of the screen.
        Vector2 newPos = new Vector2(((transform.position.x + 48) % 32) - 16, ((transform.position.y + 27) % 18) - 9);
        transform.position = newPos;
    }
	//method that is called when the collider attached to the current game object collides with another
	private void OnCollisionEnter2D(Collision2D collision)
	{
		//calls the method to take damage on the health object attached to the player controller
		health.TakeDamage();
	}
}

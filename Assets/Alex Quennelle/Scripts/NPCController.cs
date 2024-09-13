using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//class for controlling an enemy NPC. Acts as the brain of the NPC
public class NPCController : MonoBehaviour
{
	//float value for controlling how fast the NPC moves
    [SerializeField] float moveSpeed = 5f;
	//value that controls how close the NPC needs to get to it's target position before picking a new one
    [SerializeField] float targetRad = 1f;
	//radius in which the NPC will detect and target the player
    [SerializeField] float aggroRange = 5f;
	//reference to the player's transform component so the target position can be set to the player's when within aggroRange
    [SerializeField] Transform player;
	//the target position the NPC is trying to reach
    Vector2 targetPos;
	//reference to the NPC's rigidbody to apply physics forces too
    Rigidbody2D rb;
	//method that is called once at the start of the game
    private void Start()
    {
		//fetches and stores the NPC's attached Rigidbody2D component
        rb = GetComponent<Rigidbody2D>();

		//initializes the target position to a random point on a unit circle around the NPC
        targetPos = (new Vector2(Random.value * 2 - 1, Random.value * 2 - 1)) + (Vector2)transform.position;
    }
	//method that is called once every frame
    private void Update()
    {
		//sets the NPC's up vector to be one that points from its current position to the target position
		//this has the effect of rotating the NPC to point towards its target position
        transform.up = ((Vector3)targetPos - transform.position).normalized;

		//takes the distance between the current position and the current position, and if that distance is smaller than the target radius, picks a new random position along a circle with a radius of 10 units centered on the current position
        if (Vector2.Distance(targetPos, transform.position) < targetRad)
        {
            targetPos = (new Vector2(Random.value * 2 - 1, Random.value * 2 - 1) * 10) + (Vector2)transform.position;
        }
		//takes the distance to the player, and if that distance is smaller than the aggro range, sets the target position to a point 2 times as far, along the vector that points from the NPC to the player
		//the reason to do this instead of just setting the target to the player's position is because doing it that way causes issues with the screen wrapping
        if (Vector3.Distance(transform.position, player.position) < aggroRange)
        {
            targetPos = ((player.position - transform.position) * 2) + transform.position;
        }
		//adds a force to the NPC int the up direction with a magnitude equal to the moveSpeed value
		rb.AddForce(transform.up * moveSpeed);
		Wrapscreen();
    }
	//adjusts the NPC's position to always be within the screen bounds by wrapping it to the oposite edge when it leaves the screen
    void Wrapscreen()
    {
		//adds 1.5 times the screen width to the NPC's x position and takes the remainder.Then subtracts half the screen width. these steps ensure the remainder operator behaves as expected when the player goes into the negative coordinates.
		//also does the same with the y position and the height of the screen.
        Vector2 newPos = new Vector2(((transform.position.x + 48) % 32) - 16, ((transform.position.y + 27) % 18) - 9);
        transform.position = newPos;

		//get the distance to the target position on each axis, and divide them by the screen width and height respectively. finally, round the result to the nearest integer
		//this gives a modifier value which is 1 when the target position is greater than 1 screen away on the respective axis, and 0 when the target position is less than 1 screen away
		int xMod = (int)Mathf.Round((transform.position.x - targetPos.x) / 32);
		int yMod = (int)Mathf.Round((transform.position.y - targetPos.y) / 18);
		//multiply the modifier values by the screen's size in their respective axis, and add that to the target position
		//this moves the target position to be closer to the NPC when it is too far away, keeping its relative position from before its position was adjusted
        targetPos = new Vector2(targetPos.x + (xMod * 32), targetPos.y + (yMod * 18));
    }
	//method that is called once every frame in the scene view when Unity draws the gizmos
    private void OnDrawGizmos()
    {
		//draws a green sphere at the taget position in the scene view only
		//meant for deubgging and visualisation
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(targetPos, 0.1f);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//class for handling health and damage
//added the serializable tag to allow this class to be visible in the editor when it is used as a variable in a monobehaviour class
[System.Serializable]
public class Health
{
	//variable to store the current hit points
	//this variable is initialized to the max value at the start of the game
	[SerializeField] int hitPoints = 1;
	//unity event that is invoked when the attached entity dies
	public UnityEvent onDeath = new UnityEvent();
	//public method for taking damage
	//all damage related logic is contained in this method
	public void TakeDamage()
	{
		//decrease the hitpoints variable by 1, then test if the new value is 0
		hitPoints--;
		if (hitPoints == 0)
		{
			//if it is 0, invoke the onDeath event
			onDeath.Invoke();
		}
	}
}

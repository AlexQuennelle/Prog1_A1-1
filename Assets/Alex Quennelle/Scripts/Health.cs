using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Health
{
	[SerializeField] int hitPoints = 1;
	public UnityEvent onDeath = new UnityEvent();
	public void TakeDammage()
	{
		hitPoints -= 1;
		if (hitPoints == 0)
		{
			onDeath.Invoke();
		}
	}
}

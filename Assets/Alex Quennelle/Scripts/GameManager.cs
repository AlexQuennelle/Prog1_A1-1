using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	//function to reload the scene
	public void Reload()
	{
		//loads the first scene in the project
		//since there is only one scene, this effectively reloads the current scene
		SceneManager.LoadScene(0);
	}
}

using UnityEngine;
using System.Collections;

public class Controltogame : MonoBehaviour {
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
		{
			Debug.Log("Controles indo game");
			
			Application.LoadLevel("Game");
		}
	}
}

using UnityEngine;
using System.Collections;

public class Menu2 : MonoBehaviour {

	// Update is called once per frame
	void Update () 
	{
		if(Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
		{
			Debug.Log("Control to game");

			Application.LoadLevel("Game");
		}
	}
}

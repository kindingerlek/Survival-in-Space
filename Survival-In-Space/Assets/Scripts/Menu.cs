using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {

	// Update is called once per frame
	void Update () 
	{
		if(Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
		{
			Debug.Log("menu indo para controles");

			Application.LoadLevel("Controles");
		}
	}
}

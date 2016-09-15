using UnityEngine;
using System.Collections;

public class Power_upTEST : MonoBehaviour {

	PlayerBehaviour player;
	int type;

	// Use this for initialization
	void Start () {
		player = (PlayerBehaviour) GameObject.FindObjectOfType<PlayerBehaviour> ();
		type = Random.Range (1, 4);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision target)
	{
		if(target.gameObject.tag == "Player")
		{
			if(type == 1)
				player.IncreaseMaxSpeed(50f);
			else if (type == 2)
				player.DecreaseFireDelay(30f);
			else
				player.AddScore(Random.Range(5,15));

			Destroy(gameObject);
		}

	}

}

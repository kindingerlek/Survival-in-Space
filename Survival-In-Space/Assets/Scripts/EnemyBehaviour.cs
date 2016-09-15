using UnityEngine;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour {

	public float DistanceLimit;
	public float fireRate;
	public float MaxSpeed;
	public float acceleration;

	private float nextFire;

	public GameObject TopDownCamera;
	public GameObject particlesEffect;
	public GameObject ShootSpawnPoint;
	public GameObject BulletType;

	private GameObject player;
	private GameController gameController;

	private Vector3 lastForwardVector;

	// Use this for initialization
	void Start () {
		/*GameObject gameControllerObject = GameObject.FindGameObjectWithTag ("GameController");
		if (gameControllerObject != null)
		{
			gameController = gameControllerObject.GetComponent <GameController>();
		}
		if (gameController == null)
		{
			Debug.Log ("Cannot find 'GameController' script");
		}

*/

		gameController = (GameController) GameObject.FindObjectOfType<GameController> ();

	}
	
	// Update is called once per frame
	void Update () {
		player = GameObject.FindGameObjectWithTag ("Player");
		if(player != null)
		{
			LookAtPlayer ();
			lastForwardVector = transform.forward;

			/*
			if
			(
				(this.renderer.isVisible && Vector3.Distance(transform.position, player.transform.position) < DistanceLimit) ||
				(Vector3.Distance(transform.position, player.transform.position) < DistanceLimit)
			)
				shoot ();*/

			if(Vector3.Distance(transform.position, player.transform.position) < DistanceLimit)
				shoot();
		}

		SetMaxSpeed ();
	}

	void SetMaxSpeed(){
		// Seta a velocidade maxima da nave com base no valor da variavel 'MaxSpeed'
		if(GetComponent<Rigidbody>().velocity.x > MaxSpeed)
			GetComponent<Rigidbody>().velocity = new Vector3(MaxSpeed, 0, GetComponent<Rigidbody>().velocity.z);
		if(GetComponent<Rigidbody>().velocity.z > MaxSpeed)
			GetComponent<Rigidbody>().velocity = new Vector3(GetComponent<Rigidbody>().velocity.x, 0, MaxSpeed);
		
		if(GetComponent<Rigidbody>().velocity.x < (MaxSpeed * -1f) )
			GetComponent<Rigidbody>().velocity = new Vector3((MaxSpeed * -1f), 0, GetComponent<Rigidbody>().velocity.z);
		if(GetComponent<Rigidbody>().velocity.z < (MaxSpeed * -1f))
			GetComponent<Rigidbody>().velocity = new Vector3(GetComponent<Rigidbody>().velocity.x, 0, (MaxSpeed * -1f));

		GetComponent<Rigidbody>().AddForce (transform.forward * acceleration);
		transform.forward = lastForwardVector;
	}

	void LookAtPlayer()
		//Aponta a frente para o 'player'
	{
		transform.LookAt (player.transform.position);
	}

	void shoot()
	{
		//Instancia o 'bullet' dentro do tempo estipulado por 'fireRate'
		if (Time.time > nextFire)
		{			
			nextFire = Time.time + fireRate;
			GameObject bullet = 
				Instantiate(BulletType,ShootSpawnPoint.transform.position,ShootSpawnPoint.transform.rotation) as GameObject;

			bullet.transform.GetComponent<Rigidbody>().velocity += this.GetComponent<Rigidbody>().velocity;
			//Atribui a velocidade inicial do 'bullet' a velocidade deste objeto
		}

	}

	void OnCollisionEnter(Collision target)
	{
		if(target.gameObject.tag == "bullet" || target.gameObject.tag == "Comet"){
			
			Destroy (gameObject);
			Instantiate (particlesEffect,transform.position, transform.rotation);
			gameController.AddScore(1);						
		}
	}
}

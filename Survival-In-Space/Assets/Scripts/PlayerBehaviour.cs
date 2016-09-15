using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerBehaviour : MonoBehaviour {
	public bool Imortal; 				// Torna o player imortal
	public float accelerationForce;		// Força de aceleraçao do player
	public float force;
	public float maxSpeed;				// Velocidade maxima que o player
	public float sensitivityX = 15F;	// Sensibilidade do Mouse no Eixo X
	public float fireDelay;
	
	public GameObject Instantiators; 	// Objeto que contem os instanciadore
	public GameObject explosionEffect;  // Efeito de explosao
	public GameObject TopDown;			// Camera TopDown
	public GameObject FirstCam;			// Camera FirstCam
	public GameObject ThirtCam;			// Camera ThirtCam
	public GameObject BulletType;		// Typo de Bullet
	public Transform shootSpawn;		// Ponto para instanciar os 'Bullets'
	public List<GameObject> foguetes;	// Efeito dos Foguetes
	
	private float nextFire;
	private bool isTopdownCamera;		// Verificador se a Camera e a TopDown
	private int ActivedCameras = 0 ;	// Contador para a camera ativa
	private bool dead = false;
	private GameController gameController;
	
	

	#region System
	void Start () {
		gameController = (GameController) GameObject.FindObjectOfType<GameController> ();
		
		isTopdownCamera = true;
	}// Use this for initialization
	void Update()	{
		Instantiators.transform.position = this.transform.position;
		if (Input.GetKeyDown (KeyCode.C)) {
			ActivedCameras += 1;
		}
		
		VerifyCamera ();
	}
	void FixedUpdate () 
	{
		transform.position = new Vector3 (transform.position.x, 0, transform.position.z);
		
		SetMaxSpeed ();
		
		//if(Input.GetAxis("Vertical") > 0)
		float acceleration = Input.GetAxis("Vertical");

		if ((Input.GetButton("Fire1") || Input.GetButton("Jump")) && Time.time > nextFire)
		{
			nextFire = Time.time + fireDelay;
			shoot ();
		}			
		else
		{
			GetComponent<Rigidbody>().AddForce (transform.forward * acceleration * accelerationForce);
			
			if(acceleration > 0)
			{
				// liga o objeto de particulas do foguete
				foreach (GameObject obj in foguetes)
				{
					obj.SetActive(true);
				}
			}
			else
			{
				// desliga o objeto
				foreach (GameObject obj in foguetes)
				{
					obj.SetActive(false);
				}
			}
		}
	}
	#endregion	
	void OnCollisionEnter(Collision target)
	{
		if (dead) return;
		
		if(!Imortal){
			if (target.transform.tag == "bulletEnemy" || target.transform.tag == "Comets")
			{
				killmyself();
			}
			else if(target.transform.tag == "asteroids")
			{
				if(target.transform.localScale.x > 0.2f && (GetComponent<Rigidbody>().velocity - target.rigidbody.velocity).magnitude < 0.6 * maxSpeed)
				{
					killmyself();
				}
			}
		}
		
	}
	void SetMaxSpeed(){
		if(GetComponent<Rigidbody>().velocity.x > maxSpeed)
			GetComponent<Rigidbody>().velocity = new Vector3(maxSpeed, 0, GetComponent<Rigidbody>().velocity.z);
		if(GetComponent<Rigidbody>().velocity.z > maxSpeed)
			GetComponent<Rigidbody>().velocity = new Vector3(GetComponent<Rigidbody>().velocity.x, 0, maxSpeed);
		
		if(GetComponent<Rigidbody>().velocity.x < (maxSpeed * -1f) )
			GetComponent<Rigidbody>().velocity = new Vector3((maxSpeed * -1f), 0, GetComponent<Rigidbody>().velocity.z);
		if(GetComponent<Rigidbody>().velocity.z < (maxSpeed * -1f))
			GetComponent<Rigidbody>().velocity = new Vector3(GetComponent<Rigidbody>().velocity.x, 0, (maxSpeed * -1f));
	}
	#region CameraController
	void OtherLookMouse()
	{
		transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityX, 0);
		if (GetComponent<Rigidbody>())
			GetComponent<Rigidbody>().freezeRotation = true;
	}	
	void TopDownLookMouse()
	{
		Plane PlayerPlane = new Plane (Vector3.up, transform.position);
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		float hitdisk = 0f;
		
		if(PlayerPlane.Raycast (ray, out hitdisk))
		{
			Vector3 targetPoint = ray.GetPoint(hitdisk);
			
			Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
			
			transform.rotation = targetRotation;
		}
	}	
	void VerifyCamera()
	{
		if(isTopdownCamera)
			TopDownLookMouse();
		else
			OtherLookMouse();
		
		if(ActivedCameras > 2)
			ActivedCameras = 0;
		if(ActivedCameras == 0)
		{
			isTopdownCamera = true;
			
			
			TopDown.gameObject.GetComponent<Camera>().enabled = true;
			FirstCam.gameObject.GetComponent<Camera>().enabled = false;
			ThirtCam.gameObject.GetComponent<Camera>().enabled = false;
			
		}
		else if(ActivedCameras == 1)
		{
			isTopdownCamera = false;
			
			TopDown.gameObject.GetComponent<Camera>().enabled = false;
			FirstCam.gameObject.GetComponent<Camera>().enabled = true;
			ThirtCam.gameObject.GetComponent<Camera>().enabled = false;
			
		}
		else
		{
			isTopdownCamera = false;
			
			TopDown.gameObject.GetComponent<Camera>().enabled = false;
			FirstCam.gameObject.GetComponent<Camera>().enabled = false;
			ThirtCam.gameObject.GetComponent<Camera>().enabled = true;
			
		}
		
		Cursor.visible = isTopdownCamera;
	}
	#endregion
	void killmyself()
	{
		Debug.Log ("Estou morto");
		
		Instantiate(explosionEffect, transform.position, transform.rotation);
		
		this.ActivedCameras = 0; //Muda a camera para a TopDown quando morrer
		VerifyCamera();
		
		this.gameObject.SetActive(false); // Apenas desativa o player
		gameController.Dead = true;
	}
	void shoot()
	{
		GameObject bullet = Instantiate(BulletType,shootSpawn.position,shootSpawn.rotation) as GameObject;
		bullet.transform.GetComponent<Rigidbody>().velocity = this.GetComponent<Rigidbody>().velocity;
		
		//audio.Play ();
	}

	#region Sets
	public void IncreaseMaxSpeed(float percent)
	{
			this.maxSpeed *= 1+(percent/100);
	}

	public void DecreaseFireDelay(float percent)
	{
			this.fireDelay *= 1-(percent/100);
	}
	public void AddScore(int value)
	{
		gameController.AddScore (value);
	}
	#endregion
}
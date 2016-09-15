using UnityEngine;
using System.Collections;

public class AsteroidsBehaviour : MonoBehaviour {
	public GameObject parent;

	public GameObject particlesEffect; // Efeito de explosao
	public AudioClip explosionAudioSource;
	//Intervalo numerico para adicionar força inicial
	public int minForce; 
	public int maxForce;

	public int numChildAsteroids = 2; // Numeros de Asteroids gerados apartir deste

	Vector3 torque;
	Vector3 force;
	int factor;

	// Use this for initialization
	void Start () {
		// Gera uma força aleatoria e um Torque aleatorio a este objeto

		parent = GameObject.Find ("Asteroids");

		factor = Random.Range (1, 10) * 10;

		torque.x = Random.Range (-50, 50);
		torque.y = Random.Range (-50, 50);
		torque.z = Random.Range (-50, 50);

		force.x = (Random.Range (minForce, maxForce) * factor) * randMultiply();
		force.y = 0;
		force.z = (Random.Range (minForce, maxForce) * factor) * randMultiply();


		GetComponent<Rigidbody>().AddForce (force);
		GetComponent<Rigidbody>().AddTorque (torque);


	}

	int randMultiply()
	{
		int k;
		do{
		k = Random.Range (-2, 2);
		}while(k==0);
		return k;
	}


	void Update()
	{
		// Mantem a posiçao de Y sempre em 0
		transform.position = new Vector3 (transform.position.x, 0, transform.position.z);
	}

	void InstantiateExplosion()
	{
		GameObject explosion = Instantiate (particlesEffect,transform.position, transform.rotation) as GameObject;

		explosion.GetComponent<ParticleEmitter>().minSize *= (transform.localScale.magnitude)/0.4f;
		explosion.GetComponent<ParticleEmitter>().maxSize *= (transform.localScale.magnitude)/0.4f;
		explosion.GetComponent<ParticleEmitter>().minEnergy *= (transform.localScale.magnitude)/0.4f;
		explosion.GetComponent<ParticleEmitter>().maxEnergy *= (transform.localScale.magnitude)/0.4f;
		explosion.GetComponent<ParticleEmitter>().minEmission *= (transform.localScale.magnitude)/0.4f;
		explosion.GetComponent<ParticleEmitter>().maxEmission *= (transform.localScale.magnitude)/0.4f;

		explosion.GetComponent<ParticleEmitter>().rndVelocity =
			new Vector3(
				(explosion.GetComponent<ParticleEmitter>().rndVelocity.x * (explosion.GetComponent<ParticleEmitter>().rndVelocity.x)/0.4f),
				(explosion.GetComponent<ParticleEmitter>().rndVelocity.y * (explosion.GetComponent<ParticleEmitter>().rndVelocity.y)/0.4f),
				(explosion.GetComponent<ParticleEmitter>().rndVelocity.z * (explosion.GetComponent<ParticleEmitter>().rndVelocity.z)/0.4f)
				);

		explosion.GetComponent<AudioSource>().volume *= (transform.localScale.magnitude)/0.4f;
		//AudioSource.PlayClipAtPoint(explosionAudioSource, transform.position);
	}

	void OnCollisionEnter(Collision target)
	{
		if(target.gameObject.tag == "bullet" ||
		   target.gameObject.tag == "Comet" ||
		   target.gameObject.tag == "bulletEnemy"){

			Destroy (gameObject);

			InstantiateExplosion();

			if(this.gameObject.transform.localScale.x > 0.1F)
			{
				//Verifica o Tamanho deste Objeto, e caso seja maior que 0.05, instancia asteroides menores
				GameObject child;

				gameObject.transform.localScale *=  0.5F;
				gameObject.GetComponent<Rigidbody>().mass *= 0.5F;

				for(int i=0; i<numChildAsteroids;i++)
				{
					// Arrumar esta seçao de codigo

					int x = Random.Range(-1,1)/25;
					int z = Random.Range(-1,1)/25;
					Vector3 spawnPosition = transform.position;
				
					//spawnPosition = new Vector3(spawnPosition.x + x, 0 , spawnPosition.z + z);


					child = Instantiate (this.gameObject,spawnPosition, Random.rotation) as GameObject;
					child.transform.parent = parent.transform;
					//child.rigidbody.AddForce(spawnPosition * 80);
				}				
			}			
		}
	}

}

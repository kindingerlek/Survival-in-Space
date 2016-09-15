using UnityEngine;
using System.Collections;

public class BulletEnemyBehaviour : MonoBehaviour {

	public string[] Filter = new string[1];
	public bool useFilter = false;

	public GameObject explosion;
	public float force;
	
	void Start () {
	transform.parent = GameObject.Find ("Bullets").transform;
		GetComponent<Rigidbody>().AddForce (transform.forward * force);
	GetComponent<AudioSource>().Play ();
	}
	
	void Update () {
		
	}
	
	void OnCollisionEnter(Collision target)
	{
	/*if (target.gameObject.tag != "bulletEnemy")
			Destroy (this.gameObject);
		//Destroy (target.gameObject);*/

		if(useFilter)
		{
			bool destroy = true;
			
			//Debug.Log ("Verificando Filtro");
			for(int i = 0 ; i < Filter.Length ; i++)
			{
				if(target.gameObject.tag == Filter[i])
				{
					destroy = false;
				}
			}
			if(destroy) Destroy (target.gameObject);
		}
		else
			Destroy (target.gameObject);
	}
     
}
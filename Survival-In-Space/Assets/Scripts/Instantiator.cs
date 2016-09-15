using UnityEngine;
using System.Collections;

public class Instantiator : MonoBehaviour {

	public int numObjects; // Numero de Objetos a serem instanciados
	public float scale;	   // Scala dos Objetos Instanciados
	public float radiusMin;   // Raio para o Spawn
	public float radiusMax;   // Raio para o Spawn
	private float radius;

	public GameObject parent;
	public GameObject[] ObjectsPrefab; // Array de Prefabs
	private GameObject[] ObjectsArray; // Array de Objetos

	// Use this for initialization
	void Start () {
		ObjectsArray = new GameObject[numObjects];
	}
	
	// Update is called once per frame
	void Update ()
	{
		/* Percorre todo o Array de objetos, verificando se ha alguma posiçao com conteudo nulo. Caso haja, instancia um
		 * objeto, de tipo variado conforme foi definido no Array de Prefabs.
		 */
		for(int i = 0 ; i < numObjects ; i++)
		{
			if(ObjectsArray[i] == null)
			{
				int index = Random.Range(0,ObjectsPrefab.Length);
				radius = Random.Range(radiusMin,radiusMax);

				Vector2 newPosition2D = 
					new Vector2(transform.position.x, transform.position.z) + Random.insideUnitCircle.normalized * radius;
				
				Vector3 newPosition = 
					new Vector3 (newPosition2D.x, 0 , newPosition2D.y);

				ObjectsArray[i] = Instantiate(ObjectsPrefab[index], newPosition, Random.rotation) as GameObject;
				ObjectsArray[i].transform.parent = parent.transform;

				if(scale != 0)
				{
					ObjectsArray[i].transform.localScale = new Vector3(scale,scale,scale);
				}
			}
		}
	}
}
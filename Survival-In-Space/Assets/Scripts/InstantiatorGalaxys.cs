using UnityEngine;
using System.Collections;

public class InstantiatorGalaxys : MonoBehaviour {

	//private GameObject[] galaxysArray;
	public GameObject[] galaxysPrefab;
	public int NumOfGalaxys; 

	// Use this for initialization
	void Start () {
				//galaxysArray = new GameObject[NumOfGalaxys];
				galaxysPrefab = new GameObject[1];

				for (int i = 0; i < NumOfGalaxys; i++) {
						GameObject galaxy;

						int high = Random.Range (1, 11) * 10;
						Vector3 scale;

						Vector2 Position2D = new Vector2 (0, 0);

						if (high <= 20) {
								scale = new Vector3 (50, 50, 1);
								Position2D = new Vector2 (transform.position.x, transform.position.z) + Random.insideUnitCircle.normalized * 35;
						} else if (high <= 30) {
								scale = new Vector3 (73, 73, 1);
								Position2D = new Vector2 (transform.position.x, transform.position.z) + Random.insideUnitCircle.normalized * 49;
						} else if (high <= 50) {
								scale = new Vector3 (96, 96, 1);
								Position2D = new Vector2 (transform.position.x, transform.position.z) + Random.insideUnitCircle.normalized * 63;
						} else if (high <= 70) {
								scale = new Vector3 (119, 119, 1);
								Position2D = new Vector2 (transform.position.x, transform.position.z) + Random.insideUnitCircle.normalized * 77;
						} else if (high <= 90) {
								scale = new Vector3 (142, 142, 1);
								Position2D = new Vector2 (transform.position.x, transform.position.z) + Random.insideUnitCircle.normalized * 92;
						} else { //if(high <= 110)
								scale = new Vector3 (165, 165, 1);
								Position2D = new Vector2 (transform.position.x, transform.position.z) + Random.insideUnitCircle.normalized * 113;
						}

						Vector3 Position = new Vector3 (Position2D.x, 0, Position2D.y);

						galaxy = Instantiate (galaxysPrefab [Random.Range (0, galaxysPrefab.Length - 1)], this.transform.position, Quaternion.identity) as GameObject;
						galaxy.transform.localScale = scale;
						galaxy.transform.position = Position;
				}
	}


	// Update is called once per frame
	void Update () {
	
	}
}

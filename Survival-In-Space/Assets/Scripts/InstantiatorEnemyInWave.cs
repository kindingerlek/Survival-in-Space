using UnityEngine;
using System.Collections;

public class InstantiatorEnemyInWave : MonoBehaviour {

	public int numWaves;
	public float radius;
	public GameObject EnemyPrefab;

	private int[] FibonacciArray;
	private GameObject[] EnemyArray;
	private int spawnNumber;
	public int WaveCounter;

	// Use this for initialization


	void Start () {
		EnemyArray = new GameObject[spawnNumber];
		FibonacciArray = new int[numWaves+1];

		FibonacciArray [0] = 0;
		FibonacciArray [1] = 1;

		for(int i=2; i < numWaves+1 ; i++)
		{
			FibonacciArray[i] = FibonacciArray[i-1] + FibonacciArray[i-2];

			Debug.Log("Fibonacci [" + i + "] = "  + FibonacciArray[i]);
		}
		WaveCounter = 1;
	}
	
	// Update is called once per frame
	void Update () {
		if(canContinue())
		{
			EnemyArray = new GameObject[spawnNumber];

			for(int i = 0; i < EnemyArray.Length;i++)
			{
				Vector2 newPosition2D = new Vector2(transform.position.x, transform.position.z) + Random.insideUnitCircle.normalized * radius;
				Vector3 newPosition = new Vector3 (newPosition2D.x, 0 , newPosition2D.y);

				EnemyArray[i] = Instantiate(EnemyPrefab, newPosition, this.transform.rotation) as GameObject;
			}

			Debug.Log("WaveCounter:" + WaveCounter);
			Debug.Log("spawnNumber:" + spawnNumber);

		}
	}

	bool canContinue()
	{
		for(int i = 0; i < EnemyArray.Length;i++)
		{
			if(EnemyArray[i] !=  null)
				return false;
		}
		WaveCounter++;
		if(WaveCounter >= numWaves)
			spawnNumber = FibonacciArray [numWaves];
		else			
			spawnNumber = FibonacciArray [WaveCounter-1];

		return true;
	}

}

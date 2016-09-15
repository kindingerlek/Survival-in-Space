using UnityEngine;
using System.Collections;

public class WaveControl : MonoBehaviour {
	public           int MaxEnemy; 		  // Maximo de Inimigos em uma Wave
	public 			 int WaveCounter;	  // Contador de Waves
	public		   float radius;
	public  GameObject[] EnemyPrefabs = new GameObject[0]; // Array com todos os Prefabs dos Inimigos
	public    GameObject parent;
	public    GameObject powerUpPrefab;


	private GameObject[] EnemyArray;      // Array com os Inimigos em jogo
	public        int[] EnemyArrayType;  // Array contendo o Tipo de Inimigos
	private         bool newEnemyType;
	private          int ActualSpawnType; // Tipo de inimigo a ser 'spawnado'
	
	
	void Start()
	{
		EnemyArrayType = new int[MaxEnemy];
		EnemyArray = new GameObject[MaxEnemy];
		
		ActualSpawnType = 0;
		WaveCounter = 0;

		for ( int i = 0; i < EnemyArrayType.Length ; i++)
		{
			EnemyArrayType[i] = -1;
		}
	}
	
	void Update()
	{
		if(CanContinue())
		{
			// Confirma se todo o Array de inimigos esta vazio, para entao atualizar a Wave..
			UpdateWave();
			SpawnEnemy();
		}
	}
	
	void UpdateWave()
	{
		/* Busca o primeiro conteudo do Array que seja diferente do 'ActualSpawnType', entao o substitue e para a busca.
		 * Caso nao haja substituiçao, a variavel 'newEnemyType' torna-se TRUE, e o valor de 'ActualSpawnType' aumenta 
		 * em um ponto, reiniciando o ciclo.
		 */


		WaveCounter++;
		newEnemyType = true;	

		for(int i = 0 ; i < EnemyArrayType.Length ; i++) 
		{
			if ( EnemyArrayType[i] != ActualSpawnType )
			{
				EnemyArrayType[i] = ActualSpawnType;
				newEnemyType = false;

				if(i == (MaxEnemy - 1) && newEnemyType == false)
					newEnemyType = true;

				break;
			}
		}
		
		if(newEnemyType)
			if(ActualSpawnType < EnemyPrefabs.Length)
			{
				ActualSpawnType++;
			}

		else if(ActualSpawnType < EnemyPrefabs.Length)
			/* Caso o valor de 'ActualSpawnType' seja maior que o numero de tipos de naves inimigas, isso significa que
			 * o jogador lutou contra todos os tipo de inimigos, sendo assim completou o jogo. E da load na tela de 'Win'.
			 */
			Application.LoadLevel("Win");

		Debug.Log("newEnemyType: " + newEnemyType);
		Debug.Log("ActualSpawnType: " + ActualSpawnType);
		//Debug.Log("");
	}
	
	void SpawnEnemy() // Instancia os inimigos em um Array, conforme seu tipo, definido pelo Array 'EnemyArrayType'
	{
		for(int i = 0 ; i < EnemyArray.Length ; i++)
		{ 
			if(EnemyArrayType[i] < 0 || i > EnemyPrefabs.Length - 1)
				break;

			Vector2 newPosition2D = 
				new Vector2(transform.position.x, transform.position.z) + Random.insideUnitCircle.normalized * radius;
			Vector3 newPosition = 
				new Vector3 (newPosition2D.x, 0 , newPosition2D.y);

			/*Debug.Log("Spawnando inimigos");
			Debug.Log("i: " + i + " EnemyArrayTipe[i]: " + EnemyArrayType[i]);
			Debug.Log("Spawnando inimigos");*/

			EnemyArray[i] = 
				Instantiate(EnemyPrefabs[ EnemyArrayType[i] ], newPosition, this.transform.rotation) as GameObject;

			EnemyArray[i].transform.parent = parent.transform;
		}
	}
	bool CanContinue() // Confirma se todo o Array de inimigos esta vazio.
	{
		for( int i = 0 ; i < EnemyArray.Length ; i++)
		{
			if(EnemyArray[i] != null)
			{
				return false;
			}
		}		
		return true;
	}

}

using UnityEngine;
using System.Collections;

public class DestroyObjects : MonoBehaviour {

	public string[] Filter = new string[1];
	public bool useFilter = false;

	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerExit(Collider Other)
	{
		//Debug.Log ("Ativado");
		if(useFilter)
		{
			bool destroy = true;

			//Debug.Log ("Verificando Filtro");
			for(int i = 0 ; i < Filter.Length ; i++)
			{
				if(Other.gameObject.tag == Filter[i])
				{
					destroy = false;
				}
			}
			if(destroy) Destroy (Other.gameObject);
		}
		else
			Destroy (Other.gameObject);

	}
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemSpawner : MonoBehaviour {

	public GameObject[] Items;


	private System.Random random = new System.Random();



	public void dropItem(GameObject enemy)
	{

		int spawnDecision = spawnRate ();

		if(spawnDecision > -1)
		{
			GameObject item = Items[spawnDecision];
			Vector3 spawnPosition = new Vector3(enemy.transform.localPosition.x - 0.6f, enemy.transform.localPosition.y + 1.0f ,enemy.transform.localPosition.z + 0.75f);
			Instantiate (item, spawnPosition, Quaternion.identity); 
		}
		
	}

	int spawnRate()
	{
		float temp = (float)(random.NextDouble ());

		if(temp > .9f)
		{
			return 0;
		}
		else
		{
			return -1;
		}
	}


}

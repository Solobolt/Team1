using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

	GameManager gameManager;
	public GameObject[] Wave;
	int waveNumber = 0;
	
	float waveDelay;
	// Use this for initialization
	void Start () 
	{
		waveDelay = 0;
		gameManager = FindObjectOfType<GameManager>();
		SpawnWave();
	}
	
	// Update is called once per frame
	void Update () 
	{
		waveDelay += Time.deltaTime;

		if (waveDelay > 2f && gameManager.enemyNumb <= 1)
		{
			SpawnWave();
			waveDelay = 0;
		}
	}

	//Spwans enemies at certain times
	void SpawnWave()
	{
		Instantiate (Wave[waveNumber],new Vector3(0,0,gameManager.zBoundry),new Quaternion(0,0,0,0));

		if (waveNumber < (Wave.Length -1))
		{
			waveNumber ++;
		} 
		else if (waveNumber >= Wave.Length)
		{
			waveNumber = (Wave.Length-1);
		}
	}
}

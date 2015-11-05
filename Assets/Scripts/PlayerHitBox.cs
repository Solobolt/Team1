using UnityEngine;
using System.Collections;

public class PlayerHitBox : MonoBehaviour {

	private GameManager gameManager;
	private CubeController player;

	// Use this for initialization
	void Start () {
		gameManager = FindObjectOfType<GameManager> ();
		player = FindObjectOfType<CubeController> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void OnTriggerEnter(Collider otherObject)
	{
		if(otherObject.tag == "EnemyLazor")
		{
			gameManager.P2LifeRemove ();
			player.fireMode = 0;
		}
	}

}

using UnityEngine;
using System.Collections;

public class CubeController : MonoBehaviour {
	
	private Transform myTransform;

	private Vector3 playerPosition;

	public float moveSpeed = 100f;

	GameManager gameManager;


	public PlayerMissile playerMissile;
	public GameObject lazor;
	public GameObject[] muzzle;
	private float lazorFireTime;
	private float lazorFireRate = 0.1f;
	public int fireMode = 0;


	// Use this for initialization
	void Start () {
		myTransform = this.transform;
		gameManager = FindObjectOfType <GameManager>();
	}
	
	// Update is called once per frame
	void Update () {
		Movement ();
		fireLazors ();
		checkBoundry ();
	}

	//Handles basic movement for cube
	private void Movement()
	{
		Time.timeScale = 0.1f;
		playerPosition = myTransform.position;
		if(Input.GetKey ("a"))
		{
			playerPosition.x = playerPosition.x - moveSpeed * Time.deltaTime;
			Time.timeScale = 1f;
		}

		if (Input.GetKey ("d"))
		{
			playerPosition.x = playerPosition.x + moveSpeed * Time.deltaTime;
			Time.timeScale = 1f;
		}

		if (Input.GetKey ("w"))
		{
			playerPosition.z = playerPosition.z + moveSpeed * Time.deltaTime;
			Time.timeScale = 1f;
		}

		if (Input.GetKey ("s"))
		{
			playerPosition.z = playerPosition.z - moveSpeed * Time.deltaTime;
			Time.timeScale = 1f;
		}

		if (Input.GetKey ("space")) 
		{
			Time.timeScale = 2f;
		} 

		myTransform.position = playerPosition;
	}

	private void checkBoundry ()
	{
		playerPosition = myTransform.position;

		//Horizontal Boundry check
		if(playerPosition.x <= -gameManager.xBoundry)
		{
			playerPosition.x = -gameManager.xBoundry;
		}
		else if(playerPosition.x >= gameManager.xBoundry)
		{
			playerPosition.x = gameManager.xBoundry;
		}

		//Vertical Boundrty Check
		if(playerPosition.z >= gameManager.zBoundry)
		{
			playerPosition.z = gameManager.zBoundry;
		} 
		else if (playerPosition.z <= -gameManager.zBoundry)
		{
			playerPosition.z = -gameManager.zBoundry;
		}
		myTransform.position = playerPosition;
	}

	//Handles the fireing of the weapons
	private void fireLazors()
	{
		if (Input.GetMouseButton (0)&& Time.time > lazorFireTime)
		{
			fireModes ();
			lazorFireTime = Time.time + lazorFireRate;
		}

		if (Input.GetMouseButton(0)) 
		{
			Time.timeScale = 1f;
		}
	}

	//Handles how the player fires
	private void fireModes()
	{
		float randAngle = Random.Range (0,360);
		switch (fireMode) 
		{
		case 0:
			Instantiate (lazor, muzzle[0].transform.position, muzzle[0].transform.rotation);
			break;

		case 1:
			for (int i = 1; i < 3; i++)
			{
				Instantiate (lazor, muzzle[i].transform.position, muzzle[i].transform.rotation);
			}
			break;

		case 2:
			for (int i = 0; i < muzzle.Length; i++)
			{
				Instantiate (lazor, muzzle[i].transform.position, muzzle[i].transform.rotation);
			}
			break;
		
		case 3:
			for (int i = 0; i < muzzle.Length; i++)
			{
				Instantiate (playerMissile, muzzle[i].transform.position, Quaternion.Euler(0,randAngle,0));
			}
			break;
		}
	}
}

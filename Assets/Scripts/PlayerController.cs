using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    private Transform myTransform;
    private Vector3 playerPosition;
    public float moveSpeed = 100f;

    GameManager gameManager;


	public GameObject playerMissile;
    public GameObject lazor;
    private float lazorFireTime;
    private float lazorFireRate = 0.1f;

    //Controls setup
    public GameObject weaponBarrel;
    private string useSpecial;
    public GameObject[] muzzle;
	public int fireMode = 0;

    //Aiming
    float aimingX;
    float aimingY;

    //Tilting
    float tiltX = 0;
    float tiltZ = 0;

    public GameObject playerModel;

    // Use this for initialization
    void Start () {
        myTransform = this.transform;
        gameManager = FindObjectOfType<GameManager>();
    }
	
	// Update is called once per frame
	void Update () {
        JoystickMovement();
        CheckBoundry();
    }

	private void KeyboardMovement()
	{
		playerPosition = myTransform.position;
		if (Input.GetButton("P1_VerticalK"))
		{
			float inputValue = 0;
			if(Input.GetKey ("w"))
			{
				inputValue = 1;
			}
			if(Input.GetKey ("s"))
			{
				inputValue = -1;
			}

			playerPosition.z = playerPosition.z + (inputValue * moveSpeed * Time.deltaTime);
		}
		
		if (Input.GetButton("P1_HorizontalK"))
		{
			float inputValue = 0;
			if(Input.GetKey ("d"))
			{
				inputValue = 1;
			}
			if(Input.GetKey ("a"))
			{
				inputValue = -1;
			}
			playerPosition.x = playerPosition.x + (inputValue * moveSpeed * Time.deltaTime);
		}

		if (Input.GetButton("P1_Fire1K"))
		{
			if (Time.time > lazorFireTime)
			{
				Instantiate(lazor, muzzle[0].transform.position, muzzle[0].transform.rotation);
				lazorFireTime = Time.time + lazorFireRate;
			}
		}
		
		if (Input.GetButton("P1_Fire2K"))
		{
			if (Time.time > lazorFireTime)
			{
				print("NO ACTION MAPPED: (P1_Fire2)");
				Time.timeScale = 2;
				lazorFireTime = Time.time + lazorFireRate;
			}
		}
		else
		{
			Time.timeScale = 1;
		}	
		myTransform.position = playerPosition;
	}
    //Handles the movement of the player
    private void JoystickMovement()
    {
        
        playerPosition = myTransform.position;

        //Handles basic movemenet of player
        if (Input.GetAxis("P1_Vertical") != 0)
        {
            float inputValue = Input.GetAxis("P1_Vertical");
            playerPosition.z = playerPosition.z + (inputValue * moveSpeed * Time.deltaTime);
            tiltZ = inputValue * 20;
        }
        else
        {
            tiltZ = 0;
        }

        if (Input.GetAxis("P1_Horizontal") != 0)
        {
            float inputValue = Input.GetAxis("P1_Horizontal");
            playerPosition.x = playerPosition.x + (inputValue * moveSpeed * Time.deltaTime);
            tiltX = inputValue * 20;
        }
        else
        {
            tiltX = 0;
        }

        float angle = (Mathf.Atan2(aimingX, aimingY) * Mathf.Rad2Deg);
        weaponBarrel.transform.rotation = Quaternion.Euler(myTransform.rotation.x, angle, myTransform.rotation.z);
        playerModel.transform.rotation = Quaternion.Euler(tiltZ, 0, -tiltX);

        // Handles aiming of player
        if (Input.GetAxis("P1_Mouse X") != 0)
        {
            aimingX = -Input.GetAxis("P1_Mouse X");
        }
        else aimingX = 0;

        if (Input.GetAxis("P1_Mouse Y") != 0)
        {
            aimingY = -Input.GetAxis("P1_Mouse Y");
        }
        else aimingY = 0;

        if (Input.GetAxis("P1_Fire1") != 0)
        {
            if (Time.time > lazorFireTime)
            {
				fireWeapons ();
            }
        }

        if (Input.GetAxis("P1_Fire2") != 0)
        {
            if (Time.time > lazorFireTime)
            {
                print("TEST ACTION MAPPED: (P1_Fire2)");
                Time.timeScale = 2;
                lazorFireTime = Time.time + lazorFireRate;
            }
        }
        else
        {
            Time.timeScale = 1;
        }

        myTransform.position = playerPosition;

        KeyboardMovement();

        
    }

    //Keeps the player within a certain area 
    private void CheckBoundry()
    {
        playerPosition = myTransform.position;

        //Horizontal Boundry check
        if (playerPosition.x <= -gameManager.xBoundry)
        {
            playerPosition.x = -gameManager.xBoundry;
        }
        else if (playerPosition.x >= gameManager.xBoundry)
        {
            playerPosition.x = gameManager.xBoundry;
        }

        //Vertical Boundrty Check
        if (playerPosition.z >= gameManager.zBoundry)
        {
            playerPosition.z = gameManager.zBoundry;
        }
        else if (playerPosition.z <= -gameManager.zBoundry)
        {
            playerPosition.z = -gameManager.zBoundry;
        }
        myTransform.position = playerPosition;
    }

    //Handles collitions
    private void OnTriggerEnter(Collider otherObject)
    {
        if (otherObject.tag == "EnemyLazor")
        {
            gameManager.P1LifeRemove();
			if(gameManager.P1Lives == 0)
			{
				Destroy (this.gameObject);
			}
        }
    }

	void fireWeapons()
	{

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
				Instantiate (playerMissile, muzzle[i].transform.position, muzzle[0].transform.rotation);
			}
			break;
		}
		lazorFireTime = Time.time + lazorFireRate;
	}
}

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour {

    public GameObject player1GameObject;
    public GameObject player2GameObject;
    private GameObject P1Spawn;
    private GameObject P2Spawn;

	public float zBoundry = 50.0f;
	public float xBoundry = 145.0f;

	public float zBoundryMoving = 0.0f;

	public int innerClip = 30;

	public int enemyNumb = 0;

	public GameObject P1LifeUIObject;
    public GameObject P2LifeUIObject;

    public int P1Lives = 3;
    public int P2Lives = 3;

    public GameObject[] enemyUnitList;

	public GameObject enemiesKilledUIObject;
	public int enemiesKilled = 0;

    public string[] controllers;
    // Use this for initialization
    void Start() {
        P1Spawn = GameObject.FindGameObjectWithTag("P1Spawner");
        P2Spawn = GameObject.FindGameObjectWithTag("P2Spawner");

        controllers = Input.GetJoystickNames();

        //instantiate player 1 HERE
        Instantiate(player1GameObject,P1Spawn.transform.position,P1Spawn.transform.rotation);
        
        if (controllers[1]!="")
        {
            //instatiate player 2
            Instantiate(player2GameObject, P2Spawn.transform.position, P2Spawn.transform.rotation);
            print("Player 1:" + controllers[0]);
            print("Player 2:" + controllers[1]);
        }

    }
	
	// Update is called once per frame
	void Update () 
	{
		if (enemyNumb == 0) 
		{
				Time.timeScale = 1f;
		}
		//Update enemy unit list
		enemyUnitList = GameObject.FindGameObjectsWithTag ("Enemy");

		//NEW UI stuff
		enemiesKilledUIObject.GetComponent <Text> ().text = "Enemies Killed:" + enemiesKilled;
		P1LifeUIObject.GetComponent <Text> ().text = "P1 Lives:" + P1Lives;
        P2LifeUIObject.GetComponent <Text> ().text = "P2 Lives:" + P2Lives;
    }

    public void P1LifeRemove()
    {
        P1Lives--;
    }

    public void P2LifeRemove()
    {
        P2Lives--;
    }
}
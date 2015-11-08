using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelChange : MonoBehaviour {

    private GameManager gameManager;

    public Canvas gameOverScreen;

    private bool gameOverScreenUp = false;

	// Use this for initialization
	void Start () {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
	}
	
	// Update is called once per frame

	void Update () {
        if (gameManager.P1Lives <= 0 && gameManager.numbPlayers == 1 && gameOverScreenUp == false)
        {
            gameOverScreenUp = true;
            Instantiate(gameOverScreen,new Vector3(0,0,0), new Quaternion(0,0,0,0));
        }
        else
        {
            if(gameManager.P1Lives <= 0 && gameManager.numbPlayers == 2 && gameManager.P2Lives <= 0 && gameOverScreenUp == false)
            {
                gameOverScreenUp = true;
                Instantiate(gameOverScreen, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0));
            }
        }
    }
}

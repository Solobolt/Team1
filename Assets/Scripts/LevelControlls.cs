using UnityEngine;
using System.Collections;

public class LevelControlls : MonoBehaviour {
    //Reloads currant level
    public void RestartLevel()
    {
        Application.LoadLevel(Application.loadedLevel);
    }

    //Exits the Game
    public void QuitGame()
    {
        Application.Quit();
    }
}

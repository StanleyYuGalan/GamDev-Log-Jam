using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Time.timeScale = 1;
    }

    public void loadMainMenu()
    {
     SceneManager.LoadScene("Main Menu");
    }
	
    public void loadGame()
    {
    SceneManager.LoadScene("Endless Runner");
    }

    public void quitGame()
    {
        Debug.Log("Game quit");
        Application.Quit();
    }

}

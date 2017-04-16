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
        PlayerPrefs.DeleteKey(KeyNames.KEY_NEW_NAME);
        PlayerPrefs.DeleteKey(KeyNames.KEY_HIGHSCORE);

        int rand = UnityEngine.Random.Range(1, 4);

        switch (rand)
        {
            case 1: SceneManager.LoadScene("Endless Runner");
                break;
            case 2: SceneManager.LoadScene("Endless Runner - Fire");
                break;
            case 3:
                SceneManager.LoadScene("Endless Runner - Illegal");
                break;

        }

    }

    public void quitGame()
    {
        Debug.Log("Game quit");
        Application.Quit();
    }

}

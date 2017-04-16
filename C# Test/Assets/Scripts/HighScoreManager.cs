using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreManager : MonoBehaviour
{
    private ScoreObject[] scoreList;
    private int numScores = 0;
    private string[] scoreTextArray = new string[] { "1. -", "2. -", "3. -", "4. -", "5. -" };
    
    public Text scoreText1, scoreText2, scoreText3, scoreText4, scoreText5;

    // Use this for initialization
    void Start ()
    {

    }

    // Update is called once per frame
    void Update ()
    {

    }

    public void loadHighscores()
    {
        //resetPlayerPrefs();

        constructOriginalScoreList();

        for (int i = 0; i < numScores; i++)
        {
            if (scoreList[i].Score != 0)
            {
                scoreTextArray[i] = (i+1) + ". " + scoreList[i].ToString();
            }
        }

        scoreText1.text = scoreTextArray[0];
        scoreText2.text = scoreTextArray[1];
        scoreText3.text = scoreTextArray[2];
        scoreText4.text = scoreTextArray[3];
        scoreText5.text = scoreTextArray[4];
    }

    private void resetPlayerPrefs()
    {
        PlayerPrefs.DeleteKey(KeyNames.KEY_NEW_NAME);
        PlayerPrefs.DeleteKey(KeyNames.KEY_HIGHSCORE);
        PlayerPrefs.DeleteKey(KeyNames.KEY_NAME_ARRAY);
        PlayerPrefs.DeleteKey(KeyNames.KEY_SCORE_ARRAY);
    }

    private void constructOriginalScoreList ()
    {
        string[] names = PlayerPrefsX.GetStringArray(KeyNames.KEY_NAME_ARRAY);
        float[] scores = PlayerPrefsX.GetFloatArray(KeyNames.KEY_SCORE_ARRAY);
        scoreList = new ScoreObject[5];

        numScores = names.Length;

        if (scores.Length != 0 && names.Length != 0 && scores.Length == names.Length)
        {
            for (int i = 0; i < numScores; i++)
            {
                scoreList[i] = new ScoreObject(names[i], scores[i]);
            }
        }
    }
}

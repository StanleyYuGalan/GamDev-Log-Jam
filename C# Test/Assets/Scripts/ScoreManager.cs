using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    private ScoreObject[] scoreList;
    private string newName;
    private float newScore;
    private int numScores = 0;
    public Text newScoreText;

    // Use this for initialization
    void Start ()
    {
        scoreList = new ScoreObject[6];
        constructOriginalScoreList();
        newName = PlayerPrefs.GetString(KeyNames.KEY_NEW_NAME);
        newScore = Convert.ToSingle(newScoreText.text);
    }

    // Update is called once per frame
    void Update ()
    {

    }

    public bool isNewHighScore()
    {
        newScore = Convert.ToSingle(newScoreText.text);
        Debug.Log("SCORE SCORE: " + newScore);

        if (numScores == 0 || numScores != 5 || numScores == 5 && newScore > scoreList[numScores - 1].Score)
        {
            return true;
        }

        return false;
    }

    public void storeNewHighscore()
    {
        newScore = Convert.ToSingle(newScoreText.text);
        Debug.Log(newName + ": " + newScore);

        int tieIndex = checkForScoreTies();

        if ( tieIndex != -1 )
        {
            insertTiedScore(tieIndex);
        }
        else
        {
            insertNewScoreThenSort();
        }

        if( numScores > 5 )
        {
            numScores = 5;
        }

        float[] scores = new float[numScores];
        string[] names = new string[numScores];

        for ( int i = 0; i < numScores; i++ )
        {
            names[i] = scoreList[i].Name;
            scores[i] = scoreList[i].Score;
        }

        PlayerPrefsX.SetStringArray(KeyNames.KEY_NAME_ARRAY, names);
        PlayerPrefsX.SetFloatArray(KeyNames.KEY_SCORE_ARRAY, scores);
    }

    private void constructOriginalScoreList()
    {
        string[] names = PlayerPrefsX.GetStringArray(KeyNames.KEY_NAME_ARRAY);
        float[] scores = PlayerPrefsX.GetFloatArray(KeyNames.KEY_SCORE_ARRAY);

        numScores = names.Length;

        if ( scores.Length != 0 && names.Length != 0 && scores.Length == names.Length )
        {
            for( int i = 0; i < numScores; i++ )
            {
                scoreList[i] = new ScoreObject(names[i], scores[i]);
            }
        }
    }

    private int checkForScoreTies()
    {
        for( int i = 0; i < numScores; i++ )
        {
            if( scoreList[i].Score == newScore )
            {
                return i;
            }
        }

        return -1;
    }

    private void insertTiedScore ( int tieIndex )
    {
        for (int i = numScores; i > tieIndex; i--)
        {
            scoreList[i] = scoreList[i - 1];
        }

        newName = PlayerPrefs.GetString(KeyNames.KEY_NEW_NAME);
        scoreList[tieIndex + 1] = new ScoreObject(newName, newScore);
    }

    private void insertNewScoreThenSort()
    {
        newName = PlayerPrefs.GetString(KeyNames.KEY_NEW_NAME);
        scoreList[numScores] = new ScoreObject(newName, newScore);
        ScoreObject temp;

        numScores++;

        for (int i = 0; i < numScores; i++)
        {
            for (int j = 0; j < numScores - 1; j++)
            {
                if( scoreList[j].Score < scoreList[j + 1].Score )
                {
                    temp = scoreList[j + 1];
                    scoreList[j + 1] = scoreList[j];
                    scoreList[j] = temp;
                }
            }
        }
    }
}

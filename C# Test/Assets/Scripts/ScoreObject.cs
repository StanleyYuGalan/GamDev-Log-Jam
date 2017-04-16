using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreObject : MonoBehaviour
{
    string name;
    float score;

    public ScoreObject ( string name, float score )
    {
        this.name = name;
        this.score = score;
    }

    public string Name
    {
        get { return name; }
    }

    public float Score
    {
        get { return score; }
    }

    public string ToString ()
    {
        return name + ": " + score;
    }
}

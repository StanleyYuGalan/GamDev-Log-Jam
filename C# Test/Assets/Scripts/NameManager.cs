using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NameManager : MonoBehaviour
{
    public Text newName;

    // Use this for initialization
    void Start ()
    {

    }

    // Update is called once per frame
    void Update ()
    {

    }

    public void storeNewName()
    {
        PlayerPrefs.SetString(KeyNames.KEY_NEW_NAME, newName.text);
    }
}

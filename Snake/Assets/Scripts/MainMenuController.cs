using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public Text HighScore;


	// Use this for initialization
	void Start ()
    {
        HighScoreFunction();    
	}
	
    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    void HighScoreFunction()
    {
        HighScore.text = PlayerPrefs.GetInt("HighScore").ToString();
    }

	// Update is called once per frame
	void Update ()
    {
		
	}
}

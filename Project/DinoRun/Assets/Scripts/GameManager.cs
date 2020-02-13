using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool GameActive = false;
    public bool GameOver = false;
    public int Score = 0;
    public int Highscore = 0;
    public float ScoreSpeed = 0.1f;
    public float timer = 0f;
    public string Scene = "Game";
    public string HighscoreVar = "Highscore";
    // Start is called before the first frame update
    void Start()
    {
        Highscore = PlayerPrefs.GetInt(HighscoreVar, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("TitleScreen");
        }
        timer += Time.deltaTime;
        if (timer > ScoreSpeed)
        {
            if (GameActive)
            {
                Score++;
                timer = 0f;
            }
        }
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKey(KeyCode.UpArrow))
        {
            if (!GameOver)
            {
                GameActive = true;
            }
            else
            {
                if (PlayerPrefs.GetInt(HighscoreVar, 0) < Score)
                {
                    PlayerPrefs.SetInt(HighscoreVar, Score);
                }
                SceneManager.LoadScene(Scene);
            }
            
        }
    }
}

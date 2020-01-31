using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool GameActive = false;
    public bool GameOver = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!GameOver)
            {
                GameActive = true;
            }
            else
            {
                SceneManager.LoadScene("Game");
            }
            
        }
    }
}

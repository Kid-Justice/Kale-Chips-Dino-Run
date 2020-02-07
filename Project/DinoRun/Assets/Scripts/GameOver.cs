using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    Text text;
    GameManager GM;
    // Start is called before the first frame update
    void Start()
    {
        text = gameObject.GetComponent<Text>();
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(GM.GameOver == true)
        {
            text.text = "Game Over";
        }
            
    }
}

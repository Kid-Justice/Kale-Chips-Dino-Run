using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scores : MonoBehaviour
{
    bool hasPlayedSound = false;
    public AudioSource audioSource;
    public AudioClip levelup;
    public float volume = 0.5f;
    Text text;
    public GameObject GMOB;
    public bool isHighscore;
    int Highscore;
    GameManager GM;
    // Start is called before the first frame update
    void Start()
    {
        text = gameObject.GetComponent<Text>();
        GM = GMOB.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Highscore = GM.Highscore;
        if (isHighscore)
        {
            if (Highscore >= 10000)
            {
                text.text = "" + Highscore;
            }
            else if (Highscore >= 1000)
            {
                text.text = "0" + Highscore;
            }
            else if (Highscore >= 100)
            {
                text.text = "00" + Highscore;
            }
            else if (Highscore >= 10)
            {
                text.text = "000" + Highscore;
            }
            else
            {
                text.text = "0000" + Highscore;
            }
        }
        else
        {
            if (GM.Score >= 10000)
            {
                text.text = "" + GM.Score;
            }
            else if (GM.Score >= 1000)
            {
                text.text = "0" + GM.Score;
            }
            else if (GM.Score >= 100)
            {
                text.text = "00" + GM.Score;
            }
            else if (GM.Score >= 10)
            {
                text.text = "000" + GM.Score;
            }
            else
            {
                text.text = "0000" + GM.Score;
            }
            if (GM.Score % 100 == 0 && GM.Score >= 100 && !hasPlayedSound)
            {
                audioSource.PlayOneShot(levelup, volume);
                hasPlayedSound = true;
            }
            if (GM.Score % 101 == 0 && hasPlayedSound)
            {
                hasPlayedSound = false;
            }
        }
    }
}

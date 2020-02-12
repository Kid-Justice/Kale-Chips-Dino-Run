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
    public float TimeToBlink  = 1.0f;
    public int Intervals = 2;
    public int currentInterval = 0;
    public float BlinkTimer = 0f;
    public string LockScore = "0";
    GameManager GM;
    public bool Blinking = false;
    // Start is called before the first frame update
    void Start()
    {
        text = gameObject.GetComponent<Text>();
        GM = GMOB.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (BlinkTimer > 0f)
        {
            BlinkTimer -= Time.deltaTime;
        }
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
            if (!Blinking)
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
                /*
                if (GM.Score % 100 == 0 && GM.Score >= 100 && !hasPlayedSound)
                {
                    audioSource.PlayOneShot(levelup, volume);
                    hasPlayedSound = true;
                }
                if (GM.Score % 101 == 0 && hasPlayedSound)
                {
                    hasPlayedSound = false;
                }
                */
            }
            if (GM.Score % 100 == 0 && GM.Score >= 100 && !Blinking)
            {
                audioSource.PlayOneShot(levelup, volume);
                LockScore = text.text;
                BlinkTimer = TimeToBlink;
                currentInterval = 1;
                Blinking = true;
            }
            if (Blinking && BlinkTimer >= TimeToBlink/2)
            {
                text.text = "";
            }
            else if (Blinking && BlinkTimer < TimeToBlink / 2)
            {
                text.text = LockScore;
            }
            if (Blinking && BlinkTimer <= 0 && currentInterval < Intervals)
            {
                BlinkTimer = TimeToBlink;
                currentInterval++;
            }
            else if(Blinking && BlinkTimer <= 0 && currentInterval >= Intervals)
            {
                Blinking = false;
            }
        }
    }
}

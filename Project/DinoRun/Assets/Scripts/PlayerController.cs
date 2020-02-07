using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;

public class PlayerController : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip jump;
    public float volume = 0.5f;
    Animator anim;
    public bool Falling = false;
    public bool Jumping = false;
    public bool LockSpeed = false;
    public float LargeJumpSpeed = 0.5f;
    public float SmallJumpSpeed = 0.25f;
    public float Decrementor = 0.01f;
    public float CurrentSpeed = 0f;
    public float LongJumpTime = 0.1f;
    public float timer = 0f;
    public float GameTimer = 0f;
    public Vector3 StartPosition;
    GameManager GM;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        StartPosition = transform.position;
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        if (GM.GameActive == true)
        {
            if (!Jumping && !Falling && GameTimer > 0.5)
            {
                if (Input.GetKey(KeyCode.DownArrow))
                {
                    anim.SetInteger("State", 6);
                }
                else
                {
                    anim.SetInteger("State", 2);
                }
            }
            else if (Jumping || Falling)
            {
                anim.SetInteger("State", 0);
            }
        }
        else if (GM.GameOver == true)
        {
            anim.SetInteger("State", 4);
        }

    }
    // Update is called once per frame
    void FixedUpdate()
    {
        GameTimer += Time.fixedDeltaTime;
        if (GM.GameActive)
        {
            if (Jumping || Falling)
            {
                CurrentSpeed -= Decrementor;
                transform.position = new Vector3(transform.position.x, transform.position.y + CurrentSpeed, transform.position.z);
            }
            if (Jumping && CurrentSpeed < 0)
            {
                Jumping = false;
                Falling = true;
            }
            if (!Jumping && !Falling && GameTimer > 0.5)
            {

                transform.position = StartPosition;
                if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.UpArrow))
                {
                    Jumping = true;
                    audioSource.PlayOneShot(jump, volume);
                    timer = 0f;
                    //CurrentSpeed = SmallJumpSpeed;
                }
            }
            if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.UpArrow))
            {
                timer += Time.fixedDeltaTime;
            }

            if ((Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.UpArrow)) && Jumping && timer >= LongJumpTime && !LockSpeed)
            {
                CurrentSpeed = LargeJumpSpeed;
                LockSpeed = true;
            }
            else if ((Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.UpArrow)) && Jumping && timer < LongJumpTime && !LockSpeed)
            {
                CurrentSpeed = SmallJumpSpeed;
                LockSpeed = true;
            }
            if (Falling && transform.position.y + CurrentSpeed <= StartPosition.y)
            {
                Falling = false;
                CurrentSpeed = 0f;
                LockSpeed = false;
                timer = 0f;
                
                if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.UpArrow))
                {
                    Jumping = true;
                    audioSource.PlayOneShot(jump, volume);
                    CurrentSpeed = LargeJumpSpeed;
                    
                    //LockSpeed = true;
                }
                
            }
            if ((Jumping || Falling) && Input.GetKey(KeyCode.DownArrow))
            {
                CurrentSpeed = -0.5f;
                Falling = true; 
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<GroundTag>() != null)
        {
            if (Falling)
            {
                Falling = false;
                CurrentSpeed = 0f;
                LockSpeed = false;
            }
        }
        if (collision.gameObject.GetComponent<Obstacle>() != null)
        {
            GM.GameActive = false;
            GM.GameOver = true;
        }
    }
}

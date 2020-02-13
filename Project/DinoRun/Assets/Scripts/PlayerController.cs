using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;

public class PlayerController : MonoBehaviour
{
    public bool IsPlusDino = false;
    public AudioSource audioSource;
    public AudioClip jump;
    public AudioClip deathsound;
    public AudioClip[] jumpsounds = new AudioClip[6];
    public float volume = 0.5f;
    Animator anim;
    public bool Falling = false;
    public bool Jumping = false;
    public bool Crouching = false;
    public bool LockSpeed = false;
    public float LargeJumpSpeed = 0.5f;
    public float SmallJumpSpeed = 0.25f;
    public float Decrementor = 0.01f;
    public float CurrentSpeed = 0f;
    public float LongJumpTime = 0.1f;
    public float timer = 0f;
    public float GameTimer = 0f;
    public Vector3 StartPosition;
    public PolygonCollider2D NormalCollider;
    public PolygonCollider2D CrouchingCollider;
    GameManager GM;
    public bool hasPlayedDeathSound = false;
    public bool hasPlayed = false;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        StartPosition = transform.position;
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        if (IsPlusDino == true)
        {
            if (GM.GameActive == true)
            {
                if (!Jumping && !Falling && GameTimer > 0.5)
                {
                    if (Input.GetKey(KeyCode.DownArrow))
                    {
                        anim.SetInteger("Plus_State", 3);
                    }
                    else
                    {
                        anim.SetInteger("Plus_State", 1);
                    }
                }
                else if (Jumping || Falling)
                {
                    anim.SetInteger("Plus_State", 0);
                }
            }

            else if (GM.GameOver == true)
            {
                anim.SetInteger("Plus_State", 2);
            }
            if (Crouching)
            {
                NormalCollider.enabled = false;
                CrouchingCollider.enabled = true;
            }
            else
            {
                NormalCollider.enabled = true;
                CrouchingCollider.enabled = false;
            }
        }
        else if (IsPlusDino == false)
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
            if (Crouching)
            {
                NormalCollider.enabled = false;
                CrouchingCollider.enabled = true;
            }
            else
            {
                NormalCollider.enabled = true;
                CrouchingCollider.enabled = false;
            }
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        GameTimer += Time.fixedDeltaTime;
        if (GM.GameActive)
        {
            hasPlayed = true;
            if (Input.GetKey(KeyCode.DownArrow))
            {
                Crouching = true;
            }
            else
            {
                Crouching = false;
            }
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
                if ((Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.UpArrow)) && !Crouching)
                {
                    Jumping = true;
                    if (IsPlusDino)
                    {
                        audioSource.PlayOneShot(jumpsounds[Random.Range(0, 6)], volume);
                    }
                    else
                    {
                        audioSource.PlayOneShot(jump, volume);
                    }
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
            if (Falling && transform.position.y < StartPosition.y)
            {
                Falling = false;
                transform.position = new Vector3(transform.position.x, StartPosition.y, transform.position.z);
            }
        }
        else
        {
            if (!hasPlayedDeathSound && hasPlayed)
            {
                Debug.Log("IsPlaying");
                audioSource.PlayOneShot(deathsound, volume);
                hasPlayedDeathSound = true;
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

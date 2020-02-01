using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
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
        StartPosition = transform.position;
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        GameTimer += Time.deltaTime;
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
                if (Input.GetKey(KeyCode.Space))
                {
                    Jumping = true;
                    timer = 0f;
                    CurrentSpeed = SmallJumpSpeed;
                }
            }
            if (Input.GetKey(KeyCode.Space) && Jumping)
            {
                if (timer >= LongJumpTime && !LockSpeed)
                {
                    CurrentSpeed = LargeJumpSpeed;
                    LockSpeed = true;
                }
                timer = 0f;
                timer += Time.deltaTime;
            }
            if (Falling && transform.position.y + CurrentSpeed <= StartPosition.y)
            {
                Falling = false;
                CurrentSpeed = 0f;
                LockSpeed = false;
                if (Input.GetKey(KeyCode.Space))
                {
                    Jumping = true;
                    CurrentSpeed = LargeJumpSpeed;
                    LockSpeed = true;
                }
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

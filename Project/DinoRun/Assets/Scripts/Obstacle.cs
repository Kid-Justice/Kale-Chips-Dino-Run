using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    Animator anim;
    public bool IsBird;
    public bool IsPlus = false;
    public float speed = 0.1f;
    public float SpeedIncrease = 0.01f;
    public float boarder = -20;
    public float YOffset = 0f;
    public bool HasIncreasedSpeed = false;
    GameManager GM;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        transform.position = new Vector3(transform.position.x, transform.position.y + YOffset, transform.position.z);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (GM.GameActive)
        {
            transform.position = new Vector3(transform.position.x - speed, transform.position.y, transform.position.z);
        }
        if(IsBird == true && !IsPlus)
        {
            anim.SetInteger("State", 0);
        }
        if (transform.position.x <= boarder)
        {
            Destroy(gameObject);
        }
        if (GM.Score % 100 == 0 && GM.Score != 0 && !HasIncreasedSpeed)
        {
            speed += SpeedIncrease;
            HasIncreasedSpeed = true;
        }
        if (GM.Score % 100 != 0 && HasIncreasedSpeed)
        {
            HasIncreasedSpeed = false;
        }
    }
}

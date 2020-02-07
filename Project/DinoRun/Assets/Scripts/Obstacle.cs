using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;

public class Obstacle : MonoBehaviour
{
    Animator anim;
    public bool IsBird;
    public float speed = 0.1f;
    GameManager GM;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (GM.GameActive)
        {
            transform.position = new Vector3(transform.position.x - speed, transform.position.y, transform.position.z);
        }
        if(IsBird == true)
        {
            anim.SetInteger("State", 0);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    public float Speed = 0.1f;
    public float SpeedIncrease = 0.01f;
    public float SpeedAdditive;
    GameManager GM;
    public float Boarder = -20f;
    public bool HasIncreasedSpeed = false;
    // Start is called before the first frame update
    void Start()
    {
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GM.GameActive)
        {
            transform.position = new Vector3(transform.position.x - Speed, transform.position.y, transform.position.z);
            if (transform.position.x < Boarder)
            {
                Destroy(gameObject);
            }
            if (GM.Score % 100 == 0 && GM.Score != 0 && !HasIncreasedSpeed)
            {
                Speed += SpeedIncrease;
                HasIncreasedSpeed = true;
            }
            if (GM.Score % 100 != 0 && HasIncreasedSpeed)
            {
                HasIncreasedSpeed = false;
            }
        }
    }
}

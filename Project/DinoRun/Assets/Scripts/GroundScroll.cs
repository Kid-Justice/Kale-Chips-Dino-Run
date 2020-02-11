using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundScroll : MonoBehaviour
{
    public float Speed = 0.1f;
    public Vector3 SpotToTeleportTo;
    public float Boarder;
    GameManager GM;
    public GameObject GMOB;
    public bool HasIncreasedSpeed = false;
    public float SpeedIncrease = 0.01f;
    // Start is called before the first frame update
    void Start()
    {
        GM = GMOB.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GM.GameActive)
        {
            transform.position = new Vector3(transform.position.x - Speed, transform.position.y, transform.position.z);
            if (transform.position.x < Boarder)
            {
                transform.position = SpotToTeleportTo;
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

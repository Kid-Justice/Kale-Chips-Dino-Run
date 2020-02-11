using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSpawn : MonoBehaviour
{
    public float Delay = 5f;
    public float yMin;
    public float yMax;
    public float timer = 0f;
    public GameObject Cloud;
    public GameObject GMOB;
    GameManager GM;

    // Start is called before the first frame update
    void Start()
    {
        GM = GMOB.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (GM.GameActive)
        {
            if (timer <= 0f)
            {
                Instantiate(Cloud, new Vector3(transform.position.x, Random.Range(yMin, yMax), transform.position.z), Quaternion.identity);
                timer = Delay;
            }
            else
            {
                timer -= Time.fixedDeltaTime;
            }
        }
    }
}

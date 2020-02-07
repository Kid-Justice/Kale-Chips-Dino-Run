using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject GMOB;
    GameManager GM;
    public int ScoreToSpawnAt = 0;
    public float SpawnInterval = 1f;
    public GameObject[] ThingsToSpawn = new GameObject[10];
    public float timer = 0f;
    int ObjectToSpawn = 0;
    Vector3 SpawnPosition;
    // Start is called before the first frame update
    void Start()
    {
        GM = GMOB.GetComponent<GameManager>();
        timer = SpawnInterval;
        SpawnPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (GM.Score >= ScoreToSpawnAt)
        {
            if (timer >= SpawnInterval)
            {
                ObjectToSpawn = Random.Range(0, ThingsToSpawn.Length);
                Instantiate(ThingsToSpawn[ObjectToSpawn], SpawnPosition, Quaternion.identity);
                timer = 0f;
            }
            else
            {
                timer += Time.deltaTime;
            }
        }
    }
}

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
    public GameObject[] FlyingDinos = new GameObject[2];
    public float[] FlyingDinoPositions = new float[2];
    public int SpawnFlyingDinosAt = 488;
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
                if (SpawnFlyingDinosAt >= GM.Score)
                {
                    ObjectToSpawn = Random.Range(0, ThingsToSpawn.Length);
                    Instantiate(ThingsToSpawn[ObjectToSpawn], SpawnPosition, Quaternion.identity);
                    timer = 0f;
                }
                else
                {
                    switch (Random.Range(1, 3))
                    {
                        case 1:
                            ObjectToSpawn = Random.Range(0, ThingsToSpawn.Length);
                            Instantiate(ThingsToSpawn[ObjectToSpawn], SpawnPosition, Quaternion.identity);
                            timer = 0f;
                            break;
                        case 2:
                            ObjectToSpawn = Random.Range(0, FlyingDinos.Length);
                            Instantiate(FlyingDinos[ObjectToSpawn], new Vector3(SpawnPosition.x, FlyingDinoPositions[ObjectToSpawn], SpawnPosition.z), Quaternion.identity);
                            timer = 0f;
                            break;
                    }
                }
            }
            else
            {
                timer += Time.deltaTime;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public float spawnWidth;
    public float spawnHeight;
    public float spawnDelay;
    public float spawnDelayReduction;
    public float minimumSpawnDelay;

    private float spawnTimer;

    public GameManager gameManager;

    public List<Enemy> enemyPrefabs;

    // Start is called before the first frame update
    void Start()
    {
        spawnTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.targetBalls.Count > 0)
        {
            spawnTimer += Time.deltaTime;

            if (spawnTimer >= spawnDelay)
            {
                spawnTimer = 0;

                spawnDelay = Mathf.Clamp(spawnDelay - spawnDelayReduction, minimumSpawnDelay, spawnDelay);

                float spawnX = 0.0f;
                float spawnY = 0.0f;

                int spawnSide = Random.Range(0, 4);

                switch (spawnSide)
                {
                    case 0:
                        spawnX = -spawnWidth;
                        spawnY = Random.Range(-spawnHeight, spawnHeight);

                        break;

                    case 1:
                        spawnX = Random.Range(-spawnWidth, spawnWidth);
                        spawnY = spawnHeight;

                        break;

                    case 2:
                        spawnX = spawnWidth;
                        spawnY = Random.Range(-spawnHeight, spawnHeight);

                        break;

                    case 3:
                        spawnX = Random.Range(-spawnWidth, spawnWidth);
                        spawnY = -spawnHeight;

                        break;

                    default:

                        break;
                }

                Enemy newEnemy = Instantiate<Enemy>(enemyPrefabs[Random.Range(0, enemyPrefabs.Count)]);
                newEnemy.transform.position = new Vector2(spawnX, spawnY);
                newEnemy.target = gameManager.targetBalls[Random.Range(0, gameManager.targetBalls.Count)];
                newEnemy.Launch();
            }
        }
    }
}

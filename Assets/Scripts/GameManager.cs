using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private float bonusTimer;
    private float nextBonusTime;
    
    public List<TargetBall> targetBalls;

    public TargetBall bonusBallPrefab;

    [HideInInspector]
    public List<TargetBall> knockedOutTargets;

    public TimeManager timeManager;

    public GameObject winPanel;
    public GameObject losePanel;

    public float minimumBonusTime;
    public float maximumBonusTime;
    public float spawnWidth;
    public float spawnHeight;
    public float bonusBallSpeed;

    private void Start()
    {
        winPanel.SetActive(false);
        losePanel.SetActive(false);
        timeManager.Paused = false;
        Time.timeScale = 1;
        bonusTimer = 0.0f;
        nextBonusTime = Random.Range(minimumBonusTime, maximumBonusTime);
    }

    // Update is called once per frame
    private void Update()
    {
        foreach (TargetBall targetBall in targetBalls)
        {
            if (!targetBall.isInGoal)
            {
                knockedOutTargets.Add(targetBall);
                targetBalls.Remove(targetBall);

                break;
            }
        }

        foreach (TargetBall targetBall in knockedOutTargets)
        {
            if (targetBall.isInGoal)
            {
                targetBalls.Add(targetBall);
                knockedOutTargets.Remove(targetBall);

                break;
            }
        }

        if (targetBalls.Count == 0)
        {
            timeManager.Paused = true;
            losePanel.SetActive(true);
            Time.timeScale = 0;
        }

        if (timeManager.startingTime <= 0)
        {
            timeManager.Paused = true;
            winPanel.SetActive(true);
            Time.timeScale = 0;
        }

        bonusTimer += Time.deltaTime;

        if (bonusTimer >= nextBonusTime)
        {
            bonusTimer = 0.0f;
            nextBonusTime = Random.Range(minimumBonusTime, maximumBonusTime);

            LaunchBonus();
        }
    }

    private void LaunchBonus()
    {
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

        TargetBall newBonusBall = Instantiate<TargetBall>(bonusBallPrefab);
        newBonusBall.transform.position = new Vector2(spawnX, spawnY);

        Rigidbody2D rigidBody = newBonusBall.GetComponent<Rigidbody2D>();

        Vector2 moveDirection = transform.position - newBonusBall.transform.position;

        moveDirection.Normalize();
        moveDirection *= bonusBallSpeed;

        rigidBody.velocity = moveDirection;

        knockedOutTargets.Add(newBonusBall);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

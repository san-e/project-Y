using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogicManager : MonoBehaviour
{
    public int score = 0;
    public Text scoreText;
    public ParticleSystem ps;
    public Camera mainCamera;
    public float velocity = 1.0F;
    [HideInInspector]
    public float leftBound;
    [HideInInspector]
    public float rightBound;
    [HideInInspector]
    public float upperBound;
    [HideInInspector]
    public float lowerBound;
    public float cameraDepth;
    public GameObject enemy;
    private float timer;

    private void Start()
    {
        leftBound = mainCamera.ScreenToWorldPoint(new Vector3(0f, 0f, cameraDepth)).x;
        rightBound = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, 0f, cameraDepth)).x;
        upperBound = mainCamera.ScreenToWorldPoint(new Vector3(0f, Screen.height, cameraDepth)).z;
        lowerBound = mainCamera.ScreenToWorldPoint(new Vector3(0f, 0f, cameraDepth)).z;

    }

    private void SpawnMiddle(int amount, int health)
    {
        float offset = amount * 2f;
        int angle = Random.Range(-30, 30);
        for (int i = 0; i < amount; i++)
        {
            Instantiate(enemy, new Vector3(offset + i * 20f, 0f, upperBound*2), Quaternion.Euler(-90, 0, angle)).GetComponent<EnemyScript>().health = health;
        }
    }

    private void Update()
    {
        if (timer <= 0) 
        {
            SpawnMiddle(Random.Range(1, 5), 2);
            timer = 2f;
        }
        var main = ps.main;
        main.simulationSpeed = velocity;
        timer -= Time.deltaTime;
    }

    public void addPoints(int amount)
    {
        score += amount;
        scoreText.text = score.ToString();
    }

}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PathFinder : MonoBehaviour
{
    [SerializeField] Slider healthSlider;
    [SerializeField] Vector2 healthSliderOffset;

    EnemySpawner enemySpawner;
    WaveConfigSO waveConfig;

    private List<Transform> waypoints;
    private int waypointIndex = 0;


    private void Awake()
    {
        enemySpawner = FindObjectOfType<EnemySpawner>();
    }

    void Start()
    {
        waveConfig = enemySpawner.GetCurrentWave();
        waypoints = waveConfig.GetWaypoints();
        transform.position = waypoints[waypointIndex].position;
    }

    void Update()
    {
        FollowPath();
    }

    private void FollowPath()
    {
        if (waypointIndex < waypoints.Count)
        {
            Vector3 targetPosition = waypoints[waypointIndex].position;
            float delta = waveConfig.GetMoveSpeed() * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, delta);
            healthSlider.transform.position = Camera.main.WorldToScreenPoint(transform.position + (Vector3)healthSliderOffset);
            Debug.Log(transform.position);
            if (transform.position == targetPosition)
            {
                waypointIndex++;
            }
        } else
        {
            Destroy(gameObject);
        }
    }
}

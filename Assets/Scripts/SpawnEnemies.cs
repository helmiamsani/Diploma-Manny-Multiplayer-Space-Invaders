﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class SpawnEnemies : NetworkBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab;

    public Transform enemyParent;

    [SerializeField]
    private float spawnInterval = 1.0f;
    [SerializeField]
    private float enemySpeed = 1.0f;

    public override void OnStartServer()
    {
        InvokeRepeating("SpawnEnemy", this.spawnInterval, this.spawnInterval);
    }

    void SpawnEnemy()
    {
        Vector2 spawnPosition = new Vector2(Random.Range(-4.0f, 4.0f), this.transform.position.y);
        GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity, enemyParent) as GameObject;
        enemy.GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, -this.enemySpeed);
        NetworkServer.Spawn(enemy);
        Destroy(enemy, 10); 
    }
}

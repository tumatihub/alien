using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombWeapon : MonoBehaviour
{
    [SerializeField] Bomb bombPrefab = null;
    [SerializeField] Transform bombSpawnPoint = null;
    [SerializeField] int totalBombs = 3;
    int currentBombs = 0;

    public static event Action<int> OnStartBombWeapon;
    public static event Action OnUseBomb;

    void Awake()
    {
        currentBombs = totalBombs;    
    }

    void Start()
    {
        OnStartBombWeapon?.Invoke(totalBombs);
    }

    public void SpawnBomb()
    {
        if (currentBombs <= 0) return;

        GameObject.Instantiate(bombPrefab, bombSpawnPoint.position, Quaternion.identity);
        currentBombs--;
        OnUseBomb?.Invoke();
    }
}

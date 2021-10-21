using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombWeapon : MonoBehaviour
{
    [SerializeField]
    private Bomb bombPrefab = null;
    [SerializeField]
    private Transform bombSpawnPoint = null;

    public void SpawnBomb()
    {
        GameObject.Instantiate(bombPrefab, bombSpawnPoint.position, Quaternion.identity);
    }
}

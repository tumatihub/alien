using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField]
    private float timer = 2f;
    private float countdown = 0f;
    [SerializeField]
    private float explosionRadius = 1.4f;
    private bool exploded = false;

    void Start()
    {
        countdown = timer;
    }

    void Update()
    {
        if (countdown > 0)
        {
            countdown -= Time.deltaTime;
        }
        else if (!exploded)
        {
            Explode();
        }
    }

    private void Explode()
    {
        exploded = true;
        Collider2D[] destructibleWalls = Physics2D.OverlapCircleAll(transform.position, explosionRadius, LayerMask.GetMask("DestructibleWall"));
        if (destructibleWalls.Length > 0)
        {
            foreach(Collider2D collider in destructibleWalls)
            {
                DestructibleWall wall = collider.GetComponent<DestructibleWall>();
                wall?.Destroy();
            }
        }
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}

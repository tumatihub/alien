using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] float timer = 2f;
    float countdown = 0f;
    [SerializeField] float explosionRadius = 1.4f;
    bool exploded = false;

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

    void Explode()
    {
        exploded = true;
        Collider2D[] destructibleWalls = Physics2D.OverlapCircleAll(
            transform.position, 
            explosionRadius, 
            LayerMask.GetMask("DestructibleWall")
        );
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

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}

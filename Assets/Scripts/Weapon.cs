using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] Transform shockPosition = null;

    [SerializeField] float cooldownTime = 1f;
    float countdown = 0f;

    [SerializeField] SpriteRenderer shock = null;


    void Update()
    {
        if(countdown > 0)
        {
            countdown -= Time.deltaTime;
        }    
    }

    public void Shoot()
    {
        if(countdown <= 0)
        {
            GameObject.Instantiate(shock, shockPosition.position, Quaternion.identity);
            countdown = cooldownTime;
        }
    }
}

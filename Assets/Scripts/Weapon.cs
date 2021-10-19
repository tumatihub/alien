using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    private Transform shockPosition = null;

    [SerializeField]
    private float cooldownTime = 1f;
    private float countdown = 0f;

    [SerializeField]
    private SpriteRenderer shock = null;


    private void Update()
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

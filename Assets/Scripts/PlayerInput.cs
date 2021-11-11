using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Weapon))]
[RequireComponent(typeof(BombWeapon))]
[RequireComponent(typeof(Engine))]
public class PlayerInput : MonoBehaviour
{
    private float horizontalInput = 0;
    
    [SerializeField]
    private Transform shipSprite = null;

    private Weapon weapon;
    private BombWeapon bombWeapon;

    private Engine engine;


    private void Awake()
    {
        weapon = GetComponent<Weapon>();
        bombWeapon = GetComponent<BombWeapon>();
        engine = GetComponent<Engine>();
    }

    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Fire2"))
        {
            weapon.Shoot();
        }

        if (Input.GetButtonDown("Fire3"))
        {
            bombWeapon.SpawnBomb();
        }

        if (Input.GetButtonDown("Fire1")) engine.StartEngine();
        if (Input.GetButtonUp("Fire1")) engine.StopEngine();

        engine.Move(horizontalInput);
        RotateShip();
    }


    private void RotateShip()
    {
        if(horizontalInput > 0)
        {
            shipSprite.rotation = Quaternion.Euler(0, 0, -15);
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (horizontalInput < 0)
        {
            shipSprite.rotation = Quaternion.Euler(0, 0, 15);
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            shipSprite.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

}

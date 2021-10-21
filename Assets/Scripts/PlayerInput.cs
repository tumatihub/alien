using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Weapon))]
[RequireComponent(typeof(BombWeapon))]
public class PlayerInput : MonoBehaviour
{
    private Rigidbody2D rb = null;
    [SerializeField]
    private float force = 1000f;
    private float horizontalInput = 0;
    private float speed = 5f;
    
    [SerializeField]
    private Transform shipSprite = null;

    [SerializeField]
    private float rotationSpeed = 10f;
    private bool IsTouchDevice = false;

    private Weapon weapon;
    private BombWeapon bombWeapon;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        weapon = GetComponent<Weapon>();
        bombWeapon = GetComponent<BombWeapon>();
    }

    private void Update()
    {
        if (IsTouchDevice)
        {
            horizontalInput = Input.acceleration.x;
        }
        else
        {
            horizontalInput = Input.GetAxis("Horizontal");
        }

        if (Input.GetButtonDown("Fire2"))
        {
            weapon.Shoot();
        }

        if (Input.GetButtonDown("Fire3"))
        {
            bombWeapon.SpawnBomb();
        }

    }

    private void FixedUpdate()
    {
        //Rotate();
        
        Move();

        if (Input.GetButton("Fire1") || Input.touchCount > 0)
        {
            if (!IsTouchDevice && Input.touchCount > 0) IsTouchDevice = true;
            rb.AddForce(transform.up * force * Time.fixedDeltaTime);
        }

    }

    private void Move()
    {
        Vector2 newPos = new Vector2(
            transform.position.x + horizontalInput * speed * Time.fixedDeltaTime,
            transform.position.y
        );
        transform.position = newPos;
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

    private void Rotate()
    {
        rb.MoveRotation(rb.rotation + Time.fixedDeltaTime * rotationSpeed * -horizontalInput);
    }
}

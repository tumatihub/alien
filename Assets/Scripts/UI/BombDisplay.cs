using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BombDisplay : MonoBehaviour
{
    Stack<Image> bombs = new Stack<Image>();

    [SerializeField] Image bombImagePrefab = null;

    void Awake()
    {
        BombWeapon.OnStartBombWeapon += HandleStartBombWeapon;
        BombWeapon.OnUseBomb += HandleUseBomb;
    }

    void HandleUseBomb()
    {
        Image bomb = bombs.Pop();
        Destroy(bomb);
    }

    void HandleStartBombWeapon(int totalBombs)
    {
        InitDisplay(totalBombs);
    }

    void InitDisplay(int totalBombs)
    {
        for (int i = 0; i < totalBombs; i++)
        {
            Image bomb = Instantiate(bombImagePrefab, transform);
            bombs.Push(bomb);
        }
    }

    void OnDestroy()
    {
        BombWeapon.OnStartBombWeapon -= HandleStartBombWeapon;
        BombWeapon.OnUseBomb -= HandleUseBomb;
    }
}

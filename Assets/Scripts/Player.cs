using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Engine))]
public class Player : MonoBehaviour
{
    void Awake()
    {       
        Engine.OnEndFuel += HandleEndFuel;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("enemy"))
        {
            ResetScene();
        }
    }

    void HandleEndFuel()
    {
        ResetScene();
    }

    void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void OnDestroy()
    {
        Engine.OnEndFuel -= HandleEndFuel;
    }
}

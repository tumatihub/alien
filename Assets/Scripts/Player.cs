using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Engine))]
public class Player : MonoBehaviour
{
    private Engine engine = null;

    private void Awake()
    {
        engine = GetComponent<Engine>();
        engine.OnEndFuel += HandleEndFuel;
        engine.OnCosumeFuel += HandleConsumeFuel;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("enemy"))
        {
            ResetScene();
        }
    }

    private void HandleConsumeFuel(float currentFuel)
    {
        Debug.Log(currentFuel + " / " + engine.TotalFuel);
    }

    private void HandleEndFuel()
    {
        ResetScene();
    }

    private void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

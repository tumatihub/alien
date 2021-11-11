using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Engine : MonoBehaviour
{
    Rigidbody2D rb = null;
    [SerializeField] float force = 1000f;
    float speed = 5f;


    [SerializeField] float totalFuel = 100f;
    public float TotalFuel => totalFuel;
    float currentFuel = 0f;
    float consumptionRateInSeconds = 1f;
    float fuelCost = 10f;
    float countdown = 0f;

    public static event Action<float, float> OnCosumeFuel;
    public static event Action OnEndFuel;

    IEnumerator engineCoroutine;

    void Awake()
    {
        engineCoroutine = EngineCoroutine();
        currentFuel = totalFuel;
        countdown = consumptionRateInSeconds;
        rb = GetComponent<Rigidbody2D>();
    }

    public IEnumerator EngineCoroutine()
    {
        while (currentFuel > 0)
        {
            if (countdown > 0)
            {
                countdown -= Time.fixedDeltaTime;
            }
            else
            {
                ConsumeFuel();
                countdown = consumptionRateInSeconds;
            }
            rb.AddForce(transform.up * force * Time.fixedDeltaTime);
            yield return new WaitForFixedUpdate();
        }
        OnEndFuel?.Invoke();
    }

    void ConsumeFuel()
    {
        if (currentFuel > 0)
        {
            currentFuel -= fuelCost;
            OnCosumeFuel?.Invoke(currentFuel, totalFuel);
        }
    }

    public void StartEngine()
    {
        if (currentFuel <= 0) return;

        StartCoroutine(engineCoroutine);
    }

    public void StopEngine()
    {
        StopCoroutine(engineCoroutine);
    }

    public void Move(float horizontalInput)
    {
        if (horizontalInput == 0) return;

        Vector2 dir = Vector2.right * horizontalInput;
        
        transform.Translate(dir * speed * Time.deltaTime);
    }
}

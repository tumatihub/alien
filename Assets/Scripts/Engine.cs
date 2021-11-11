using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Engine : MonoBehaviour
{
    private Rigidbody2D rb = null;
    [SerializeField]
    private float force = 1000f;
    private float speed = 5f;


    [SerializeField]
    private float totalFuel = 100f;
    public float TotalFuel => totalFuel;
    private float currentFuel = 0f;
    private float consumptionRateInSeconds = 1f;
    private float fuelCost = 10f;
    private float countdown = 0f;

    public static event Action OnEndFuel;
    public static event Action<float, float> OnCosumeFuel;

    private IEnumerator engineCoroutine;

    private void Awake()
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

    private void ConsumeFuel()
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

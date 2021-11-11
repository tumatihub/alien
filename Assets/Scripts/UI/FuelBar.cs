using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class FuelBar : MonoBehaviour
{
    Slider fuelBar = null;

    void Awake()
    {
        fuelBar = GetComponent<Slider>();
        fuelBar.value = 1;
        Engine.OnCosumeFuel += HandleConsumeFuel;
    }

    public void HandleConsumeFuel(float currentFuel, float totalFuel)
    {
        fuelBar.value = currentFuel / totalFuel;
    }

    void OnDestroy()
    {
        Engine.OnCosumeFuel -= HandleConsumeFuel;
    }
}

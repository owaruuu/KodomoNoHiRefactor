using System;
using System.Collections;
using UnityEngine;


public class ChargeBar : MonoBehaviour
{
    public Action<float> onChargeChanged;

    private float charge;
    private float maxCharge;
    private float mincharge;
    private float startingCharge;
    private float amounToAdd;

    private bool functioning;

    private float timeToStartDecreasing;
    private float amountToDecrease;
    private float maxAmountToDecrease;
    private float decreaseStep;
    private float currentChargeDecreaseMultiplier;

    public float Charge
    {
        get { return charge; }
        set 
        { 
            charge = value;
            onChargeChanged?.Invoke(value);
        }
    }

    public float MaxCharge 
    { 
        get => maxCharge; 
    }
    
    public float Mincharge 
    { 
        get => mincharge; 
    }
    
    public float StartingCharge 
    { 
        get => startingCharge; 
    }
    
    public float AmountToAdd
    {
        get => amounToAdd; 
    }

    private void Awake()
    {
        charge = startingCharge;
        onChargeChanged?.Invoke(charge);
    }

    private IEnumerator ChargeDrain()
    {
        while (functioning)
        {
            if (timeToStartDecreasing <= 0)
            {
                charge -= amountToDecrease * Time.deltaTime;
                onChargeChanged?.Invoke(charge);
            }

            yield return null;
        }      
    }
}

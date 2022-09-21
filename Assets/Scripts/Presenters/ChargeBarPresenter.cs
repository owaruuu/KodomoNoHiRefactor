using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChargeBarPresenter : MonoBehaviour
{
    [SerializeField] private ChargeBar chargeBar;
    [SerializeField] private Slider chargeBarSlider;
    
    private void Awake()
    {
        if(chargeBar == null)
        {
            Debug.LogWarning(
                "ChargeBar presenter needs a ChargeBar to present, please make sure one is set in the Inspector", gameObject);
            enabled = false;
        }

        if(chargeBarSlider == null)
        {
            Debug.LogWarning("Charge Bar Presenter needs a Slider to Update, please make sure one is set in The Inspector", gameObject);
            enabled=false;
        }
    }

    private void OnEnable()
    {
        chargeBar.onChargeChanged += OnChargeChanged;
    }

    private void OnDisable()
    {
        chargeBar.onChargeChanged -= OnChargeChanged;
    }

    private void OnChargeChanged(float charge)
    {
        chargeBarSlider.value = charge;
    }
}

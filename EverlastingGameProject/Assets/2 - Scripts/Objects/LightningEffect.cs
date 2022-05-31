using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightningEffect : MonoBehaviour
{
    private Light2D myLight;

    public float lightningRepeatTime;

    [Range(0f, 10f)]
    [SerializeField]
    private float intensity;

    [Range(0f, 10f)]
    [SerializeField]
    private float highFlickerMin;
    [Range(0f, 10f)]
    [SerializeField]
    private float highFlickerMax;

    [Range(0f, 10f)]
    [SerializeField]
    private float lowFlickerMin;
    [Range(0f, 10f)]
    [SerializeField]
    private float lowFlickerMax;


    private void Start()
    {
        myLight = GetComponent<Light2D>();

        InvokeRepeating("Strike", 3f, lightningRepeatTime);
    }

    private void Strike()
    {
        HighFlicker();
        Invoke("LowFlicker", 0.1f);
        Invoke("ResetLight", 0.2f);
        Invoke("HighFlicker", 0.7f);
        Invoke("LowFlicker", 0.8f);
        Invoke("ResetLight", 0.9f);
    }

    private void ResetLight()
    {
        myLight.intensity = intensity;
    }

    private void HighFlicker()
    {
        myLight.intensity = Random.Range(highFlickerMin, highFlickerMax);
    }

    private void LowFlicker()
    {
        myLight.intensity = Random.Range(lowFlickerMin, lowFlickerMax);
    }


}

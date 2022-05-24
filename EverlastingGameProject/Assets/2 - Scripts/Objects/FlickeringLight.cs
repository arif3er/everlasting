using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FlickeringLight : MonoBehaviour
{

    private Light2D myLight;

    [Range(0f, 10f)]
    [SerializeField]
    private float minIntensity;
    [Range(0f, 10f)]
    [SerializeField]
    private float maxIntensity;

    [Range(0f, 10f)]
    [SerializeField]
    private float minInnerRadius;
    [Range(0f, 20f)]
    [SerializeField]
    private float maxInnerRadius;

    [Range(0f, 10f)]
    [SerializeField]
    private float minOuterRadius;
    [Range(0f, 10f)]
    [SerializeField]
    private float maxOuterRadius;

    [Range(0f, 1f)]
    [SerializeField]
    private float minFalloffStrength;
    [Range(0f, 1f)]
    [SerializeField]
    private float maxFalloffStrength;


    private void Start()
    {
        myLight = GetComponent<Light2D>();
    }

    private void Flicker()
    {
        myLight.intensity = Random.Range(minIntensity, maxIntensity);
        myLight.pointLightInnerRadius = Random.Range(minInnerRadius, maxInnerRadius);
    }







}

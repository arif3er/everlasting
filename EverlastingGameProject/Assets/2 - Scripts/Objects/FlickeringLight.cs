using System.Reflection;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FlickeringLight : MonoBehaviour
{
    private Light2D myLight;
    private static FieldInfo m_FalloffField = typeof(Light2D).GetField("m_FalloffIntensity", BindingFlags.NonPublic | BindingFlags.Instance);

    public float repeatTime;

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
        InvokeRepeating("Flicker", 0f, repeatTime);

    }


    private void Flicker()
    {
        myLight.intensity = Random.Range(minIntensity, maxIntensity);
        myLight.pointLightInnerRadius = Random.Range(minInnerRadius, maxInnerRadius);
        myLight.pointLightOuterRadius = Random.Range(minOuterRadius, maxOuterRadius);
        m_FalloffField.SetValue(myLight, Random.Range(minFalloffStrength,maxFalloffStrength));
    }







}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class BeholderStart : MonoBehaviour
{
    public GameObject beforeBossStart;
    public GameObject afterBossStart;

    public Light2D globalLight;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            globalLight.intensity = 20;
            beforeBossStart.SetActive(false);
            afterBossStart.SetActive(true);
            Invoke("ResetGlobalLight", 0.3f);
        }
    }

    private void ResetGlobalLight()
    {
        globalLight.intensity = 0.3f;
        Destroy(this);
    }
}

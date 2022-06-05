using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class BeholderEnd : MonoBehaviour
{
    public GameObject beforeBossStart;
    public GameObject afterBossStart;

    public Light2D globalLight;

    private NpcStats npcStats;

    private void Start()
    {
        npcStats = GetComponent<NpcStats>();
    }

    private void Update()
    {
        if(npcStats.currentHealth <= 0)
        {
            globalLight.intensity = 20;
            beforeBossStart.SetActive(true);
            afterBossStart.SetActive(false);
            Invoke("ResetGlobalLight", 0.3f);
        }
    }

    private void ResetGlobalLight()
    {
        globalLight.intensity = 0.3f;
        Destroy(this);
    }
}

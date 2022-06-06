using UnityEngine;

public class BeholderEnd : MonoBehaviour
{
    public GameObject beforeBossStart;
    public GameObject afterBossStart;

    private NpcStats npcStats;

    private void Start()
    {
        npcStats = GetComponent<NpcStats>();
    }

    private void Update()
    {
        if(npcStats.currentHealth <= 0)
        {
            Invoke("ResetScene", 2f);
        }
    }

    private void ResetScene()
    {
        beforeBossStart.SetActive(true);
        afterBossStart.SetActive(false);
        Destroy(this);
    }
}

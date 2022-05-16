using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedNpcAI : MonoBehaviour
{
    public float speed;                     // Hareket hizi
    public float stoppingDistance;          // Player'a yaklasirken durma mesafesi

    private float timeBetweenShots;         // Saldirilar arasindaki bekleme suresi (cooldown) (içerde degistirilen degisken)
    public float startTimeBetweenShots;     // Saldirilar arasindaki bekleme suresi (cooldown) (baslangicta ayarlanan referans degisken)

    private Transform player;               // Player'in koordianti
    public Transform firePosition;          // Merminin spawn oldugu koordinat

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        timeBetweenShots = startTimeBetweenShots;
    }

    void Update()
    {
        // Player'dan uzaktaysa player'a dogru ilerler, stoppingDistanece'a ulasinca durur.
        if (Vector2.Distance(transform.position, player.position) > stoppingDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        } else if(Vector2.Distance(transform.position, player.position) < stoppingDistance)
        {
            transform.position = this.transform.position;
        } 

        // Bekleme süresi dolduysa mermiyi çagirir (saldirir) ve bekleme süresi yenilenir, dolmadiysa bekleme süresi azalmaya devam eder.
        if(timeBetweenShots <= 0)
        {
            GameObject obj = ObjectPooler.current.GetPooledObject();

            if (obj == null) return;

            obj.transform.position = firePosition.position;
            obj.transform.rotation = firePosition.rotation;
            obj.SetActive(true);

            timeBetweenShots = startTimeBetweenShots;
        }
        else
        {
            timeBetweenShots -= Time.deltaTime;
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayerStat : MonoBehaviour
{
    //public GameObject healthBar;
    public float health,healthBarAnimSpeed;
    private float maxHealth,trueScale;
    void Start()
    {
        maxHealth = health;
    }

    void Update()
    {
        trueScale = health / maxHealth;
        #region
        // Can azalmas?n?n oldu?u kod parças?
        if (transform.localScale.x > trueScale)
        {
            transform.localScale = new Vector3(transform.localScale.x - ((transform.localScale.x-trueScale)/healthBarAnimSpeed), transform.localScale.y, transform.localScale.z); 
        }
        // Burada Input.GetKeydown yani n ye bast??? anda azalmas? yerine hasar ald??? zaman? yazaca??z. Triggerland??? zaman çal??acak.
        if (Input.GetKeyDown("n") && health>0)
        {
            health -= 10;
        }
        /*
        if (health == 0)
        {
          // ölme animasyonlar? çal??acak
        }
        */
        #endregion
        #region
        // can dolumu yani pot içti?i zaman çal??acak kod
        if (transform.localScale.x < trueScale)
        {
            transform.localScale = new Vector3(transform.localScale.x + ((trueScale-transform.localScale.x) / healthBarAnimSpeed), transform.localScale.y, transform.localScale.z);
        }
        if (health > maxHealth)
        {
            health = maxHealth;
        }
        // Burada pot içti?imizde tetiklenecek
        if (Input.GetKeyDown("m") && health<maxHealth)
        {
            health +=10; // kaç can?m?z? doldurmak istiyorsak maxHealth'a e?itlersek can?m?z fullenir.
        }
        #endregion

      

    }
}

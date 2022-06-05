using UnityEngine;
using UnityEngine.SceneManagement;

public class Stairs : MonoBehaviour
{
    SceneManager sceneManager;
    public int sceneIndex;
    public ItemSaves itemSaves;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (itemSaves.hasKey == 1)
            {
                SceneManager.LoadScene(sceneIndex);
            }
            else
            {
                Debug.Log("Anahtar?n yok..");
            }
        }
    }
}

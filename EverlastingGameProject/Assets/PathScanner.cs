using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathScanner : MonoBehaviour
{

    public float scanTime;

    private void Start()
    {
        StartCoroutine(Scan());
    }

    IEnumerator Scan()
    {
        AstarPath.active.Scan();
        yield return new WaitForSeconds(scanTime);
        StartCoroutine(Scan());
    }
}

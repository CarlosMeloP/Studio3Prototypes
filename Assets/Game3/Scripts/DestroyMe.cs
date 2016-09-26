using UnityEngine;
using System.Collections;

public class DestroyMe : MonoBehaviour
{
    public static float timeToDestroy;

    // Use this for initialization
    void Start()
    {
       
        StartCoroutine(DestroyThis());

    }

    IEnumerator DestroyThis()
    {
        yield return new WaitForSeconds(timeToDestroy);
        Destroy(this.gameObject);
    }
}


using UnityEngine;
using System.Collections;

public class Fruit : MonoBehaviour {


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("BOARD"))
        {
            var collisionPos = collision.contacts[0].point;


            Destroy(gameObject);
        }
    }
}

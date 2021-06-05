using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projetil : MonoBehaviour
{
    public float DestroyTimer = 3;

    void Start() {
        Destroy(gameObject, DestroyTimer);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) {
            /*if ( other.GetComponentInParent<PlayerControl>().shield.activeInHierarchy ) {
                other.GetComponentInParent<PlayerControl>().shield.SetActive(false);
            } else {
                player.TomouDano();
            }*/

            other.GetComponent<PlayerControl>().TomouDano();
            Destroy(gameObject);
        }
    }
}

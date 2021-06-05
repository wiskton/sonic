using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerControl>().TomouDano();
        }
        else if(other.CompareTag("Ataque")) {
            //other.GetComponentInParent<PlayerControl>().shield.SetActive(false);
            gameObject.SetActive(false);
        }
    }
}

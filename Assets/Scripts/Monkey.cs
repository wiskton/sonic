using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monkey : Enemy
{
    public Rigidbody2D projetilPrefab;
    public float impulseForce = 5;

    public void JogarCoco() {
        Rigidbody2D tempCoco = Instantiate(projetilPrefab, transform.position, transform.rotation) as Rigidbody2D;
        tempCoco.AddForce(Vector2.left * impulseForce, ForceMode2D.Impulse);
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{

    private bool jump = false;
    public float moveForce = 20;
    public float maxSpeed = 10;
    public float jumpForce = 700;
    public Transform groundCheck;
    
    private bool grounded = false;
    private float hForce = 1;
    private bool spinDash = false;
    private Rigidbody2D rb2d;
    private bool estaVivo = true;

    private Animator anim;

    public Rigidbody2D moedasPrefab;
    public Transform moedasSpawner;
    private bool tomouDano = false;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
        
        anim.SetBool("OnGround", grounded);

        if(grounded){
            anim.SetBool("Jump", false);
        }

        anim.SetBool("SpinDash", spinDash);

        if (spinDash && !tomouDano)
        {
            hForce -= 0.01f;
            if (rb2d.velocity.x <= 1)
            {
                spinDash = false;
                hForce = 1;
            }
        }

        if (Input.GetKeyDown("space"))
        {
            Jump();
        }
        if (Input.GetKeyDown("a"))
        {
            SpinDash();
        }

    }

    private void FixedUpdate(){
        if (estaVivo){
            anim.SetFloat("Speed", rb2d.velocity.x);

            rb2d.AddForce(Vector2.right * hForce * moveForce);
            if(rb2d.velocity.x > maxSpeed){
                rb2d.velocity = new Vector2(Mathf.Sign(rb2d.velocity.x) * maxSpeed, rb2d.velocity.y);
            }

            if(jump && !tomouDano){
                anim.SetBool("Jump", true);

                rb2d.AddForce(new Vector2(0, jumpForce));
                jump = false;
            }
        }
    }

    public void Jump(){
        if(grounded){
            jump = true;
        }
    }

    public void SpinDash(){
        if(grounded){
            spinDash = true;
        }
    }

    public void TerminouDano() {
        tomouDano = false;
        hForce = 1;
    }

    public void TomouDano() {
        if (tomouDano) {
            return;
        } else if (estaVivo && !tomouDano) {
            if (LevelManager.levelManager.GetMoedas() > 0) {
                tomouDano = true;
                spinDash = false;
                jump = false;
                anim.SetBool("Jump", false);
                rb2d.velocity = Vector2.zero;
                anim.SetTrigger("Dano");
                rb2d.AddForce(new Vector2(-10, 10), ForceMode2D.Impulse);
                hForce = 0;

                int totalDeMoedas = LevelManager.levelManager.GetMoedas();
                if (totalDeMoedas >= 10) {
                    totalDeMoedas = 10;
                }
                LevelManager.levelManager.ResetMoedas();
                for (int i = 0; i < totalDeMoedas; i++)
                {
                    Rigidbody2D tempMoeda = Instantiate(moedasPrefab, moedasSpawner.position, Quaternion.identity) as Rigidbody2D;
                    int randomForceX = Random.Range(-20, 5);
                    int randomForceY = Random.Range(1, 10);
                    tempMoeda.AddForce(new Vector2(randomForceX, randomForceY), ForceMode2D.Impulse);
                }
            } else {
                Morreu();
            }
        }
    }

    public void Morreu() {
        if (estaVivo) {
            estaVivo = false;
            spinDash = false;
            jump = false;
            anim.SetBool("Jump", false);
            rb2d.velocity = Vector2.zero;
            rb2d.AddForce(Vector2.up * 20, ForceMode2D.Impulse);
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<CircleCollider2D>().enabled = false;
            anim.SetBool("Morreu", true);
        }
    }
}

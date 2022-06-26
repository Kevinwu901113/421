using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public int health;
    public int damage;
    public int speed;
    protected Animator Anim;
    public Transform playerTrans;
    public Rigidbody2D rb;
    public float attackScope;
    public Vector2 movement;
    public bool isHurt;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        Anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    public void Update()
    {
        if(isHurt == false)
            MoveMent();

        switchAnim();
    }

    public void MoveMent()
    {
        movement = (playerTrans.position - transform.position);
        if (System.Math.Abs(movement.x) <= attackScope && System.Math.Abs(movement.y) <= 0.1)
        {
            Anim.SetBool("attack", true);
            rb.velocity = new Vector2(0, 0);
            return;
        }

        Anim.SetBool("attack", false);

        if (movement.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if(movement.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        rb.velocity = speed * movement.normalized;
    }

    public void death()
    {
        Destroy(gameObject);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
            Anim.SetTrigger("death");

        Anim.SetBool("isHurt", true);
        rb.velocity = new Vector2((-movement.x)/Mathf.Abs(movement.x)*0.8f, 0.0f);
        isHurt = true;

        Debug.Log(health);
    }

    public void switchAnim()
    {
        Anim.SetFloat("speed", rb.velocity.magnitude);

        if(isHurt)
        {
            Debug.Log(rb.velocity.x);
            rb.AddForce(new Vector2((-rb.velocity.x/Mathf.Abs(rb.velocity.x)) * 0.1f, 0.0f));
            if(Mathf.Abs(rb.velocity.x) < 0.1f)
            {
                isHurt = false;
                Anim.SetBool("isHurt", false);
            }
        }
    }
}

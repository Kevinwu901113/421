using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public int health;
    public int damage;
    protected Animator Anim;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        Anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    public void Update()
    {

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
        Debug.Log(health);
    }
}

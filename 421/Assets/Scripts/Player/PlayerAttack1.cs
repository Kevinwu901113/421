using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack1 : MonoBehaviour
{
    public int damage;

    private Animator anim;
    private PolygonCollider2D coll2D;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInParent<Animator>();
        coll2D = GetComponent<PolygonCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
            collision.GetComponent<Enemy>().TakeDamage(damage);
    }
}

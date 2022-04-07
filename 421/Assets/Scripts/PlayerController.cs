using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;

    Vector2 movement;
    public float speed;
    public int attackMode;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (attackMode == 0)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
        }
        else
        {
            movement.x = 0;
            movement.y = 0;
        }

        AnimatorStateInfo info = anim.GetCurrentAnimatorStateInfo(0);
        if(attackMode != 0 && info.normalizedTime >= 1.0f)
            attackMode = 0;

        // 调整人物方向
        if (movement.x != 0)
            transform.localScale = new Vector3(movement.x, 1, 1);

        // 判断攻击
        if (Input.GetKeyDown(KeyCode.J))
            attackMode = 1;

        SwitchAnim();
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }

    void SwitchAnim()
    {
        // 设置为movement向量的大小
        anim.SetFloat("speed", movement.magnitude);
        anim.SetInteger("attack", attackMode);
    }
}

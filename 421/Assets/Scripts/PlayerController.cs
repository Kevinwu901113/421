using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;
    AnimatorStateInfo stateInfo;

    Vector2 movement;
    Vector2 spMovement;
    public float speed;
    public int attackMode;
    public int sp;
    private float speedFactor;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        stateInfo = anim.GetCurrentAnimatorStateInfo(0);

        /*if (attackMode == 0)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
        }
        else
        {
            movement.x = 0;
            movement.y = 0;
        }*/

        //AnimatorStateInfo info = anim.GetCurrentAnimatorStateInfo(0);
       //if(attackMode != 0 && info.normalizedTime >= 1.0f)
            //attackMode = 0;

        // 调整人物方向
        //if (movement.x != 0)
            //transform.localScale = new Vector3(movement.x, 1, 1);


        HandleInput();
        SwitchAnim();
    }

    private void FixedUpdate()
    {
        if (stateInfo.IsName("run"))
            speedFactor = 1.0f;
        // 攻击状态下小幅度移动
        else if (stateInfo.IsName("attack_1"))
            speedFactor = 0.2f;
        else if (stateInfo.IsName("attack_2"))
            speedFactor = 0.7f;
        else
            return;

        rb.MovePosition(rb.position + movement * speed * speedFactor * Time.fixedDeltaTime);
    }

    void SwitchAnim()
    {
        // 设置为movement向量的大小
        anim.SetFloat("speed", movement.magnitude);
    }

    void HandleInput()
    {
        // 移动
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // 调整人物方向
        if (movement.x != 0)
            transform.localScale = new Vector3(movement.x, 1, 1);

        // 攻击动画播放完
        if ((stateInfo.IsName("attack_1") || stateInfo.IsName("attack_2")||stateInfo.IsName("attack_3") || stateInfo.IsName("sp_attack"))
            && stateInfo.normalizedTime > 1.0f)
        {
            attackMode = 0;
            anim.SetInteger("attack", attackMode);
        }

        // 按下J攻击
        if(Input.GetKeyDown(KeyCode.J))
        {
            HandleAttack();
        }

        // 按下K使用sp攻击
        if(Input.GetKeyDown(KeyCode.K))
        {
            HandleSpAttack();
        }
    }

    void HandleAttack()
    {
        // 如果处于idle状态，则进入攻击状态1
        if((stateInfo.IsName("idle") || stateInfo.IsName("run")) && attackMode == 0)
        {
            attackMode = 1;
            anim.SetInteger("attack", attackMode);
        }
        // 如果处于攻击状态1且动画播放进度小于80%，此时按下攻击键可过渡到攻击阶段2
        else if(stateInfo.IsName("attack_1") && attackMode == 1 && stateInfo.normalizedTime < 0.8f)
        {
            attackMode = 2;
        }
        // 同上
        else if(stateInfo.IsName("attack_2") && attackMode == 2 && stateInfo.normalizedTime < 0.8f)
        {
            attackMode = 3;
        }
    }

    void HandleSpAttack()
    {
        if (sp == 0 || attackMode == 4)
            return;
        attackMode = 4;
        sp = sp - 1;
        anim.SetInteger("attack", attackMode);
    }
    
    // 在攻击动画相应的关键帧进行攻击模式切换，可以使攻击动画更加流畅
    void GoToNextAttackAction()
    {
        anim.SetInteger("attack", attackMode);
    }

    void SpAttackMovement()
    {
        spMovement.x = transform.localScale.x;
        spMovement.y = 0;
        rb.MovePosition(rb.position + spMovement * 8.0f);
    }
}

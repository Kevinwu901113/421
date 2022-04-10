using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;
    AnimatorStateInfo stateInfo;

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

        // �������﷽��
        //if (movement.x != 0)
            //transform.localScale = new Vector3(movement.x, 1, 1);


        HandleInput();
        SwitchAnim();
    }

    private void FixedUpdate()
    {
        // idle״̬��ת�����ܶ�
        if(attackMode == 0)
            rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
        else    // ����״̬��С�����ƶ�
            rb.MovePosition(rb.position + movement * (speed * 0.1f) * Time.fixedDeltaTime);
    }

    void SwitchAnim()
    {
        // ����Ϊmovement�����Ĵ�С
        anim.SetFloat("speed", movement.magnitude);
    }

    void HandleInput()
    {
        // �ƶ�
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // �������﷽��
        if (movement.x != 0)
            transform.localScale = new Vector3(movement.x, 1, 1);

        // ��������������
        if ((stateInfo.IsName("attack_1") || stateInfo.IsName("attack_2")||stateInfo.IsName("attack_3"))
            && stateInfo.normalizedTime > 1.0f)
        {
            attackMode = 0;
            anim.SetInteger("attack", attackMode);
        }

        // ����J����
        if(Input.GetKeyDown(KeyCode.J))
        {
            HandleAttack();
        }
    }

    void HandleAttack()
    {
        // �������idle״̬������빥��״̬1
        if((stateInfo.IsName("idle") || stateInfo.IsName("run")) && attackMode == 0)
        {
            attackMode = 1;
            anim.SetInteger("attack", attackMode);
        }
        // ������ڹ���״̬1�Ҷ������Ž���С��80%����ʱ���¹������ɹ��ɵ������׶�2
        else if(stateInfo.IsName("attack_1") && attackMode == 1 && stateInfo.normalizedTime < 0.8f)
        {
            attackMode = 2;
        }
        // ͬ��
        else if(stateInfo.IsName("attack_2") && attackMode == 2 && stateInfo.normalizedTime < 0.8f)
        {
            attackMode = 3;
        }
    }
    
    // �ڹ���������Ӧ�Ĺؼ�֡���й���ģʽ�л�������ʹ����������������
    void GoToNextAttackAction()
    {
        anim.SetInteger("attack", attackMode);
    }
}
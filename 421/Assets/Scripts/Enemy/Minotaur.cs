using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minotaur : Enemy
{
    public bool isHurt;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        Anim = GetComponent<Animator>();
        health = 10;
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }
    
}

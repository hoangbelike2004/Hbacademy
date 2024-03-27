using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Charater
{
    // Start is called before the first frame update
    [SerializeField] private float AttackRange;
    [SerializeField] private float MoveSpeed;
    [SerializeField] private Rigidbody2D rb2;
    [SerializeField] private GameObject AttackArea;
    private Istate CurrentState;
    private bool iswall = true,isDeath;
    private Charater target;
    public Charater Target => target;
    private void Update()
    {
        if(CurrentState != null)
        {
            CurrentState.OnExecute(this);
        }
    }
    public void ChangeState(Istate Newstate)
    {
        if(CurrentState != null) {
        CurrentState.OnExit(this);
        }
        CurrentState = Newstate;
        if(Newstate != null)
        {
            CurrentState.OnEnter(this);
        }
    }
    public void Moving()
    {
        ChangeAnim("run");
        rb2.velocity = transform.right * MoveSpeed;
    }
    public void StopMoving()
    {
        ChangeAnim("idle");
        rb2.velocity = Vector2.zero;
    }
    public void Attack()
    {
        ActiveAttack();
        ChangeAnim("attack");
        Invoke(nameof(DeActiveAttack), .5f);
    }
    public bool IstargetInRange()//Check if it's within range or not
    {
        if(target != null && Vector2.Distance(target.transform.position, transform.position) <= AttackRange){
            return true;
        }
        else { return false; }
    }
    public override void OnDead()
    {
        ChangeState(null);
        base.OnDead();
        isDeath = true;
        Destroy(healthBar.gameObject);
        rb2.velocity = Vector2.zero;
       
    }
    public override void OnDespawn()
    {
        base.OnDespawn();
        Debug.Log("ds2");
        Destroy(gameObject);
    }
    public override void OnInit()
    {
        base.OnInit();
        isDeath = false;
        ChangeState(new IdleState());
        DeActiveAttack();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "EnemyWall")
        {
           
            ChangeDiraction(!iswall);
        }
        
      
    }

    public void ChangeDiraction(bool iswall)
    {
        this.iswall = iswall;
        transform.rotation = iswall ? Quaternion.Euler(Vector3.zero): Quaternion.Euler(Vector3.up*180);

    }

    internal void SetTarget(Charater charater)
    {
        this.target = charater;
        if (IstargetInRange()&&!isDeath)
        {
            ChangeState(new AttackState());
        }
        else
        if(target != null)
        {
            ChangeState(new PatrolState());
        }
        else
        {
            if(!isDeath)
            ChangeState(new IdleState());
        }
    }
    private void ActiveAttack()
    {
        AttackArea.SetActive(true);
    }

    private void DeActiveAttack()
    {
        AttackArea.SetActive(false);
    }
}

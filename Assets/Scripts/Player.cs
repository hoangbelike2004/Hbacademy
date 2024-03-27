using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Charater
{
    [SerializeField] private Rigidbody2D rb;
    private bool isGrounded;
    private float KeyMove;
    [SerializeField] private Kunai kunai;
    [SerializeField] private LayerMask pla;
    [SerializeField] private float speed,height,attacktime,JumpRecover;
    [SerializeField] private Transform KunaiPos;
    [SerializeField] private GameObject AttackArea;
    private int Coin = 0;
    private Vector3 SavePoint;
    // Start is called before the first frame update

    private void Awake()
    {
        Coin = PlayerPrefs.GetInt("Coin", 0);
    }
    // Update is called once per frame
    void Update()
    {
        if (Isdead) return;
        
        CheckGrounded();
        Move();
        if(JumpRecover <= 1.1f) { JumpRecover += Time.deltaTime; }
        isGrounded = CheckGrounded();
        
            if (Input.GetKeyDown(KeyCode.Space)&& isGrounded&&JumpRecover > 1f)
            {
            JumpRecover = 0;
                Jump();
            }
            

            if (rb.velocity.x != 0 && isGrounded)
            {
                ChangeAnim("run"); 
            }
            if (Input.GetKeyDown(KeyCode.J) && isGrounded && rb.velocity.x == 0)
            {
            
                Attack();
            
            Debug.Log("idle");
             }
            if (Input.GetKeyDown(KeyCode.B) && isGrounded)
            {
                Throw();

            }
            if (isGrounded && rb.velocity.x == 0&& rb.velocity.y == 0 && KeyMove == 0)
            {
            ChangeAnim("idle");
            
            }

        if (rb.velocity.y > 0.1f)
        {
            ChangeAnim("jump");
        }
       if (rb.velocity.y < 0f && !isGrounded)
        {
            ChangeAnim("fall");
        }
        

    }
    private bool CheckGrounded()
    {
        Debug.DrawLine(transform.position,transform.position + Vector3.down*1f,Color.red);
        RaycastHit2D hit = Physics2D.Raycast(transform.position,Vector2.down,1f,pla);
        return hit.collider != null ? true : false ;
    }
    public override void OnInit()
    {
        base.OnInit();
        transform.position = SavePoint;
        ChangeAnim("idle");
        DeActiveAttack();

        JumpRecover = 0;

    }
    public override void OnDespawn()
    {
        base.OnDespawn();
        OnInit();
    }
    public override void OnDead()
    {
        base.OnDead();
        
    }
    private void Move()
    {
        KeyMove = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(speed*KeyMove, rb.velocity.y);
        if(KeyMove != 0)
        transform.rotation = Quaternion.Euler(new Vector3(0, KeyMove > 0 ? 0 : 180,0));
    }
    private void Jump()
    {
        rb.AddForce(height * Vector2.up, ForceMode2D.Impulse);
    }
    private void Attack()
    {
        ChangeAnim("attack");
        ActiveAttack();
        Invoke(nameof(DeActiveAttack), .5f);

    }
    private void Throw()
    {
        ChangeAnim("throw");
        Instantiate(kunai, KunaiPos.position, KunaiPos.rotation);
    }
    private void ResetAttack()
    {
        ChangeAnim("idle");
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Deathzon")
        {
            ChangeAnim("dead");
            Invoke(nameof(OnInit), 1f);
        }
        if(collision.tag == "Coin")
        {
            Coin++;
            PlayerPrefs.SetInt("Coin", Coin);
            UiManager.Instance.SetCoin(Coin);
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
     public void SetMove(float Horizontal)
    {
        this.KeyMove = Horizontal;
    }
    public override void SavePointValue()
    {
        SavePoint = transform.position;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charater : MonoBehaviour
{
    private float hp;
    private string currentAnim;
    [SerializeField] private Animator anim;
    [SerializeField] protected HealthBar healthBar;
    [SerializeField] protected CombatText CBtext;
    public bool Isdead => hp <= 0;
    private void Start()
    {
        OnInit();
        SavePointValue();
    }
    public virtual void OnInit()//initialization function
    {
        hp = 100;
        healthBar.OnInit(100,transform);
    }    
    public virtual void SavePointValue()
    {

    }
    public virtual void OnDespawn()//destroy function
    {
        
    }
    public void OnHit(float damage)
    {
        if(hp >= damage)
        {
            hp -= damage;
            if(hp <= damage)
            {
                hp = 0;
                OnDead();
            }
            healthBar.SetHp(hp);
            Instantiate(CBtext, transform.position + Vector3.up, Quaternion.identity).OnInit(damage);
        }
    }

    public virtual void OnDead()
    {
        ChangeAnim("dead");
        
        Invoke(nameof(OnDespawn), 1f);
    }
    protected void ChangeAnim(string nameAnim)
    {
        if (currentAnim != nameAnim)
        {
            anim.ResetTrigger(nameAnim);
            currentAnim = nameAnim;
            anim.SetTrigger(currentAnim);
        }
    }
}

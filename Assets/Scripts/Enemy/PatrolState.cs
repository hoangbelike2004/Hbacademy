using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : Istate
{
    float timer, ramdomtime;
    public void OnEnter(Enemy enemy)
    {
        timer = 0;
        ramdomtime = Random.Range(3f, 6f);
    }

    public void OnExecute(Enemy enemy)
    {
        timer += Time.deltaTime;
        if(enemy.Target != null)
        {//changes the enemy's direction to the player's direction
            enemy.ChangeDiraction(enemy.Target.transform.position.x > enemy.transform.position.x);
            if (enemy.IstargetInRange())//When the target is within range, attack and Stop moving
            {
                enemy.ChangeState(new AttackState());

            }
            else
            {
                enemy.Moving();
            }
            
        }
        else
        {
            if (timer < ramdomtime)
            {
                enemy.Moving();
            }
            else
            {
                enemy.ChangeState(new IdleState());
            }
        }
        
        
    }

    public void OnExit(Enemy enemy)
    {
        
    }
}
;
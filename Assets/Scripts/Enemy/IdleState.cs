using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class IdleState : Istate
{
    float timer,ramdomtime;
    public void OnEnter(Enemy enemy)
    {
        enemy.StopMoving();
        timer = 0;
        ramdomtime = Random.Range(2f, 4f);

    }

    public void OnExecute(Enemy enemy)
    {
        if(timer > ramdomtime)
        {
            enemy.ChangeState(new PatrolState());
        }
        timer += Time.deltaTime;
    }

    public void OnExit(Enemy enemy)
    {

    }

}

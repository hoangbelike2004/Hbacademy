using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Istate
{
    void OnEnter(Enemy enemy);//start state

    void OnExecute(Enemy enemy);//update state
    void OnExit(Enemy enemy);//exit state
}

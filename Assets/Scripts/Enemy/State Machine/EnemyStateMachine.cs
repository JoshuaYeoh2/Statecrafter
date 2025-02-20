using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{
    StateMachine sm;

    [HideInInspector] public Enemy enemy;

    void Start()
    {
        sm = new StateMachine();

        enemy=GetComponent<Enemy>();

        // STATES
        /////////////////////////////////////////////////////////////////////////////////////////

        EnemyState_Idle idle = new(this);
        EnemyState_Fighting fighting = new(this);
        EnemyState_Fleeing fleeing = new(this);

        // TRANSITIONS
        /////////////////////////////////////////////////////////////////////////////////////////

        idle.AddTransition(fighting, (timeInState) =>
        {
            if(
                enemy.closestEnemy &&
                !enemy.closestHazard
            )
            {
                return true;
            }
            return false;
        });

        idle.AddTransition(fleeing, (timeInState) =>
        {
            if(
                enemy.closestHazard
            )
            {
                return true;
            }
            return false;
        });
        
        /////////////////////////////////////////////////////////////////////////////////////////
        
        fighting.AddTransition(idle, (timeInState) =>
        {
            if(
                !enemy.closestEnemy ||
                enemy.closestHazard
            )
            {
                return true;
            }
            return false;
        });        
        
        /////////////////////////////////////////////////////////////////////////////////////////
        
        fleeing.AddTransition(idle, (timeInState) =>
        {
            if(
                !enemy.closestHazard
            )
            {
                return true;
            }
            return false;
        });        
        
        // DEFAULT
        /////////////////////////////////////////////////////////////////////////////////////////
        
        sm.SetInitialState(idle);
    }

    void Update()
    {
        sm.Tick(Time.deltaTime);
    }
}

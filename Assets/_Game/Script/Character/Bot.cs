using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bot : Character
{
    private CounterTime counterTime = new CounterTime();
    public CounterTime CounterTime { get { return counterTime; } }

    #region State
    public IState<Bot> idleState = new IdleState();
    public IState<Bot> seekState = new SeekState();
    public IState<Bot> buildState = new BuildState();
    public IState<Bot> fallState = new FallState();
    #endregion
    IState<Bot> currentState;

    [SerializeField] private NavMeshAgent agent;
    private Vector3 destionation;
    public bool IsDestionation => Vector3.Distance(destionation, TF.position) < 0.2f;



    public override void OnInit()
    {
        base.OnInit();
        destionation = TF.position;
        changState(seekState);
    }


    public void SetDestionation(Vector3 position)
    {
        agent.enabled = true;
        destionation = position;
        agent.SetDestination(destionation);
    }


    // Update is called once per frame
    void Update()
    {
        if (currentState != null)
        {
            currentState.OnExecute(this);            
        }
    }

    public void changState(IState<Bot> state )
    {
        if(currentState != null)
        {
            currentState.OnExit(this);
        }
        currentState = state;
        if(currentState != null )
        {
            currentState.OnEnter(this);
        }

    }
    internal void EnableCol()
    {
        col.enabled = true;
    }
    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if (other.CompareTag(Constants.TAG_PLAYER) || other.CompareTag(Constants.TAG_BOT))
        {
            Character character = Cache.GetCharacter(other);
            if (BrickCounts < character.BrickCounts)
            {
                Falling();
                changState(fallState);
            }
        }
    }


}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WomanInBus : NPC
{
    public UnityEvent startEvent;

    private void Start()
    {
        startEvent.Invoke();
    }

    private void Update()
    {
        //if(agent.isActiveAndEnabled) 
        animator.SetFloat("Speed", agent.velocity.magnitude);
    }

    public override void StartAction()
    {
        
    }

    public void MoveToPos()
    {
        agent.SetDestination(Vector3.zero);
    }
    
    public void MoveToPos(Transform trans)
    {
        agent.SetDestination(trans.position);
    }   
}

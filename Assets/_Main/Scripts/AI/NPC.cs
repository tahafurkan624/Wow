using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class NPC : MonoBehaviour
{
    public NavMeshAgent agent;
    public Animator animator;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    public virtual void StartAction()
    {
        Debug.Log("Default Start Action");
    }
}

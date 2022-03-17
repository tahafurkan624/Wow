using System;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    public bool canMove = true;

    private NavMeshAgent _agent;

    [SerializeField] private Animator _animatorM, _animatorF;

    public Animator AnimatorM { get => _animatorM; set => _animatorM = value; }
    public Animator AnimatorF { get => _animatorF; set => _animatorF = value; } 
    public NavMeshAgent Agent => _agent;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if(_animatorF.enabled)_animatorF.SetFloat("Speed", _agent.velocity.magnitude);
        if(_animatorM.enabled)_animatorM.SetFloat("Speed", _agent.velocity.magnitude);
    }

    public void Cry()
    {
        _animatorF.SetTrigger("Cry");
    }

    public void MoveToPoint(Vector3 position)
    {
        if(_agent.enabled) _agent.SetDestination(position);
    }
}

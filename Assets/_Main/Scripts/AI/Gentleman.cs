using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;

public class Gentleman : MonoBehaviour
{
    private Animator _animator;
    private NavMeshAgent _agent;

    public NavMeshAgent Agent => _agent;

    [SerializeField] private Transform walkPos, standUpPos;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        _animator.SetFloat("Speed", _agent.velocity.magnitude);
    }

    public void StandUp()
    {
        _animator.SetTrigger("StandUp");
        StartCoroutine(WalkDelay());
    }

    IEnumerator WalkDelay()
    {
        transform.DOMove(standUpPos.position, 1f).SetEase(Ease.OutQuad);
        yield return new WaitForSeconds(1.1f);
        _agent.enabled = true;
        _agent.SetDestination(walkPos.position);
    }
}

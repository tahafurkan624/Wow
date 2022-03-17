using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class WickedMan : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void StartTypingAnim()
    {
        transform.DOMove(transform.position + new Vector3(0.07f, 0, 0), .5f);
        _animator.SetTrigger("WomanTrigger");

        StartCoroutine(DialogueWaitDelay());
    }

    private IEnumerator DialogueWaitDelay()
    {
        yield return new WaitForSeconds(.8f);
        DialogueSystem.instance.GetChapterText();
        DialogueSystem.instance.isDialogueOpen = true;
    }

    public void StartSetBackAnim()
    {
        transform.DOMove(transform.position + new Vector3(-0.09f, 0, 0), .5f);

        StartCoroutine(DialogueWaitDelay());
    }
}

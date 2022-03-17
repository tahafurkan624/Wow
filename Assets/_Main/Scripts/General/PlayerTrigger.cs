using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PlayerTrigger : MonoBehaviour
{
    private WomanInBus _womanNPC;
    private Gentleman _gentleman;

    [SerializeField] private int triggerIndex;

    [SerializeField] private Transform sitTrans, winTrans, lambTrans, eyesTrans;

    [SerializeField] private ParticleSystem[] _particles;

    private void Awake()
    {
        _womanNPC = FindObjectOfType<WomanInBus>();
        _gentleman = FindObjectOfType<Gentleman>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.TryGetComponent(out Player player))
        {
            switch (triggerIndex)
            {
                case 0:
                    player.Agent.updateRotation = false;
                    player.transform.DORotate(new Vector3(0,180,0), 1f);
                    StartCoroutine(WomanSitSequence());
                    break;
                case 1:
                    winTrans.DORotate(new Vector3(0, 90, 0), 1f).SetEase(Ease.OutQuad);
                    StartCoroutine(WindowHitSequence());
                    break;
                case 2:
                    StartCoroutine(PlayerSitSequence(player));
                    break;
                case 3:
                    break;
                case 4:
                    StartCoroutine(MoneySequence());
                    break;
                case 5:
                    break;
            }
        }
    }

    private IEnumerator MoneySequence()
    {
        yield return new WaitForSeconds(2f);
        
    }

    private IEnumerator WindowHitSequence()
    {
        var player = FindObjectOfType<Player>();

        yield return new WaitForSeconds(.24f);
        lambTrans.DOJump(sitTrans.position, 1f, 1, 1f).SetEase(Ease.OutQuad);
        lambTrans.DOLocalRotate(new Vector3(460, 310, 180), .5f);

        yield return new WaitForSeconds(.5f);
        player.transform.DOLookAt(sitTrans.position, 1f);
        lambTrans.DOLocalRotate(new Vector3(0, 0, 0), .5f);

        yield return new WaitForSeconds(.8f);
        foreach (var particle in _particles)
        { particle.gameObject.SetActive(true); }

        yield return new WaitForSeconds(1.2f);
        StartCoroutine(EyeFadeIn(eyesTrans.GetComponent<SpriteRenderer>()));
        player.AnimatorM.SetTrigger("Suprised");

        yield return new WaitForSeconds(.6f);
        DialogueSystem.instance.GetChapterText();
        DialogueSystem.instance.isDialogueOpen = true;
    }

    private IEnumerator WomanSitSequence()
    {
        //man stand up
        yield return new WaitForSeconds(3.5f);
        DialogueSystem.instance.GetChapterText();
        DialogueSystem.instance.isDialogueOpen = true;

        _gentleman.StandUp();

        yield return new WaitForSeconds(1.5f); 
        _womanNPC.agent.SetDestination(sitTrans.position);

        yield return new WaitForSeconds(1f);
        _womanNPC.animator.SetTrigger("Sit");
        _womanNPC.agent.enabled = false;
        _womanNPC.transform.DOMove(sitTrans.position, 1f).SetEase(Ease.OutQuad);
        _womanNPC.transform.DORotate(sitTrans.rotation.eulerAngles, .6f);
    }

    private IEnumerator PlayerSitSequence(Player player)
    {
        yield return new WaitForSeconds(1.9f);
        DialogueSystem.instance.GetChapterText();
        DialogueSystem.instance.isDialogueOpen = true;

        _gentleman.StandUp();

        yield return new WaitForSeconds(1.5f); 
        player.Agent.SetDestination(sitTrans.position);

        yield return new WaitForSeconds(1f);
        player.AnimatorF.SetTrigger("Sit");
        player.Agent.enabled = false;
        player.AnimatorF.SetFloat("Speed", 1f);
        player.transform.DOMove(sitTrans.position, 1f).SetEase(Ease.OutQuad);
        player.transform.DORotate(sitTrans.rotation.eulerAngles, .6f);
    }

    IEnumerator EyeFadeIn(SpriteRenderer renderer)
    {
        float elapsedTime = 0f;
        var spriteColor = renderer.color;
        while (elapsedTime < 2f)
        {
            elapsedTime += Time.deltaTime;
            renderer.color = new Color(spriteColor.r,spriteColor.g, spriteColor.b, Mathf.Lerp(0, 1, elapsedTime / 2));
            yield return new WaitForEndOfFrame();
        }
    }
}

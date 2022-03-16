using System.Collections;
using UnityEngine;

public class Bed : Interactable
{
    private void CallBedSequence()
    {
        StartCoroutine(BedSequence());
    }

    public override void Interact(Player player)
    {
        player.MoveToPoint(_interationPos.position);
        CallBedSequence();
    }

    private IEnumerator BedSequence()
    {
        inObjEvent.Invoke();
        Debug.Log("Bed seq start");
        yield return new WaitForSeconds(1f);
        Debug.Log("Cin");
    }
}

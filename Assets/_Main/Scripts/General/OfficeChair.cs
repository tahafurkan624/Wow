using System.Collections;
using UnityEngine;

public class OfficeChair : Interactable
{
    public override void Interact(Player player)
    {
        player.MoveToPoint(_interationPos.position);
        inObjEvent.Invoke();
    }

    private IEnumerator MoneySequence()
    {
        yield return new WaitForSeconds(2f);
        
    }
}

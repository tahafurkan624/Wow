using UnityEngine;

public class RudeMan : Interactable
{
    public override void Interact(Player player)
    {
        player.Agent.enabled = true;
        player.AnimatorF.SetTrigger("Stand");
        player.AnimatorF.SetFloat("Speed", 2f);
        player.Agent.SetDestination(_interationPos.position);

        DialogueSystem.instance.GetChapterText();
        DialogueSystem.instance.isDialogueOpen = true;
    }
}

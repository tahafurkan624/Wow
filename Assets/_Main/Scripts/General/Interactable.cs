using UnityEngine;
using UnityEngine.Events;

public abstract class Interactable : MonoBehaviour
{ 
    public Transform _interationPos;

    public UnityEvent inObjEvent;

    public virtual void Interact(Player player)
    {
        Debug.Log("Default Interact");
    }
}

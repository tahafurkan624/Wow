using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private LayerMask _InteractableLayerMask;
    private Player _player;
    private Camera _mainCam;

    private void Start()
    {
        _player = FindObjectOfType<Player>();
        _mainCam = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
             TryToInteract();
        }
    }

    private void TryToInteract()
    {
        var ray = _mainCam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, 100, _InteractableLayerMask))
        {
            if (hit.transform.TryGetComponent(out Interactable interactable))
            {
                interactable.Interact(_player);
            }
            else
            {
                if(_player.canMove) MoveToPos(hit.point);
            }
        }
    }

    private void MoveToPos(Vector3 pos)
    {
        _player.MoveToPoint(pos);
        //mouse click particle play
    }
}

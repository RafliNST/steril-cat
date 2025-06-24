using UnityEngine;
using UnityEngine.Events;

[RequireComponent (typeof(Collider2D))]
public abstract class Interactable : MonoBehaviour
{
    public Sprite icon;

    public abstract void Interact();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        InteractableUI.senseInteractable?.Invoke(this);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        InteractableUI.exitInteractable?.Invoke();
    }
}

using UnityEngine;

public class AnimationInteract : Interactable
{
    [SerializeField]
    Animator animator;

    public string triggerName;

    public override void Interact()
    {
        animator.SetTrigger(triggerName);
    }
}

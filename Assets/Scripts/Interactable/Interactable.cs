using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

[RequireComponent (typeof(Collider2D))]
public class Interactable : MonoBehaviour
{
    [SerializeField, Header("For Animation")]
    string triggerName = "None";
    
    [SerializeField, Header("For Icon When Interact")]
    #nullable enable
    public Sprite? icon;
    
    [SerializeField, Header("For Scene Loader")]
    #nullable enable
    private string? sceneName;
    
    [SerializeField, Header("For Resource")]
    #nullable enable
    private ResourceScriptable? resource;

    public virtual void Interact()
    {
        Player.onAnimationPlay?.Invoke(triggerName);
    }

    private void FurtherInteract()
    {
        if (resource != null)
            resource.Consume(1);
        if (sceneName != "")
            SceneManager.LoadScene(sceneName);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        InteractableUI.senseInteractable?.Invoke(this);
        Player.onAnimationFinishied.AddListener(FurtherInteract);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        InteractableUI.exitInteractable?.Invoke();
        Player.onAnimationFinishied?.RemoveListener(FurtherInteract);
    }
}
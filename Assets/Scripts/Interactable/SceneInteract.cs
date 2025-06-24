using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneInteract : Interactable
{
    public string sceneName;

    public override void Interact()
    {
        SceneManager.LoadScene(sceneName);
    }
}

using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public PlayerInput _input;
    public float speedForce, maxSpeed;

    Rigidbody2D physicsComp;
    Animator animator;

    #region Event Action
    public static UnityEvent<string> onAnimationPlay;
    public static UnityEvent onAnimationFinishied;
    #endregion

    private void Awake()
    {
        physicsComp = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        onAnimationPlay = new UnityEvent<string>();
        onAnimationFinishied = new UnityEvent();

        onAnimationPlay.AddListener(AnimationInvoke);
    }

    private void Update()
    {
        physicsComp.transform.position += Movement() * speedForce * Time.deltaTime;
    }

    private void AnimationInvoke(string triggerName)
    {
        animator.SetTrigger(triggerName);
    }

    public void AnimationEnd()
    {
        onAnimationFinishied?.Invoke();
    }

    private Vector3 Movement()
    {
        return _input.moveAction.ReadValue<Vector2>();
    }    
}

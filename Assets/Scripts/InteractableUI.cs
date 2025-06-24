using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InteractableUI : MonoBehaviour
{
    public static UnityEvent<Interactable> senseInteractable;
    public static UnityEvent exitInteractable;

    [SerializeField] int maxIcon;
    [SerializeField] float xGap, yOffset;
    [SerializeField] GameObject iconObj;
    [SerializeField] PlayerInput _input;

    [Range(.1f, 1f)]
    [SerializeField] float visibilityThreshold;

    SpriteRenderer[] spriteRenderers;
    GameObject[] iconGO;
    Interactable[] Interactables;

    private int showedIcon;
    private float currentIcon = 0;


    private void OnEnable()
    {
        _input = GameObject.FindGameObjectWithTag("GameInput").GetComponent<PlayerInput>();
    }

    private void Start()
    {
        _input = GameObject.FindGameObjectWithTag("GameInput").GetComponent<PlayerInput>();

        transform.localPosition = new Vector3(0, yOffset, 0);

        senseInteractable = new UnityEvent<Interactable>();
        exitInteractable = new UnityEvent();

        spriteRenderers = new SpriteRenderer[maxIcon];
        iconGO = new GameObject[maxIcon];
        Interactables = new Interactable[maxIcon];

        for (int i = 0; i < maxIcon; i++)
        {
            Color color = Color.white;
            color.a = visibilityThreshold;

            iconGO[i] = Instantiate(iconObj, transform, false); // false = posisi lokal dipertahankan
            iconGO[i].transform.localPosition = Vector3.right * (float)(i + (xGap * i));
            spriteRenderers[i] = iconGO[i].GetComponent<SpriteRenderer>();
            spriteRenderers[i].color = color;
            iconGO[i].SetActive(false);
        }

        senseInteractable.AddListener(AdjustInteractables);
        exitInteractable.AddListener(RemoveIcon);

        _input.iconChangeAction.performed += ChangeIcon;
        _input.iconSelectAction.performed += SelectIcon;
    }

    void AdjustInteractables(Interactable trigger)
    {
        RenderIcon(trigger.icon);
        Interactables[showedIcon-1] = trigger;
    }

    void RenderIcon(Sprite e)
    {
        Color color = Color.white;
        color.a = visibilityThreshold;
        spriteRenderers[showedIcon].color = color;
        
        color.a = 1f;
        spriteRenderers[(int)currentIcon].color = color;
        
        iconGO[showedIcon].SetActive(true);

        var renderer = spriteRenderers[showedIcon];
        renderer.sprite = e;
        
        showedIcon++;
    }

    void RemoveIcon()
    {
        iconGO[--showedIcon].SetActive(false);        

        currentIcon = Mathf.Clamp(currentIcon, 0, Mathf.Clamp01(showedIcon - 1));
        
        Color color = Color.white;
        spriteRenderers[(int)currentIcon].color = color;
        
        transform.localPosition = new Vector3(-(currentIcon + (currentIcon * xGap)), yOffset, 0);
    }

    void ChangeIcon(InputAction.CallbackContext ctx)
    {
        Color color = Color.white;
        color.a = visibilityThreshold;
        spriteRenderers[(int)currentIcon].color = color;

        currentIcon = Mathf.Clamp(currentIcon + _input.iconChangeAction.ReadValue<float>(), 0, Mathf.Clamp01(showedIcon-1));
        transform.localPosition = new Vector3(-(currentIcon + (currentIcon * xGap)), yOffset, 0);

        color.a = 1f;
        spriteRenderers[(int)currentIcon].color = color;
    }

    void SelectIcon(InputAction.CallbackContext ctx)
    {
        if (showedIcon == 0) return;
        Interactables[(int)currentIcon].Interact();
    }
}
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "ResourceScriptable", menuName = "Scriptable Objects/ResourceScriptable")]
public class ResourceScriptable : ScriptableObject
{
    public Sprite icon;
    [SerializeField]
    private int _qty;
    [SerializeField]
    private string resourceName;

    public int qty
    {
        get
        {
            return _qty;
        }
        private set
        {
            if (qty - value < 0)
            {
                return;
            }
            _qty = value;
        }
    }

    public void Consume(int amount)
    {
        qty -= amount;
    }
}
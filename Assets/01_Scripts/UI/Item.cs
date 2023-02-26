using UnityEngine;

public class Item : MonoBehaviour
{
    public Texture2D icon;
    public event System.Action<Item> OnUseEvent;
    public bool isConsumable = false;

    public virtual void Use()
    {
        OnUseEvent?.Invoke(this);
    }
}
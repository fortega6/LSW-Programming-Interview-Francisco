using UnityEngine;

public abstract class ItemSO : ScriptableObject
{
    public Sprite icon;
    public string itemName;
    public bool isUsing;
    public RuntimeAnimatorController animator;
    public bool active = false;
}

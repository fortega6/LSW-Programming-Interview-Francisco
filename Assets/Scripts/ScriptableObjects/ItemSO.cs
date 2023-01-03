using UnityEngine;

public abstract class ItemSO : ScriptableObject
{
    public Sprite icon;
    public string itemName;
    public Animator animator;
    public bool isUsing;
}

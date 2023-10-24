using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Game/Item")]
public abstract class ItemBase : ScriptableObject
{
    public string itemName;
    public string itemInfo;
    public Sprite itemImage;

    public abstract void Initialize();
    public abstract void UseItems(Boards boards);
}

using UnityEngine;

[CreateAssetMenu(fileName = "LovePunch", menuName = "Game/LovePunch")]
public class LovePunch : ItemBase
{
    public override void Initialize()
    {
        itemName = "A punch full of love";
        itemInfo = "Cause the enemy to have their skill points refreshed (this item is completely useless when used against an enemy without skill points).";
        itemImage = Resources.Load<Sprite>("Items/Item3");;
    }

    public override void UseItems(Boards boards){
        boards.ItemsReduceSkill();
    }
}
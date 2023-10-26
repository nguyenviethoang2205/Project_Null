using UnityEngine;

// [CreateAssetMenu(fileName = "GoldPill", menuName = "Game/GoldPill")]
public class GoldPill : ItemBase
{
    public override void Initialize()
    {
        itemName = "HND-2910 Gold";
        itemInfo = "Deal an additional 3 damage for each time you damage the opponent.";
        itemImage = Resources.Load<Sprite>("Items/Item1");;
    }

    public override void UseItems(Boards boards){
        boards.PlayerUseItemAnimation();
        boards.ItemsInsertDamage(3);
    }
}
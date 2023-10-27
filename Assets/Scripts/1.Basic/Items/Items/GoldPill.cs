using UnityEngine;

// [CreateAssetMenu(fileName = "GoldPill", menuName = "Game/GoldPill")]
public class GoldPill : ItemBase
{
    public override void Initialize()
    {
        itemName = "HND-2910 Gold";
        itemInfo = "Increase the player's attack points by 2.";
        itemImage = Resources.Load<Sprite>("Items/Item1");;
    }

    public override void UseItems(Boards boards){
        boards.PlayerUseItemAnimation();
        boards.ItemsInsertDamage(2);
    }
}
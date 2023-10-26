using UnityEngine;

// [CreateAssetMenu(fileName = "BoomBaKuBum", menuName = "Game/BoomBaKuBoom")]
public class BoomBaKuBum : ItemBase
{
    public override void Initialize()
    {
        itemName = "BOOM-BA-KU-BUM";
        itemInfo = "Deal 75 damage to the enemy, but cannot reduce the enemy's health below 1.";
        itemImage = Resources.Load<Sprite>("Items/Item2");;
    }

    public override void UseItems(Boards boards){
        boards.PlayerUseItemAnimation();
        boards.ItemsDealDamage(75);
    }
}
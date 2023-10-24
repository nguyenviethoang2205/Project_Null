using UnityEngine;

[CreateAssetMenu(fileName = "BoomBaKuBum", menuName = "Game/BoomBaKuBoom")]
public class BoomBaKuBum : ItemBase
{
    public override void Initialize()
    {
        itemName = "BOOM-BA-KU-BUM";
        itemInfo = "Deal 200 damage to enemy.";
        itemImage = Resources.Load<Sprite>("Items/Item2");;
    }

    public override void UseItems(Boards boards){
        int damage = 100;
        boards.ItemsDealDamage(100);
    }
}
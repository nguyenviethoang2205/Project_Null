using UnityEngine;

public class GalacticMushroom : ItemBase
{
    public override void Initialize()
    {
        itemName = "Galactic Mushroom";
        itemInfo = "You will become 'dizzy,' but the character's attacks deal an additional 5 damage.\n(There might be a way to counteract this effect)";
        itemImage = Resources.Load<Sprite>("Items/Item10");
    }

    public override void UseItems(Boards boards){
        boards.PlayerUseItemAnimation();
        boards.ItemsInsertDamage(5);
        boards.activePiece.status = "dizzy";
    }
}
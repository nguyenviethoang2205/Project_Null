using UnityEngine;

public class AntiqueClock : ItemBase
{
    public override void Initialize()
    {
        itemName = "Antique Clock";
        itemInfo = "Reset the game to its initial state (does not refresh any skills and do not restore used items).";
        itemImage = Resources.Load<Sprite>("Items/Item11");
    }

    public override void UseItems(Boards boards){
        boards.PlayerUseItemAnimation();
        boards.ItemsRestartGame();
    }
}
using UnityEngine;

// [CreateAssetMenu(fileName = "PowerPickaxe", menuName = "Game/PowerPickaxe")]
public class PowerPickaxe : ItemBase
{
    public override void Initialize()
    {
        itemName = "Power Pickaxe";
        itemInfo = "Destroy 3 lines.";
        itemImage = Resources.Load<Sprite>("Items/Item4");;
    }

    public override void UseItems(Boards boards){
        boards.PlayerUseItemAnimation();
        boards.ItemsDestroyLine();
        boards.ItemsDestroyLine();
        boards.ItemsDestroyLine();
    }
}
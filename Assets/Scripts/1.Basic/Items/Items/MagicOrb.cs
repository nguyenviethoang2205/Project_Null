using UnityEngine;

// [CreateAssetMenu(fileName = "MagicOrb", menuName = "Game/MagicOrb")]
public class MagicOrb : ItemBase
{
    public override void Initialize()
    {
        itemName = "Magic Orb";
        itemInfo = "Destroy a piece in the queue and replace it with a random piece.";
        itemImage = Resources.Load<Sprite>("Items/Item6");;
    }

    public override void UseItems(Boards boards){
        boards.PlayerUseItemAnimation();
        boards.ItemsChangeNextPiece();
    }
}
using UnityEngine;

[CreateAssetMenu(fileName = "GachaCapsule", menuName = "Game/GachaCapsule")]
public class GachaCapsule : ItemBase
{
    public override void Initialize()
    {
        itemName = "Gacha Capsule";
        itemInfo = "Destroy the Piece you are controlling and change it into a random different Piece.";
        itemImage = Resources.Load<Sprite>("Items/Item5");;
    }

    public override void UseItems(Boards boards){
        boards.ItemsChangeControlPiece();
    }
}
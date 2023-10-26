using UnityEngine;

// [CreateAssetMenu(fileName = "BoomBaKuBum", menuName = "Game/BoomBaKuBoom")]
public class DestinyCoin : ItemBase
{
    public override void Initialize()
    {
        itemName = "Destiny Coin";
        itemInfo = "Perform one of the following:\n1.Inflict 100 damage and push 3 lines;\n2.The player's attacks deal more 5 damage and double the speed of piece drops until the end of the game.";
        itemImage = Resources.Load<Sprite>("Items/Item9");
    }

    public override void UseItems(Boards boards){
        boards.PlayerUseItemAnimation();
        int chooseRandom = Random.Range(0,2);
        if (chooseRandom == 1 ){
            boards.ItemsDealDamage(100);
            boards.MakeAGrayLine();
            boards.MakeAGrayLine();
            boards.MakeAGrayLine();
        } else {
            boards.ItemsInsertDamage(5);
            boards.dropSpeed = boards.dropSpeed / 2;
        }
    }
}
using UnityEngine;

[CreateAssetMenu(fileName = "MysteryBox", menuName = "Game/MysteryBox")]
public class MysteryBox : ItemBase
{
    public override void Initialize()
    {
        itemName = "Mystery Box";
        itemInfo = "Perform one of the following actions:\n1.Deal 1~500 damage.\n2.Destroy 1~10 Lines.\n3.Increase damage by 1~10 for each attack on the enemy.\n4.Create and push 1~10 Lines.";
        itemImage = Resources.Load<Sprite>("Items/Item7");;
    }

    public override void UseItems(Boards boards){
        int chooseRandom = Random.Range(0, 7);
        if (chooseRandom == 0){
            int damageRandom = Random.Range(1, 501);
            boards.ItemsDealDamage(damageRandom);
        } else if (chooseRandom == 1){
            int destroyRandom = Random.Range(1, 11);
            for (int i = 0; i < destroyRandom; i++){
                boards.ItemsDestroyLine();
            }
        } else if (chooseRandom == 2){
            int buffRandom = Random.Range(1, 11);
            boards.ItemsInsertDamage(buffRandom);
        } else {
            int destroyRandom = Random.Range(1, 11);
            for (int i = 0; i < destroyRandom; i++){
                boards.MakeAGrayLine();
            }
        }
        
    }
}
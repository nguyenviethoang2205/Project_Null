using UnityEngine;

// [CreateAssetMenu(fileName = "MysteryBox", menuName = "Game/MysteryBox")]
public class MysteryBox : ItemBase
{
    public override void Initialize()
    {
        itemName = "Mystery Box";
        itemInfo = "Perform one of the following actions:\n1.Deal 1~500 damage.\n2.Destroy 1~10 Lines.\n3.Increase damage by 1~10 for each attack on the enemy.\n4.Create and push 1~10 Lines.";
        itemImage = Resources.Load<Sprite>("Items/Item7");;
    }

    public override void UseItems(Boards boards){
        boards.PlayerUseItemAnimation();
        int chooseRandom = Random.Range(0, 7);
        if (chooseRandom == 0){
            int damageRangeRandom = Random.Range(1, 1000);
            int damageRandom = 0; 
            if (damageRangeRandom == 1){
                damageRandom =  Random.Range(401,501);
            } else if (damageRangeRandom >= 2 && damageRangeRandom <= 6){
                damageRandom =  Random.Range(301,401);
            } else if (damageRangeRandom >= 7 && damageRangeRandom <= 17){
                damageRandom =  Random.Range(201,301);
            } else if (damageRangeRandom >= 18 && damageRangeRandom <= 38){
                damageRandom =  Random.Range(101,201);
            } else {
                damageRandom =  Random.Range(1,101);
            }
            boards.ItemsDealDamage(damageRandom);
        } else if (chooseRandom == 1){
            int destroyRandom = Random.Range(1, 10);
            for (int i = 0; i < destroyRandom; i++){
                boards.ItemsDestroyLine();
            }
        } else if (chooseRandom == 2){
            int buffRangeRandom = Random.Range(1, 1000);
            int buffRandom = 0;
            if (buffRangeRandom == 1){
                buffRandom =  Random.Range(9,11);
            } else if (buffRangeRandom >= 2 && buffRangeRandom <= 6){
                buffRandom =  Random.Range(7,9);
            } else if (buffRangeRandom >= 7 && buffRangeRandom <= 17){
                buffRandom =  Random.Range(5,7);
            } else if (buffRangeRandom >= 18 && buffRangeRandom <= 38){
                buffRandom =  Random.Range(3,5);
            } else {
                buffRandom =  Random.Range(1,3);
            }
            boards.ItemsInsertDamage(buffRandom);
        } else {
            int destroyRandom = Random.Range(1, 10);
            for (int i = 0; i < destroyRandom; i++){
                boards.MakeAGrayLine();
            }
        }
        
    }
}
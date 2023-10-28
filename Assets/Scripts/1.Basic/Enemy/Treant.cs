using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treant: EnemyCore
{
    public override void Awake()
    {
        maxSkillWait = 100;
        skillWait = 0;
        skillBar.SetMaxSkillValue(maxSkillWait);
        skillBar.SetSkillValue(skillWait);
        CheckStatus();
        getName();
        EnemyImage = Resources.Load<Sprite>("Enemy/Treant");
        getHealth();
        getDetail();
        getDifficulty();
    }

    private bool isSkillReady = false;
    private int waitExplosive = 0;
    public void EnemySkill(){
        if (isSkillReady == false){
            skillWait += boards.damage;
            skillBar.SetSkillValue(skillWait);
            if (skillWait >= maxSkillWait){
                boards.DoEnemyAttack();
                maxSkillWait = 40;
                skillWait = maxSkillWait;
                skillBar.SetMaxSkillValue(maxSkillWait);
                skillBar.SetSkillValue(skillWait);
                waitExplosive = 10;
                isSkillReady = true;
                CheckStatus();
            }
        } else {
            skillWait -= boards.damage;
            boards.HealInt(boards.damage);
            skillBar.SetSkillValue(skillWait);
            waitExplosive -= 1;
            
            if (waitExplosive <= 0){
                for (int i = 0; i < 5; i++){
                    boards.MakeAGrayLine();
                }
                boards.HealInt(25);
                boards.DoEnemyAttack();
                maxSkillWait = 100;
                skillWait = 0;
                skillBar.SetMaxSkillValue(maxSkillWait);
                skillBar.SetSkillValue(skillWait);
                boards.CheckHealthStatus();
                isSkillReady = false;
                CheckStatus();
            } else {
                if (skillWait <= 0){
                boards.ItemsDealDamage(150);
                maxSkillWait = 100;
                skillWait = 0;
                skillBar.SetMaxSkillValue(maxSkillWait);
                skillBar.SetSkillValue(skillWait);
                isSkillReady = false;
                CheckStatus();
                }
            }
        }
    }
    public override void CheckSkillStart() { 
    }

    public override void CheckSkillSpawn() { }

    public override void CheckSkillClearLine(){
        EnemySkill();
    }

    public override string getName()
    {
        SetEnemyName("Treant Flowering");
        return EnemyName;
    }

    public override int getHealth()
    {
        SetEnemyHealth(300);
        return EnemyHealth;
    }

    public override string getDetail()
    {
        SetEnemyDetail("He is a very friendly person. However, he will show you where the real opponent is. When he receives a certain amount of damage (not caused by items), he becomes immune to the damage and creates a shield. If you destroy that shield before 10 block drops, he will take an extremely large amount of damage. If not, he will punish you.");
        return EnemyDetail;
    }

    public override string getDifficulty()
    {
        SetEnemyDifficulty("Easy");
        return EnemyDifficulty;
    }
}

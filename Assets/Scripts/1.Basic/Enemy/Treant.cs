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
        getHealth();
    }

    private bool isSkillReady = false;
    private int waitExplosive = 0;
    public void EnemySkill(){
        if (isSkillReady == false){
            skillWait += boards.damage;
            skillBar.SetSkillValue(skillWait);
            if (skillWait >= maxSkillWait){
                boards.DoEnemyAttack();
                maxSkillWait = 50;
                skillWait = maxSkillWait;
                skillBar.SetMaxSkillValue(maxSkillWait);
                skillBar.SetSkillValue(skillWait);
                waitExplosive = 10;
                isSkillReady = true;
            }
        } else {
            skillWait -= boards.damage;
            boards.HealInt(boards.damage);
            skillBar.SetSkillValue(skillWait);
            waitExplosive -= 1;
            
            if (waitExplosive <= 0){
                for (int i = 0; i < 5; i++){
                    boards.MakeAGrayLine();
                    boards.HealInt(25);
                }
                boards.DoEnemyAttack();
                maxSkillWait = 100;
                skillWait = 0;
                skillBar.SetMaxSkillValue(maxSkillWait);
                skillBar.SetSkillValue(skillWait);
                isSkillReady = false;
            } else {
                if (skillWait <= 0){
                boards.ItemsDealDamage(150);
                maxSkillWait = 100;
                skillWait = 0;
                skillBar.SetMaxSkillValue(maxSkillWait);
                skillBar.SetSkillValue(skillWait);
                isSkillReady = false;
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
}

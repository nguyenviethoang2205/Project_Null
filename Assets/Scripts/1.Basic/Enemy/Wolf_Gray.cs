using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wolf_Gray: EnemyCore
{
    public override void Awake()
    {
        maxSkillWait = 2;
        skillWait = 1;
        skillBar.SetMaxSkillValue(maxSkillWait);
        skillBar.SetSkillValue(skillWait);
        CheckStatus();
        getName();
        getHealth();
    }

    private bool Phase1 = true;
    private bool Phase2 = true;
    private bool Phase3 = true;

    public void EnemySkill(){
        if (boards.currentHealth <= (EnemyHealth / 3) && Phase3 == true){
            boards.DoEnemyAttack();
            boards.dropSpeed = boards.dropSpeed / 100 * 85;
            Phase3 = false;
        } else if (boards.currentHealth <= (EnemyHealth / 3 * 2) && Phase2 == true){
            boards.DoEnemyAttack();
            boards.dropSpeed = boards.dropSpeed / 100 * 90;
            Phase2 = false;
        } else if (boards.currentHealth == EnemyHealth && Phase1 == true){
            boards.DoEnemyAttack();
            boards.dropSpeed = boards.dropSpeed / 100 * 95;
            Phase1 = false;
        }
    }

    private bool skill2Status = true;

    public void EnemySkill2(){
        skillWait++;
        skillBar.SetSkillValue(skillWait);
        

        if (skillWait == maxSkillWait){
            if (skill2Status == true){
                boards.dropSpeed = boards.dropSpeed / 100 * 95;
                boards.MakeAGrayLine();
                skill2Status = false;
            } else {
                boards.EnemyDestroyLine();
                skill2Status = true;
            }
            boards.DoEnemyAttack();
            skillWait = 0;
            skillBar.SetSkillValue(skillWait);
        }
        CheckStatus();
    }

    public override void CheckSkillStart() { 
        EnemySkill();
    }

    public override void CheckSkillSpawn() { }

    public override void CheckSkillClearLine(){
        EnemySkill();
        EnemySkill2();
    }

    public override string getName()
    {
        SetEnemyName("Mad Dog");
        return EnemyName;
    }

    public override int getHealth()
    {
        SetEnemyHealth(200);
        return EnemyHealth;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dryad_Mage : EnemyCore
{
    public override void Awake()
    {
        /*maxSkillWait = 5;
        skillWait = 3;
        skillBar.SetMaxSkillValue(maxSkillWait);
        skillBar.SetSkillValue(skillWait);*/
        this.skillActive = false;
        this.skillTiming = Time.time;
        this.skillHealingTime = -1;
        CheckStatus();
        getName();
        getHealth();
    }

    private bool skillActive;
    private float skillTiming;
    private float skillHealingTime;
    public void EnemySkill()
    {
        if(this.skillActive == true)
            this.boards.activePiece.getStatus("dizzy");
        else
            this.boards.activePiece.getStatus("normal");
    }

    private void Update()
    {
        EnemySkill();
        if (this.skillTiming + 10f <= Time.time)
        {
            this.skillActive = true;
        }
        if (this.skillActive == true)
        {
            if (this.skillHealingTime == -1)
            {
                this.boards.Heal(1);
                this.skillHealingTime = Time.time;
            }
            else if(this.skillHealingTime + 1f<= Time.time)
            {
                this.boards.Heal(1);
                this.skillHealingTime = Time.time;
            }
        }
    }


    public override void CheckSkillStart() { }

    public override void CheckSkillSpawn() { }

    public override void CheckSkillClearLine(int totalLineClear)
    {
        if (totalLineClear != 0)
        {
            this.skillActive = false;
            this.skillTiming = Time.time;
            this.skillHealingTime = -1;
        }
    }

    public override string getName()
    {
        SetEnemyName("Dryad Mage");
        return EnemyName;
    }

    public override int getHealth()
    {
        SetEnemyHealth(300);
        return EnemyHealth;
    }
}

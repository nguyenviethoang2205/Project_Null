using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dryad_Mage : EnemyCore
{
    public override void Awake()
    {
        maxSkillWait = 0;
        skillWait = 0;
        skillBar.SetMaxSkillValue(maxSkillWait);
        skillBar.SetSkillValue(skillWait);
        this.skillActive = false;
        this.skillTiming = Time.time;
        this.skillHealingTime = -1;
        CheckStatus();
        EnemyImage = Resources.Load<Sprite>("Enemy/Dryad_Mage");
        getName();
        getHealth();
        getDetail();
        getDifficulty();
    }

    private bool skillActive;
    private float skillTiming;
    private float skillHealingTime;
    public void EnemySkill()
    {
        if(this.skillActive == true){
            boards.DoEnemyAttack();
            this.boards.activePiece.getStatus("dizzy");
            }
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

    public override void CheckSkillClearLine()
    {
        if (this.boards.totalLinesClear != 0)
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
        SetEnemyHealth(350);
        return EnemyHealth;
    }

    public override string getDetail()
    {
        SetEnemyDetail("A versatile wizard, he will make your battle much more challenging. Every 5 seconds without clearing a line, you will enter the 'dizzy' state. While you are in the 'dizzy' state, he will continuously regenerate health. You can exit the 'dizzy' state by clearing a Line.");
        return EnemyDetail;
    }

    public override string getDifficulty()
    {
        SetEnemyDifficulty("Hard");
        return EnemyDifficulty;
    }
}

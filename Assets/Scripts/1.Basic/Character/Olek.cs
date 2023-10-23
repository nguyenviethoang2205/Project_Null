using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Olek : CharacterCore
{
    public override void Awake()
    {

    }
    private bool skillActive;
    private float skillTiming;
    public void CharacterSkill()
    {

    }
    public void Update()
    {
        if (this.skillTiming + 15f <= Time.time)
        {
            this.skillActive = true;
        }
        if (this.skillActive == true && Input.GetKeyDown(KeyCode.E))
        {
            CharacterSkill();
        }
    }
    public override string GetName()
    {
        SetName("Olek");
        return characterName;
    }
}

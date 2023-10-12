using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyCore : MonoBehaviour
{
    public string EnemyName;
    public int EnemyHearth;
    
    public abstract void EnemySkill();
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelAnimationUIManager : MonoBehaviour
{
    public EnemyCore enemyCore;
    public Text enemyDisplayName;

    private void Start(){
        ChangeName();
    }

    private void ChangeName(){
        enemyDisplayName.text = enemyCore.EnemyName;
    }
}

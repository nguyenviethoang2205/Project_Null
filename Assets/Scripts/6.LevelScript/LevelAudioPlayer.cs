using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelAudioPlayer : MonoBehaviour
{
    public AudioManager mainTheme;
    public AudioManager nearEndTheme;
    public AudioManager gameOverTheme;
    public AudioManager victoryTheme;
    public AudioManager pieceDownSound;
    public AudioManager pieceClearSound; 
    public AudioManager pieceMoveSound; 
    public AudioManager playerAttackSound;
    public AudioManager playerVictorySound;
    public AudioManager playerLoseSound;
    public AudioManager defenseAttackSound;

    public AudioManager defenseVictorySound;
    public AudioManager defenseLoseSound;

    public AudioManager buttonSound;

    public void PlayThemeAudio(){
        mainTheme.PlaySound();
    }

    public void StopThemeAudio(){
        mainTheme.StopSound();
    }

    public void PlayNearEndTheme(){
        nearEndTheme.PlaySound();
    }

    public void StopNearEndTheme(){
        nearEndTheme.StopSound();
    }


    public void PlayVictoryAudio(){
        victoryTheme.PlaySound();
    }

    public void PlayGameOverAudio(){
        gameOverTheme.PlaySound();
    }

    public void PlayPieceDownSound(){
        pieceDownSound.PlaySound();
    }

    public void PlayPieceMoveSound(){
        pieceMoveSound.PlaySound();
    }

    public void PlayPieceClearSound(){
        pieceClearSound.PlaySound();
    }

    public void PlayPlayerAttackSound(){
        playerAttackSound.PlaySound();
    }

    public void PlayPlayerVictorySound(){
        playerVictorySound.PlaySound();
    }

    public void PlayPlayerLoseSound(){
        playerLoseSound.PlaySound();
    }

    public void PlayDefenseAttackSound(){
        defenseAttackSound.PlaySound();
    }

    public void PlayDefenseVictorySound(){
        defenseVictorySound.PlaySound();
    }


    public void PlayDefenseLoseSound(){
        defenseLoseSound.PlaySound();
    }

    public void PlayButtonSound(){
        buttonSound.PlaySound(); 
    }
}

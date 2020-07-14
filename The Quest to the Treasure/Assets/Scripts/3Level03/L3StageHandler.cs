using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L3StageHandler : MonoBehaviour
{
    public L3GameManager gm;
    public int NumOfEnemiesInCurrentStage { get; private set; }
    public GameObject[] EnemiesByStage;
    int currentKnownStage;


    private void Start()
    {
        currentKnownStage = gm.CurrentStage;
    }

    public void showNextStage(int nextStage)
    {
        if (EnemiesByStage.Length == 0 || nextStage >= EnemiesByStage.Length || nextStage < 0)
            return;

        EnemiesByStage[currentKnownStage].SetActive(false);
        EnemiesByStage[nextStage].SetActive(true);
        currentKnownStage = nextStage;

        NumOfEnemiesInCurrentStage = checkNumOfEnemiesInStage();
    }

    public int checkNumOfEnemiesInStage()
    {
        return GameObject.FindGameObjectsWithTag("Enemy").Length;
    }
}

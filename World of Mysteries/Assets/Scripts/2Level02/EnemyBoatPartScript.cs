using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoatPartScript : MonoBehaviour
{
    public GameObject MainPart;
    public EnemyHPScript MainPartHP;

    private void Start()
    {
        MainPartHP = MainPart.GetComponent<EnemyHPScript>();
    }

    private void OnDestroy()
    {
        Destroy(MainPart);
    }

}

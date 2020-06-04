using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHPScript : MonoBehaviour
{
    public float hp;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        hp = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void hitDamage(float damage)
    {
        animator.Play("Get_Hit");
        hp -= damage;
    }
}

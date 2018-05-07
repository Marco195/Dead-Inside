using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

    private bool attacking = false;

    private float attackTimer = 0f;

    private float AttackCd = 0.5f; // cooldown do ataque

    public Collider2D attackTrigger;

    void Awake()
    {
        attackTrigger.enabled = false; 
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && !attacking)
        {
            attacking = true;
            attackTimer = AttackCd;

            attackTrigger.enabled = true;
        }

        if (attacking)
        {
            if(attackTimer > 0)
            {
                attackTimer -= Time.deltaTime;
            }
            else 
            {
                attacking = false;
                attackTrigger.enabled = false;
            }
        }

    }

}

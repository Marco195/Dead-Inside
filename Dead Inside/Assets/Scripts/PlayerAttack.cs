using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {
    //cuida da animação de  ataque e condições para atacar

    private bool attacking = false;

    private float attackTimer = 0f;//timer do ataque

    private float AttackCd = 0.5f; // cooldown do ataque

    public Collider2D attackTrigger;

    void Awake()
    {
        //estado inicial do collider é desativado
        attackTrigger.enabled = false; 
    }

    #region Update
    void Update()
    {
        //só ataca se estiver no ar
        if (Player.instance.isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space) && !attacking)
            {
                attacking = true;
                attackTimer = AttackCd;
                //ativa o trigger do collider
                attackTrigger.enabled = true;
            }

            if (attacking)
            {
                if (attackTimer > 0)
                {
                    //Tempo que o collider permanece ativado
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
    #endregion

}

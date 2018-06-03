using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTrigger : MonoBehaviour {

    private bool kill = true;
    private int dmg = 50;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //cada vez q o collider da arma entrar em contato com o boss
        if (collision.isTrigger  !=  true && collision.CompareTag("Boss"))
        {
            //ele recebe 50 de dano
            collision.SendMessage("DamageBoss", dmg);
        }

        if (collision.isTrigger != true && collision.CompareTag("Enemy"))
        {
            collision.SendMessage("Die", kill); 
        }
    }

}

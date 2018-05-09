using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTrigger : MonoBehaviour {

    private bool kill = true;
    //private int dmg = 50;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.isTrigger  !=  true && collision.CompareTag("Enemy"))
        {
            collision.SendMessage("Die", kill);
            //collision.SendMessage("Die", dmg);
        }
    }

}

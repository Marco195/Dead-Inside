using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour {

    #region OnTriggerEnter2D 
    private bool kill = true;

    //mata o player se colidir com a trap
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.isTrigger != true && collision.CompareTag("Player"))
        {
            collision.SendMessage("Die", kill);
        }
    }
    #endregion
}

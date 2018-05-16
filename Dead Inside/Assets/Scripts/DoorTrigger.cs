using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour {

    public GameMaster gameMaster;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //compara a tag, se for Player, chama CompleteLevel do GM
        if(collision.isTrigger != true && collision.CompareTag("Player"))
        {
            gameMaster.CompleteLevel();
        }      

    }
}

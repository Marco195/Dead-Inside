using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour {

    public static GameMaster gm;

    public Transform playerPrefab;

    public Transform spawnPoint;

    public int spawnDelay = 2;

    void Start()
    {
        if (gm == null)
        {
            gm = this;
        }

        //if (gm == null)
        //{
        //    gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        //}
    }    

    public IEnumerator RespawnPlayer()
    {
        //Você usa uma instrução yield return para retornar cada elemento individualmente.        
        yield return new WaitForSeconds(spawnDelay);

        //possivel adicionar um som de respawn aqui
        Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
    }

    public static void KillPlayer(Player player)// elimina o jogador
    {
        Destroy(player.gameObject);
        gm.StartCoroutine(gm.RespawnPlayer());
    }

    public static void KillEnemy(Enemy enemy) // elimina o inimigo
    {
        Destroy(enemy.gameObject);
    }
}

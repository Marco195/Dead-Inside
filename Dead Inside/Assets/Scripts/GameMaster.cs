using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour {
    //GameMaster fará a ligação entre das fases (resumindo)

    public static GameMaster gm;

    public Transform playerPrefab;

    public Transform spawnPoint;

    public int spawnDelay = 2;

    [SerializeField]
    private int maxLives = 3;

    [SerializeField]
    private GameObject gameOverUI;

    private static int _remainingLives = 3;

    //vidas restantes
    public static int RemainingLives
    {
        get { return _remainingLives; }
    }

    //variaveis para a pontuação
    private static int startingpontuation = 0;

    private static int pontuation;

    public static int Points
    {
        get { return pontuation; }
    }


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

        //para iniciar com 3 vidas após o game over
        _remainingLives = maxLives;
        
        //inicia a pontuação
        pontuation = startingpontuation;
        pontuation = 0;       
    }

    public void EndGame()
    {
        Debug.Log("GAME OVER");
       gameOverUI.SetActive(true);
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
        _remainingLives -= 1; //subtrai 1 vida a cada vez que o metodo killplayer é chamado
        if (_remainingLives <= 0)
        {
            gm.EndGame();
        }
        else
        {
            gm.StartCoroutine(gm.RespawnPlayer());
        }
        
    }

    public static void KillEnemy(Enemy enemy) // elimina o inimigo
    {
        gm._KillEnemy(enemy);
    }

    public void _KillEnemy(Enemy _enemy)
    {
        pontuation += _enemy.points;//variável points do enemy script
        Debug.Log(pontuation);
        Destroy(_enemy.gameObject);
        Score();
    }


    static void Score()
    {

        PlayerPrefs.SetInt("Score", pontuation);
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour {
    //GameMaster fará a ligação entre das fases (resumindo)
    //IMPORTANTE: Deve haver apenas um GM para todas as fases existentes
    public static GameMaster gm;

    public Transform playerPrefab;
    public Transform spawnPoint;
    public int spawnDelay = 2;

    public int animationDelay = 4;

    [SerializeField]
    private GameObject gameOverUI;

    [SerializeField]
    public GameObject levelWonUI;

    [SerializeField]
    private int maxLives = 3;    

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


    private void Awake()
    {
        if (gm == null)
        {
            gm = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            DestroyImmediate(gameObject);
        }

        //para iniciar com 3 vidas após o game over
        _remainingLives = maxLives;

        //inicia a pontuação
        pontuation = startingpontuation;
        pontuation = 0;
    }

    //#region Start
    //void Start()
    //{
    //    if (gm == null)
    //    {
    //        gm = this;
    //    }
    //    //para iniciar com 3 vidas após o game over
    //    _remainingLives = maxLives;
        
    //    //inicia a pontuação
    //    pontuation = startingpontuation;
    //    pontuation = 0; 
    //}
    //#endregion

    #region CompleteLevel
    public IEnumerator CompleteLevel()
    {
        //Coroutine feita para conseguir acessar os objetos de outras fases quando obj GameMaster é instanciado
        GameObject UIOverlay = GameObject.FindGameObjectWithTag("UIOverlay");
        UIOverlay.transform.GetChild(3).gameObject.SetActive(true);
        extraLive();//Adiciona uma vida nova caso tenha mais de X pontos
        yield return new WaitForSeconds(animationDelay);
        
        Debug.Log("Level WON");        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);//proxima fase        
    }
    #endregion

    #region End
    public static void End()
    {
        gm.EndGame();
    }
    #endregion

    #region EndGame
    public void EndGame()
    {
        GameObject UIOverlay = GameObject.FindGameObjectWithTag("UIOverlay");
        UIOverlay.transform.GetChild(1).gameObject.SetActive(true);
        Debug.Log("GAME OVER");

        //para iniciar com 3 vidas após o game over
        _remainingLives = maxLives;

        //inicia a pontuação
        pontuation = startingpontuation;
        pontuation = 0;
    }
    #endregion

    #region RespawnPlayer
    public IEnumerator RespawnPlayer()
    {
        //Você usa uma instrução yield return para retornar cada elemento individualmente.        
        yield return new WaitForSeconds(spawnDelay);

        //possivel adicionar um som de respawn aqui
        Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
    }
    #endregion

    #region KillPlayer
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
    #endregion

    #region KillEnemy
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
    #endregion

    #region Score
    static void Score()
    {
        PlayerPrefs.SetInt("Score", pontuation);
    }
    #endregion

    #region extraLive
    //adiciona + 1 quando o jogador fizer 5 pontos
    private void extraLive()
    {
        if (pontuation >= 4)
        {
            _remainingLives += 1; 
        }
    }
    #endregion

    #region DestroyGM
    public static void DestroyGM()
    {
        //usado para destruir o obg GM ao clicar em "sair" no game over
        DestroyImmediate(gm.gameObject);
    }
    #endregion
}

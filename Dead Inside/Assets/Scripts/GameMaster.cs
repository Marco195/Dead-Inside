using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour {
    //GameMaster fará a ligação entre das fases (resumindo)
    //IMPORTANTE: Deve haver apenas um GM para todas as fases existentes
    public static GameMaster gm;
    
    //variaveis relacionadas ao respawn do player
    public Transform playerPrefab;
    public Transform spawnPoint;
    public int spawnDelay = 2;

    public int animationDelay = 4;

    public static bool pause = false;

    public AudioManager manager;

    //vidas restantes
    private static int _remainingLives = 3;    
    public static int RemainingLives
    {
        get { return _remainingLives; }
    }

    //variaveis para a pontuação
    private static int pontuation = 0;
    public static int Points
    {
        get { return pontuation; }
    }

    #region DestroyGM
    public static void DestroyGM()
    {
        //usado para destruir o obg GM ao clicar em "sair" no game over
        DestroyImmediate(gm.gameObject);
    }
    #endregion

    #region Awake
    //usado para inicializar variaveis, é chamado antes do start
    private void Awake() 
    {
        if (gm == null)
        {
            gm = this;
            //mantém o obj _GM para as proximas fazes
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            //se ja houver um oobj na fase ele destrói
            DestroyImmediate(gameObject);
        }

        //para iniciar com 3 vidas após o game over
        _remainingLives = 3;

        //inicia a pontuação
        pontuation = 0;

        //AudioManager instancia
        manager = AudioManager.instance;
        if (manager == null)
        {
            Debug.LogError("Não foi encontrado Um manager de audio");
        }
    }
    #endregion

    #region Start
    private void Start()
    {
        //inicia a musica de fundo das fases
        gm.manager.PlaySound("Background");

        //para a musica do menu
        gm.manager.StopSound("MainMenu");
    }
    #endregion

    #region Update
    void Update()
    {
        //Se P ou esc press. chama a função pause game
        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
        {
                Pause();
        }
    }
    #endregion

    #region CompleteLevel
    public IEnumerator CompleteLevel()
    {
        //Coroutine feita para conseguir acessar os objetos de outras fases quando obj GameMaster é instanciado
        GameObject UIOverlay = GameObject.FindGameObjectWithTag("UIOverlay");
        UIOverlay.transform.GetChild(3).gameObject.SetActive(true);
        yield return new WaitForSeconds(animationDelay);
        
        //Debug.Log("Level WON");        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);//proxima fase 
        gm.manager.PlaySound("Background");
    }
    #endregion

    #region EndGame
    public void EndGame()
    {
        GameObject UIOverlay = GameObject.FindGameObjectWithTag("UIOverlay");
        UIOverlay.transform.GetChild(1).gameObject.SetActive(true);
        Debug.Log("GAME OVER");
        gm.manager.PlaySound("GameOver");// chama o audio de game over
        //para iniciar com 3 vidas após o game over
        _remainingLives = 3;

        //inicia a pontuação
        pontuation = 0;
    }
    #endregion

    #region Pause
    public static void Pause()
    {
        //pauseMenu
        pause = !pause;
        if (pause)
        {
            //se P ou ESC for pressionado, para o tempo e ativa o menu de pause
            GameObject UIOverlay = GameObject.FindGameObjectWithTag("UIOverlay");
            UIOverlay.transform.GetChild(4).gameObject.SetActive(true);
            Time.timeScale = 0.0f;
        }
        else
        {
            //se P pressionado denovo ele desativa o menu e volta o tempo ao normal
            GameObject UIOverlay = GameObject.FindGameObjectWithTag("UIOverlay");
            UIOverlay.transform.GetChild(4).gameObject.SetActive(false);
            Time.timeScale = 1.0f;
        }
    }
    #endregion

    #region RespawnPlayer
    //Coroutine para tratar o respawn do player 
    public IEnumerator RespawnPlayer()
    {
         //dealy para respawn do player     
        yield return new WaitForSeconds(spawnDelay);

        //possivel adicionar um som de respawn aqui
        Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
    }
    #endregion

    #region KillPlayer
    public static void KillPlayer(Player player)// elimina o jogador
    {
        gm.manager.PlaySound("Death");
        //Vidas do player são mostradas na tela atraves do LivesCounterUI
        Destroy(player.gameObject);
        //subtrai 1 vida a cada vez que o metodo killplayer é chamado
        _remainingLives -= 1; 
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
        gm.manager.PlaySound("ZombieDeath");
        pontuation += enemy.points;//variável points do enemy script
        Debug.Log(pontuation);
        Destroy(enemy.gameObject);
        Score();
    }
    #endregion

    #region Score
    static void Score()
    {
        //Gerencia os pontos do jogador
        PlayerPrefs.SetInt("Score", pontuation);
        
        //adicionado uma vida ao eleminar um inimigo
        if (pontuation == 4 || pontuation == 8)
        {
            _remainingLives += 1;
            gm.manager.PlaySound("ExtraLife");
        }
    }
    #endregion
    
}

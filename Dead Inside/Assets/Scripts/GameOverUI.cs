using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour {

    public void Quit()
    {
        //Volta ao menu inicial e destroi obj GameMaster
        SceneManager.LoadScene(0);
        GameMaster.DestroyGM();
    }

    public void Retry()
    {
        //recarrega a cena
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

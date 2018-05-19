using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour {

    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;


	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("p"))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
	}

    void Resume()
    {
        GameObject UIOverlay = GameObject.FindGameObjectWithTag("UIOverlay");
        UIOverlay.transform.GetChild(3).gameObject.SetActive(false);

        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {
        GameObject UIOverlay = GameObject.FindGameObjectWithTag("UIOverlay");
        UIOverlay.transform.GetChild(3).gameObject.SetActive(true);
        Debug.Log("PAUSED!");

        pauseMenuUI.SetActive(true);

        Time.timeScale = 0f;

        GameIsPaused = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class Pontuation : MonoBehaviour {

    private Text ScoreText;

    //Awake is called when the script instance is being loaded. 
     void Awake()
    {
        ScoreText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        //UI dos pontos
        ScoreText.text = "Pontos: " + GameMaster.Points.ToString();

    }

}

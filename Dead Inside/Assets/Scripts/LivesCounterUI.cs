using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class LivesCounterUI : MonoBehaviour {
    //contador de vidas na UI
    [SerializeField]
    private Text livesText;

    void Awake()
    {
        livesText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        //Altera o texto padrão da UI pra o tanto de vidas remanescentes
        livesText.text = " " + GameMaster.RemainingLives.ToString(); 
    }

}

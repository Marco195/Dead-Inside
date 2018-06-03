using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    private Vector2 velocity;

    public float smoothTimeY;

    public float smoothTimeX;

    public GameObject player; // cria um game object

    public bool bounds;

    [SerializeField]
    private Vector3 minCameraPos;

    [SerializeField]
    private Vector3 maxCameraPos;

    #region Start
    void Start () {

        player = GameObject.FindGameObjectWithTag("Player"); // atribui o Game Object com a tag player

	}
    #endregion

    #region FixedUpdate
    void FixedUpdate()
    {
        if (player == null) //só chama o codigo abaixo se o obj player ainda existir
        {
           player = GameObject.FindGameObjectWithTag("Player");
           return;
        }
        
        //pega a posição X e Y do player
        float posX = Mathf.SmoothDamp(transform.position.x, player.transform.position.x, ref velocity.x, smoothTimeX);
        float posY = Mathf.SmoothDamp(transform.position.y, player.transform.position.y, ref velocity.y, smoothTimeY);

        transform.position = new Vector3(posX, posY, transform.position.z);

        if (bounds)//se houver limites
        {
            //é setado um valor maximo para X, y e Z
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, minCameraPos.x, maxCameraPos.x),
            //seta um valor entre um float maximo e um float minimo
            Mathf.Clamp(transform.position.y, minCameraPos.y, maxCameraPos.y),
            Mathf.Clamp(transform.position.z, minCameraPos.z, maxCameraPos.z));
        }
    }
    #endregion

}

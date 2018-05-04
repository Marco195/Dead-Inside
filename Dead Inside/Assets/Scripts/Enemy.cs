using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public LayerMask enemyMask;

    Rigidbody2D rb;

    Transform myTransf;

    float myWidth;

    public float speed = 1;

	void Start () {
        myTransf = this.transform;

        rb = this.GetComponent<Rigidbody2D>();

        SpriteRenderer mySprite = this.GetComponent<SpriteRenderer>();

        myWidth = mySprite.bounds.extents.x;//pega o tamanho da sprite

    }

	void FixedUpdate () {

        // Checa para ver se tem algo a frente apos andar 
        Vector2 lineCastPos = myTransf.position- myTransf.right * myWidth;        
        bool isGrounded = Physics2D.Linecast(lineCastPos, lineCastPos + Vector2.down, enemyMask);
        Debug.DrawLine(lineCastPos, lineCastPos + Vector2.up);//desenha uma linha na frente do personagem

        //se não tiver chão, vira ao contrario ou se estiver travado na parede
        if (!isGrounded)
        {
            Vector3 currentRotation = myTransf.eulerAngles;
            currentRotation.y += 180;
            myTransf.eulerAngles = currentRotation;  
        }

        //sempre anda para frente
        Vector2 myVel = rb.velocity;
        myVel.x = -myTransf.right.x * speed;// positivo pra direita e negativo pra esquerda
        rb.velocity = myVel;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private Rigidbody2D rb;

    private Animator myAnimator;

    [SerializeField]
    private float movementSpeed;

    private bool facingRight;

    // Use this for initialization
    void Start()
    {
        facingRight = true;
        rb = GetComponent<Rigidbody2D>(); //cria um componente rigidbody
        myAnimator = GetComponent<Animator>(); // cria um componente animator
    }

    // Update is called once per frame
    void FixedUpdate()// tem uma atualização mais constante do que o Update comum
    {
        float horizontal = Input.GetAxis("Horizontal");// atribui o eixo X a variavel horizontal

        //chamada das funções a serem executadas a cada update
        HandleMovement(horizontal);

        Flip(horizontal); 
    }

    
    private void HandleMovement(float horizontal) //reponsavel por movimentar o personagem
    {
        rb.velocity = new Vector2(horizontal * movementSpeed, rb.velocity.y);

        //troca a animação de Idle para Walk com base na velocidade
        myAnimator.SetFloat("speed", Mathf.Abs (horizontal));
    }

    //Muda o personagem de posição de acordo com a direção que ele está indo
    private void Flip(float horizontal)
    {
        if(horizontal > 0 && !facingRight || horizontal < 0 && facingRight)
        {
            facingRight = !facingRight;

            Vector3 theScale = transform.localScale;

            //muda o personagem de lado com base em sua escala
            //1 para direita -1 para esquerda
            theScale.x *= -1;

            transform.localScale = theScale;

        }
    }

}
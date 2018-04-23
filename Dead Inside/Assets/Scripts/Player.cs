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

    private bool attack;

    [SerializeField]
    private Transform[] groundPoints;

    [SerializeField]
    private float groundRadius;

    [SerializeField]
    private LayerMask whatIsGround; //Usado para detectar se o objeto que esta abaixo do player é um ground

    private bool isGrounded;

    private bool jump;

    // variaveis serialized podem ser acessadas pelo Inspector dentro do Unity
    [SerializeField]
    private bool airControl;

    [SerializeField]
    private float jumpForce;

    

    #region Start
    // Use this for initialization
    void Start()
    {
        facingRight = true;
        rb = GetComponent<Rigidbody2D>(); //cria um componente rigidbody
        myAnimator = GetComponent<Animator>(); // cria um componente animator
    }
    #endregion


    #region Update
    private void Update()
    {
        HandleInput();// chamada do handle input
    }
    #endregion


    #region FixedUpdate
    // Update is called once per frame
    void FixedUpdate()// tem uma atualização mais constante do que o Update comum
    { //chamada das funções a serem executadas a cada update
        float horizontal = Input.GetAxis("Horizontal");// atribui o eixo X a variavel horizontal

        isGrounded = IsGrounded();
        
        HandleMovement(horizontal);

        HandleAttacks();

        HandleLayers();

        Flip(horizontal);

        ResetValues();

    }
    #endregion


    #region HandleMovement
    private void HandleMovement(float horizontal) // Cuida dos movimentos
    {
        /*if (!this.myAnimator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))//para a animação de correr quando ataca
        {
           rb.velocity = new Vector2(horizontal * movementSpeed, rb.velocity.y);
        }*/
        
        if (rb.velocity.y < 0) //usa o RB para detectar quando esta caindo
        {
            myAnimator.SetBool("land", true); // seta o trigger land para verdadeiro
        }

        if (isGrounded || airControl) { // Para a animação de correr quando no ar
            rb.velocity = new Vector2(horizontal * movementSpeed, rb.velocity.y);
        }        

        //troca a animação de Idle para Walk com base na velocidade
        myAnimator.SetFloat("speed", Mathf.Abs(horizontal));

        if (isGrounded && jump) //Jump
        {
            isGrounded = false;
            rb.AddForce(new Vector2(0, jumpForce));//pula quando press space
            myAnimator.SetTrigger("jump"); //troca a animação para playerJumpUP quando press w (trigger)
        }

    }
    #endregion


    #region HandleInput
    private void HandleInput()
    {       
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown("w"))//Jump key cap
        {
            jump = true;
        }

        if (Input.GetKeyDown(KeyCode.Space))// Attack key cap
        {
            attack = true;
            //rb.velocity = Vector2.zero;
        }
    }
    #endregion

    #region HandleAttacks
    private void HandleAttacks() // ativa o trigger attack do animator
    {
        if (attack)
        {
            myAnimator.SetTrigger("attack");
        }
    }

    #endregion

    #region HandleLayers
    private void HandleLayers()
    {
        if (!isGrounded)
        {
            myAnimator.SetLayerWeight(1, 1);
        }
        else
        {
            myAnimator.SetLayerWeight(1, 0);
        }
    }
    #endregion


    #region Flip
    //Muda o personagem de posição de acordo com a direção que ele está indo
    private void Flip(float horizontal)
    {
        if (horizontal > 0 && !facingRight || horizontal < 0 && facingRight)
        {
            facingRight = !facingRight;

            Vector3 theScale = transform.localScale;

            //muda o personagem de lado com base em sua escala
            //1 para direita -1 para esquerda
            theScale.x *= -1;

            transform.localScale = theScale;

        }
    }
    #endregion


    #region ResetValues
    private void ResetValues()
    {
        jump = false;

        attack = false;
    }
    #endregion


    #region IsGrounded
    private bool IsGrounded()
    {
        if(rb.velocity.y <= 0)
        {
            foreach (Transform point in groundPoints)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(point.position, groundRadius, whatIsGround);

                for (int i = 0; i < colliders.Length; i++)
                {

                    if(colliders[i].gameObject != gameObject)
                    {
                        myAnimator.ResetTrigger("jump");// reseta o trigger da animção de pulo
                        myAnimator.SetBool("land", false); // seta o trigger para iniciar a niamção de queda(land)
                        return true;
                    }
                }
            }            
        }
        return false;
    }
    #endregion

}
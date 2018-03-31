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
        HandleInput();
    }
    #endregion


    #region FixedUpdate
    // Update is called once per frame
    void FixedUpdate()// tem uma atualização mais constante do que o Update comum
    {
        float horizontal = Input.GetAxis("Horizontal");// atribui o eixo X a variavel horizontal

        //chamada das funções a serem executadas a cada update
        HandleMovement(horizontal);

        Flip(horizontal);

        isGrounded = IsGrounded();

        ResetValues();

    }
    #endregion

    #region HandleMovement
    private void HandleMovement(float horizontal) //reponsavel por movimentar o personagem
    {
        if (isGrounded || airControl) {
            rb.velocity = new Vector2(horizontal * movementSpeed, rb.velocity.y);
        }        

        //troca a animação de Idle para Walk com base na velocidade
        myAnimator.SetFloat("speed", Mathf.Abs(horizontal));

        if (isGrounded && jump)
        {
            isGrounded = false;
            rb.AddForce(new Vector2(0, jumpForce));
        }

    }
    #endregion

    #region HandleInput
    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jump = true;
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
    private void ResetValues() {
        jump = false;
    }
    #endregion

    #region IsGrounded
    private bool IsGrounded() {
        if(rb.velocity.y <= 0)
        {
            foreach (Transform point in groundPoints)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(point.position, groundRadius, whatIsGround);

                for (int i = 0; i < colliders.Length; i++) {

                    if(colliders[i].gameObject != gameObject)
                    {
                        return true;
                    }
                }
            }            
        }
        return false;
    }
    #endregion

}
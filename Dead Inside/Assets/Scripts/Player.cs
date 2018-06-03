using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //instacia do player
    public static Player instance = null;

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

    public bool isGrounded;
    private bool jump;

    // variaveis serialized podem ser acessadas pelo Inspector dentro do Unity
    [SerializeField]
    private bool airControl;
    [SerializeField]
    private float jumpForce;
    [SerializeField]
    public int fallBoundary = -20; //Cair alem da fronteira

    private bool died = false;

    #region Awake
    private void Awake()
    {
        //Mesma coisa feita no awake do GM, se não houver uma instância ele cria,se ja houver, ele destrói
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else instance = this;
    }
    #endregion

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

        if(transform.position.y <= fallBoundary)// comando que verifica se o player passou do limite de y
        {
            GameMaster.KillPlayer(this);
        }
    }
    #endregion

    #region FixedUpdate
    // Update is called once per frame
    void FixedUpdate()// tem uma atualização mais constante do que o Update comum
    { //chamada das funções a serem executadas a cada update
        float horizontal = Input.GetAxis("Horizontal");// atribui o eixo X a variavel horizontal

        isGrounded = IsGrounded(); //verifica se o player esta no chao

        HandleMovement(horizontal);

        HandleAttacks();

        HandleLayers();

        Flip(horizontal);

        ResetValues();
    }
    #endregion

    #region Die
    public void Die()
    {
        if (!died)
        {
            died = true;
            GameMaster.KillPlayer(this);
        }
    }
    #endregion

    #region HandleMovement
    private void HandleMovement(float horizontal) // Cuida dos movimentos
    {
        //Movimenta o personagem
        //Horizontal = a própria unity ja tem definido as teclas que movem o personagem na horizontal
        rb.velocity = new Vector2(horizontal * movementSpeed, rb.velocity.y);
        myAnimator.SetFloat("speed", Mathf.Abs(horizontal));

        if (rb.velocity.y < 0) //usa o RB para detectar quando esta caindo
        {
            myAnimator.SetBool("land", true); // seta o trigger land para verdadeiro
        }        

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
        }
    }
    #endregion

    #region HandleAttacks
    private void HandleAttacks() 
    {
        // ativa o trigger attack do animator
        if (attack)
        {
            myAnimator.SetTrigger("attack");
        }
    }

    #endregion

    #region HandleLayers
    private void HandleLayers()
    {
        //altera o peso dos layers no animator
        if (!isGrounded)
        {
            //se o player não estiver no chão o layer que terá peso maior será o Air Layer
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
        //reseta os triggers para cancelar a animação
        jump = false;

        attack = false;
    }
    #endregion

    #region IsGrounded
    //função para detectar se o personagem está no chão ou não
    private bool IsGrounded()
    {
        if(rb.velocity.y <= 0)
        {
            foreach (Transform point in groundPoints)//pega os 3 groundspoint do player 
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(point.position, groundRadius, whatIsGround);

                for (int i = 0; i < colliders.Length; i++)
                {
                    if(colliders[i].gameObject != gameObject)
                    {
                        myAnimator.ResetTrigger("jump");// reseta o trigger da animação de pulo
                        myAnimator.SetBool("land", false); // seta o trigger para iniciar a animação de queda(land)
                        return true;
                    }
                }
            }            
        }
        return false;
    }
    #endregion

}
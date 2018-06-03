using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour {

    //instancia do boss
    public static Boss instance;

    public LayerMask enemyMask;
    Rigidbody2D rb;
    Transform myTransf;
    float myWidth;

    public float speed = 1;
    private bool died = false;
    public int points = 1;

    //vida do inimigo
    public int curHealth;
    private int maxHealth = 100;

    #region Awake
    private void Awake()
    {
        curHealth = maxHealth;

        //destroi a instância do boss caso ja exista
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else instance = this;
    }
    #endregion

    #region Start
    void Start()
    {
        myTransf = this.transform;

        rb = this.GetComponent<Rigidbody2D>();

        SpriteRenderer mySprite = this.GetComponent<SpriteRenderer>();

        myWidth = mySprite.bounds.extents.x;//pega o tamanho da sprite            

    }
    #endregion

    #region FixedUpdate
    void FixedUpdate()
    {

        // Checa para ver se tem algo a frente apos andar 
        Vector2 lineCastPos = myTransf.position - myTransf.right * myWidth;
        bool isGrounded = Physics2D.Linecast(lineCastPos, lineCastPos + Vector2.down, enemyMask);
        Debug.DrawLine(lineCastPos, lineCastPos + Vector2.up);//desenha uma linha na frente do personagem

        //se não tiver chão, vira ao contrario 
        if (!isGrounded)
        {
            Vector3 currentRotation = myTransf.eulerAngles;
            currentRotation.y += 180;
            myTransf.eulerAngles = currentRotation;
        }

        //anda para frente
        Vector2 myVel = rb.velocity;
        myVel.x = -myTransf.right.x * speed;// positivo pra direita e negativo pra esquerda


        rb.velocity = myVel;

    }
    #endregion

    #region OnTriggerEnter2D 
    private bool kill = true;
    //mata o player se colidir com o inimigo
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.isTrigger != true && collision.CompareTag("Player"))
        {
            //collision.SendMessage("Die", kill);
            GameMaster.KillPlayer(Player.instance);
        }
    }
    #endregion

    #region DamageEnemy
    public void DamageBoss(int damage)
    {
        curHealth -= damage;

        if (curHealth <= 0)//mata o boss se a vida dele chegar a 0
        {
            if (!died)
            {
                died = true;
                GameMaster.KillBoss(this);
            }
        }
    }
    #endregion
}

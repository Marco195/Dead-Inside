using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    private Rigidbody2D rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        float horizontal = Input.GetAxis("Horizontal");

        HandleMovement();
    }

    private void HandleMovement() {
        rb.velocity = Vector2.left; // vetor com x = -1 e y= 0, movimento para a esquerda

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    [Range(1, 100)]
    [SerializeField]
    private float _speed = 10;
    private Rigidbody2D rb2d;

    protected void Start() {
        rb2d = GetComponent<Rigidbody2D>();
    }

	void FixedUpdate () {
        Move();
	}

    void Move() {
        float x = Input.GetAxisRaw("Horizontal");
        rb2d.velocity = new Vector2(x * _speed, rb2d.velocity.y);

        CheckForJump();
    }

    void CheckForJump() {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Down)) {
            Debug.Log("Jump");
        }
    }
}

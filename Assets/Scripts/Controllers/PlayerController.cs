using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    [Range(1, 100)]
    [SerializeField]
    private float speed = 10;
    [Range(1, 10)]
    [SerializeField]
    private float jumpForce = 10;
    [Range(1, 5)]
    [SerializeField]
    private float fallMultiplier = 2.5f;
    [Range(1, 5)]
    [SerializeField]
    private float lowJumpMultiplier = 2f;
    
    private Rigidbody2D rb2d;

    public LayerMask groundLayer;

    protected void Start() {
        rb2d = GetComponent<Rigidbody2D>();
    }

	void Update () {
        Move();
        CheckForJump();
        BetterJump();
	}

    void Move() {
        float x = Input.GetAxisRaw("Horizontal");
        rb2d.velocity = new Vector2(x * speed, rb2d.velocity.y);
    }

    void CheckForJump() {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.6f, groundLayer);
            if (hit.collider != null) {
                rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);
            }
        }
    }

    void BetterJump() {
        if (rb2d.velocity.y < 0) {
            rb2d.velocity += Vector2.up * Physics2D.gravity * (fallMultiplier - 1) * Time.deltaTime;
        } else if (rb2d.velocity.y > 0 && !Input.GetKey(KeyCode.Space)) {
            rb2d.velocity += Vector2.up * Physics2D.gravity * (lowJumpMultiplier - 1) * Time.deltaTime;
        }   
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {
    [Range(0, 2)]
    [SerializeField]
    private float fallingSpeed = 2f;
    [Range(0, 2)]
    [SerializeField]
    private float hoveringDistance = 0.15f;
    [Range(0, 5)]
    [SerializeField]
    private float secondsSpentOnGround = 2f;

    private enum State {
        FALLING,
        GROUNDED
    };

    private Rigidbody2D rb2d;
    private State state;

    private Vector2 startFloatingPos = new Vector2();
    private float startFloatingTime = 0;
    
    public LayerMask groundLayer;
    public Constants.Items itemTag;

    void Start() {
        rb2d = GetComponent<Rigidbody2D>();
        state = State.FALLING;
        rb2d.velocity = new Vector2(0, -fallingSpeed);
    }

    void Update() {
        switch (state) {
            case State.FALLING:
                FallingUpdate();
                break;
            case State.GROUNDED:
                GroundedUpdate();
                break;
        }
    }

    void FallingUpdate() {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.5f, groundLayer);
        if (hit.collider != null) {
            rb2d.velocity = new Vector2(0, 0);
            state = State.GROUNDED;
            startFloatingTime = Time.fixedTime;
            startFloatingPos = transform.position;
        }
    }

    void GroundedUpdate() {
        float timeSinceLanded = Time.fixedTime - startFloatingTime;
        rb2d.position = startFloatingPos + new Vector2(0, -hoveringDistance * Mathf.Sin(Mathf.PI * (timeSinceLanded)));
        if (timeSinceLanded > secondsSpentOnGround) {
            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leg : MonoBehaviour {
    private Constants.Outfit pantType;
    private Constants.Shoe shoeType;

    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rigidBody;
    private BoxCollider2D boxCollider;

    [Range(0, 2)]
    [SerializeField]
    private float fallingSpeed = 2f;

    [Range(0, 2)]
    [SerializeField]
    private float risingSpeed = 2f;

    [Range(0, 5)]
    [SerializeField]
    private float secondsSpentOnGround = 2f;

    [Range(0, 5)]
    [SerializeField]
    private float secondsSpentRising = 2f;

    [Range(0, 5)]
    [SerializeField]
    private float secondsSpentMoving = 2f;

    private Vector2 startPos = new Vector2();
    
    private float timeHitGround = 0f;
    private float timeStartedRising = 0f;
    private float timeStartedMoving = 0f;

    private float walkingSpeed;
    private int walkingDirection;

    private enum State {
        FALLING,
        GROUNDED,
        RISING,
        MOVING
    };

    private State currState;

    public LayerMask groundLayer;

    public void SetParams( float walkingSpeed, int walkingDirection, Constants.Outfit pantType, Constants.Shoe shoeType ) {
        this.walkingSpeed = walkingSpeed;
        this.walkingDirection = walkingDirection;
        this.pantType = pantType;
        this.shoeType = shoeType;
    }

    public void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidBody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();

        currState = State.FALLING;
        rigidBody.velocity = new Vector2( 0, -fallingSpeed );
    }

    void Update() {
        switch ( currState ) {
            case State.FALLING:
                FallingUpdate();
                break;
            case State.GROUNDED:
                GroundedUpdate();
                break;
            case State.RISING:
                RisingUpdate();
                break;
            case State.MOVING:
                MovementUpdate();
                break;
        }
    }

    private void FallingUpdate() {
        RaycastHit2D hitGround = Physics2D.Raycast( transform.position, Vector2.down, 0.5f, groundLayer );
        if ( hitGround.collider != null ) {
            rigidBody.velocity = new Vector2( 0, 0 );
            currState = State.GROUNDED;
            timeHitGround = Time.fixedTime;
        }
    }

    private void GroundedUpdate() {
        float timeSinceLanded = Time.fixedTime - timeHitGround;
        if ( timeSinceLanded > secondsSpentOnGround ) {
            currState = State.RISING;
            rigidBody.velocity = new Vector2( 0, risingSpeed );
            timeStartedRising = Time.fixedTime;
        }
    }

    private void RisingUpdate() {
        float delta = Time.fixedTime - timeStartedRising;
        if ( delta > secondsSpentRising ) {
            currState = State.MOVING;
            rigidBody.velocity = new Vector2( walkingSpeed * walkingDirection, 0 );
            timeStartedMoving = Time.fixedTime;
        }
    }

    private void MovementUpdate() {
        float delta = Time.fixedTime - timeStartedMoving;
        if ( delta > secondsSpentMoving ) {
            currState = State.FALLING;
            rigidBody.velocity = new Vector2( 0, -fallingSpeed );
        }

        Vector3 viewPos = Camera.main.WorldToViewportPoint( transform.position );
        if ( viewPos.x < -0.2f ) {
            Destroy( gameObject );
        }
    }
}

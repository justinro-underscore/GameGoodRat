using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leg : MonoBehaviour {
    private bool initialized = false;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rigidBody;
    private BoxCollider2D[] shoeBoxColliders;

    [Range(0, 10)]
    [SerializeField]
    private float fallingSpeed = 0f;

    [Range(0, 10)]
    [SerializeField]
    private float risingSpeed = 0f;

    [Range(0, 10)]
    [SerializeField]
    private float secondsSpentOnGround = 0f;

    [Range(0, 5)]
    [SerializeField]
    private float secondsSpentRising = 0f;

    [Range(0, 5)]
    [SerializeField]
    private float howFarToMove = 0f;

    private enum State {
        FALLING,
        GROUNDED,
        RISING,
        MOVING
    };

    private State currState;

    private float walkingSpeed;
    private bool walkingLeft;

    private bool droppedItem = false;
    private float movementStartX;
    private float timeHitGround = 0f;
    private float timeStartedRising = 0f;

    public Constants.People personType;
    public GameObject itemPrefab;
    public LayerMask groundLayer;

    public void SetParams( float walkingSpeed, bool walkingLeft, Constants.People personType, Constants.Outfit outfitType, Constants.Shoe shoeType ) {
        this.walkingSpeed = walkingSpeed;
        this.walkingLeft = walkingLeft;
        this.personType = personType;

        // TODO Use renderer to use outfit and shoe

        _Start();
    }

    void _Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidBody = GetComponent<Rigidbody2D>();
        shoeBoxColliders = transform.Find( "Shoe" ).GetComponents<BoxCollider2D>();

        currState = State.FALLING;
        rigidBody.velocity = new Vector2( 0, -fallingSpeed );

        initialized = true;
    }

    void Update() {
        if (initialized) {
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
    }

    private void FallingUpdate() {
        RaycastHit2D hitGround = Physics2D.Raycast( transform.position, Vector2.down, 1.7f, groundLayer );
        if ( hitGround.collider != null ) {
            shoeBoxColliders[0].enabled = false;

            rigidBody.velocity = new Vector2( 0, 0 );
            currState = State.GROUNDED;
            timeHitGround = Time.fixedTime;

            if (!droppedItem && UnityEngine.Random.value < 1f &&
                ((walkingLeft && transform.position.x > 0) ||
                (!walkingLeft && transform.position.x < 0))) {
                DropItem();
            }
        }
    }

    private void DropItem() {
        GameObject item = Instantiate(itemPrefab, new Vector3(transform.position.x, Constants.OFFSCREEN_Y + 1), Quaternion.identity);
        (item.GetComponent<Item>() as Item).Init(Constants.personToItem[personType]);
        droppedItem = true;
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
            movementStartX = transform.position.x;
            rigidBody.velocity = new Vector2( walkingSpeed * (walkingLeft ? -1 : 1), 0 );
        }
    }

    private void MovementUpdate() {
        if ((walkingLeft && transform.position.x < (movementStartX - howFarToMove)) ||
            (!walkingLeft && transform.position.x > (movementStartX + howFarToMove))) {
            shoeBoxColliders[0].enabled = true;

            currState = State.FALLING;
            rigidBody.velocity = new Vector2( 0, -fallingSpeed );
        }

        Vector3 viewPos = Camera.main.WorldToViewportPoint( transform.position );
        if ((walkingLeft && viewPos.x < -0.1f) ||
            (!walkingLeft && viewPos.x > 1.1f)) {
            Destroy( gameObject );
        }
    }

    public void ToggleShoeCollider(bool enabled) {
        shoeBoxColliders[1].enabled = enabled;
    }
}

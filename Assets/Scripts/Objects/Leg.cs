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
    [Range(0, 15)]
    [SerializeField]
    private float movementY = 0f;
    private float movementX;
    [Range(0, 1)]
    [SerializeField]
    private float itemProbability = 1f;

    private enum State {
        FALLING,
        GROUNDED,
        RISING
    };

    private State currState;

    private float walkingSpeed;
    private bool walkingLeft;

    private float movementStartX;

    public Leg otherLeg;
    public bool droppedItem = false;
    public Constants.People personType;
    public GameObject itemPrefab;
    public LayerMask groundLayer;

    public void Init( Leg otherLeg, float walkingSpeed, bool walkingLeft, Constants.People personType, float movementX ) {
        this.otherLeg = otherLeg;
        this.walkingSpeed = walkingSpeed;
        this.walkingLeft = walkingLeft;
        this.personType = personType;
        this.movementX = movementX;

        spriteRenderer = GetComponent<SpriteRenderer>();
        // TODO Use spriteRenderer to use outfit and shoe
        // (Constants.Outfit outfit, Constants.Shoe shoe) = Constants.personToOutfit[personType];
        spriteRenderer.flipX = !walkingLeft;

        _Start();
    }

    void _Start() {
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
                case State.RISING:
                    RisingUpdate();
                    break;
            }
        }
    }

    private void FallingUpdate() {
        RaycastHit2D hitGround = Physics2D.Raycast( transform.position, Vector2.down, 4.8f, groundLayer );
        if ( hitGround.collider != null ) {
            shoeBoxColliders[0].enabled = false;

            rigidBody.velocity = new Vector2( 0, 0 );
            currState = State.GROUNDED;

            otherLeg.StartRising();
        }
    }

    private void RisingUpdate() {
        if ((walkingLeft && transform.position.x < (movementStartX - movementX)) ||
            (!walkingLeft && transform.position.x > (movementStartX + movementX))) {
            shoeBoxColliders[0].enabled = true;

            currState = State.FALLING;
            rigidBody.velocity = new Vector2( 0, -fallingSpeed );
        }

        Vector3 viewPos = Camera.main.WorldToViewportPoint( transform.position );
        if ((walkingLeft && viewPos.x < -0.2f) ||
            (!walkingLeft && viewPos.x > 1.2f)) {
            otherLeg.StartRising();
            Destroy( gameObject );
        }
    }

    public void StartRising() {
        if ( currState == State.GROUNDED ) {
            currState = State.RISING;
            Vector2 velocityVector = new Vector2( walkingLeft ? -movementX : movementX, movementY );
            velocityVector.Normalize();
            rigidBody.velocity = velocityVector * walkingSpeed;
            movementStartX = transform.position.x;

            DropItem();
        }
    }

    private void DropItem() {
        if (!droppedItem && UnityEngine.Random.value < itemProbability &&
            ((walkingLeft && (transform.position.x > 0 && transform.position.x < Constants.OFFSCREEN_X)) ||
            (!walkingLeft && (transform.position.x < 0 && transform.position.x > -Constants.OFFSCREEN_X)))) {

            GameObject item = Instantiate(itemPrefab, new Vector3(transform.position.x, Constants.OFFSCREEN_Y + 1), Quaternion.identity);
            (item.GetComponent<Item>() as Item).Init(Constants.personToItem[personType]);
            droppedItem = true;
            otherLeg.droppedItem = true;
        }
    }

    public void ToggleShoeCollider(bool enabled) {
        shoeBoxColliders[1].enabled = enabled;
    }
}

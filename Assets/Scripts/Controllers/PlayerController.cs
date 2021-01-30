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

    private float pickedUpItemTime = 0;
    private const float pickedUpItemDebounce = 0.2f;
    private List<Collider2D> overItems;
    private GameObject collectedItem = null;

    public LayerMask groundLayer;

    private bool isOnLeg = false;

    private int lives = 3;

    private float hitCoolDown = 2f;
    private float timeHit = 0f;

    protected void Start() {
        rb2d = GetComponent<Rigidbody2D>();
        overItems = new List<Collider2D>();

        for ( int i = 0; i < lives; i++ ) {
            UIController.instance.SetHearts( false );
        }
    }

	void Update () {
        Move();
        CheckForJump();
        BetterJump();
        CheckForPickUpOrDrop();
	}

    void Move() {
        float x = Input.GetAxisRaw("Horizontal");
        rb2d.velocity = new Vector2(x * speed, rb2d.velocity.y);
    }

    bool IsOnGround() {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.6f, groundLayer);
        return hit.collider != null;
    }

    void CheckForJump() {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) {
            if (IsOnGround()) {
                rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);
                SoundController.instance.PlaySingle("jumpSound");
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

    void CheckForPickUpOrDrop() {
        if (Input.GetKeyDown(KeyCode.E)) {
            if (collectedItem == null && overItems.Count > 0) {
                Collider2D collider = overItems[0];
                overItems.Remove(collider);
                overItems.Add(collider);
                collectedItem = collider.gameObject;
                (collectedItem.GetComponent<Item>() as Item).PickUpItem();
                collectedItem.transform.parent = gameObject.transform;
                pickedUpItemTime = Time.fixedTime;
                UIController.instance.SetText((collectedItem.GetComponent<Item>() as Item).itemTag.ToString(), UIController.TextObject.ITEM_TEXT);
            }
            else if (collectedItem != null && Time.fixedTime > pickedUpItemTime + pickedUpItemDebounce) {
                (collectedItem.GetComponent<Item>() as Item).DropItem();
                collectedItem.transform.parent = gameObject.transform.parent;
                collectedItem = null;
                UIController.instance.SetText("None", UIController.TextObject.ITEM_TEXT);
            }
        }
    }

    bool hasDied() {
        return ( lives <= 0 );
    }

    void OnTriggerEnter2D (Collider2D other) {
        if (other.gameObject.CompareTag("Item")) {
            overItems.Add(other);
        }
        else if (other.gameObject.CompareTag("Leg")) {
            Debug.Log("Over Leg");
        }
        else if (other.gameObject.CompareTag("Shoe")) {
            Debug.Log( "Shoe Hit!" );
            if (IsOnGround()) {
                float delta = Time.fixedTime - timeHit;
                Debug.Log( delta );
                if ( delta > hitCoolDown ) {
                    Debug.Log( "Took Damage!" );
                    takeDmg();
                    timeHit = Time.fixedTime;
                }
            }
        }
    }

    void OnTriggerExit2D (Collider2D other) {
        if (other.gameObject.CompareTag("Item")) {
            overItems.Remove(other);
        }
    }

    private void takeDmg() {
        lives -= 1;
        UIController.instance.SetHearts( true );

        // bounce back (augment this with direction?)
        rb2d.AddForce( transform.up * 650 + transform.right * 650 );
    }

    void checkForLeg() {
        if ( Input.GetKeyDown( KeyCode.Space ) ) {
            if ( isOnLeg ) {
                // TODO: drop code
            } else {
                // TODO: check to see if leg is nearby? then attach to it?
            }
        }
    }
}

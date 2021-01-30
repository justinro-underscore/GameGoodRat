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
    private GameObject overItem = null;
    private GameObject collectedItem = null;

    public LayerMask groundLayer;

    private bool isOnLeg = false;

    protected void Start() {
        rb2d = GetComponent<Rigidbody2D>();
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

    void CheckForJump() {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.6f, groundLayer);
            if (hit.collider != null) {
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
            if (collectedItem == null && overItem != null) {
                collectedItem = overItem;
                overItem = null;
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

    void OnTriggerEnter2D (Collider2D other) {
        if (collectedItem == null && overItem == null && other.gameObject.CompareTag("Item")) {
            overItem = other.gameObject;
            Debug.Log(overItem.GetInstanceID());
        }
    }

    void OnTriggerExit2D (Collider2D other) {
        if (overItem != null && other.gameObject.CompareTag("Item")) {
            overItem = null;
        }
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

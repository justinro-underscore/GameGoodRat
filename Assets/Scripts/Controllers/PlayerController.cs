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

    private enum State {
        GROUNDED,
        PANT_LEG
    };
    
    private Rigidbody2D rb2d;
    private State state;

    private float pickedUpItemTime = 0;
    private const float pickedUpItemDebounce = 0.2f;
    private List<Collider2D> overItems;
    private List<Collider2D> overLegs;
    private GameObject collectedItem = null;
    private Collider2D attachedLeg = null;
    private float lastLegPosY;

    public LayerMask groundLayer;

    private bool isOnLeg = false;

    protected void Start() {
        rb2d = GetComponent<Rigidbody2D>();
        overItems = new List<Collider2D>();
        overLegs = new List<Collider2D>();
        state = State.GROUNDED;
    }

	void Update () {
        Move();
        CheckForJump();
        BetterJump();
        CheckForPickUpOrDrop();
	}

    void Move() {
        float x = Input.GetAxisRaw("Horizontal");
        if (state == State.GROUNDED) {
            rb2d.velocity = new Vector2(x * speed, rb2d.velocity.y);
        }
        else {
            float legDiffY = attachedLeg.transform.position.y - lastLegPosY;
            lastLegPosY = attachedLeg.transform.position.y;
            transform.position += new Vector3(0, legDiffY);

            rb2d.velocity = new Vector2(0, x * speed);
        }
    }

    void OnBecameInvisible() {
        if (state == State.PANT_LEG) {
            rb2d.velocity = new Vector2(rb2d.velocity.x, 1);
            JumpOffLeg();
        }
    }

    bool IsOnGround() {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.6f, groundLayer);
        return hit.collider != null;
    }

    void CheckForJump() {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) {
            if (state == State.GROUNDED) {
                if (IsOnGround()) {
                    rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);
                    SoundController.instance.PlaySingle("jumpSound");
                }
                else if (overLegs.Count > 0) {
                    Collider2D collider = overLegs[0];
                    Bounds ratBounds = GetComponent<Collider2D>().bounds;
                    if (!(collider.bounds.Contains(ratBounds.min) && collider.bounds.Contains(ratBounds.max))) {
                        return;
                    }

                    overItems.Remove(collider);
                    overItems.Add(collider);
                    attachedLeg = collider;
                    (attachedLeg.transform.parent.GetComponent<Leg>() as Leg).ToggleShoeCollider(true);
                    lastLegPosY = attachedLeg.transform.position.y;
                    state = State.PANT_LEG;
                    rb2d.velocity = Vector2.zero;
                    transform.Rotate(new Vector3(0, 0, 90));
                    rb2d.gravityScale = 0;
                }
            }
            else if (state == State.PANT_LEG) {
                JumpOffLeg();
            }
        }
    }

    void JumpOffLeg() {
        transform.Rotate(new Vector3(0, 0, -90));
        state = State.GROUNDED;
        rb2d.gravityScale = 1;
        (attachedLeg.transform.parent.GetComponent<Leg>() as Leg).ToggleShoeCollider(false);
        attachedLeg = null;
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

    void OnTriggerEnter2D (Collider2D other) {
        if (other.gameObject.CompareTag("Item")) {
            overItems.Add(other);
        }
        else if (other.gameObject.CompareTag("Leg")) {
            overLegs.Add(other);
        }
        else if (other.gameObject.CompareTag("Shoe") && state == State.GROUNDED) {
            if (IsOnGround()) {
                Debug.Log("DEAD");
            }
        }
    }

    void OnTriggerExit2D (Collider2D other) {
        if (other.gameObject.CompareTag("Item")) {
            overItems.Remove(other);
        }
        else if (other.gameObject.CompareTag("Leg")) {
            overLegs.Remove(other);
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

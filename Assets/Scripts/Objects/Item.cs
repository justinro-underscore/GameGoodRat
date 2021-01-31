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
    [Range(0, 60)]
    [SerializeField]
    private float secondsSpentOnGround = 2f;

    private enum State {
        FALLING,
        GROUNDED,
        PICKED_UP,
        DISAPPEARING
    };

    private bool initialized = false;
    private Rigidbody2D rb2d;
    private new SpriteRenderer renderer;
    private State state;

    private Vector2 startFloatingPos = new Vector2();
    private float startFloatingTime = 0;

    private float disappearTime;

    public Constants.Items itemType;
    public LayerMask groundLayer;

    public void Init(Constants.Items itemType) {
        this.itemType = itemType;

        rb2d = GetComponent<Rigidbody2D>();
        renderer = GetComponent<SpriteRenderer>();

        state = State.FALLING;
        rb2d.velocity = new Vector2(0, -fallingSpeed);

        initialized = true;
    }

    void Update() {
        if (initialized) {
            switch (state) {
                case State.FALLING:
                    FallingUpdate();
                    break;
                case State.GROUNDED:
                    GroundedUpdate();
                    break;
                case State.PICKED_UP:
                    PickedUpUpdate();
                    break;
            }
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
            state = State.DISAPPEARING;
            disappearTime = 1f;
            Disappear();
        }
    }

    void PickedUpUpdate() {
        rb2d.position = transform.parent.position;
    }

    void Disappear() {
        if (disappearTime < 0.05f) {
            Destroy(gameObject);
        }
        float waitTime = 0.1f;
        if (!renderer.enabled) {
            disappearTime /= 1.5f;
            waitTime = disappearTime;
        }
        renderer.enabled = !renderer.enabled;
        Invoke("Disappear", waitTime);
    }

    public void PickUpItem() {
        CancelInvoke("Disappear");
        renderer.enabled = true;
        state = State.PICKED_UP;
        rb2d.velocity = Vector2.zero;
    }

    public void DropItem() {
        state = State.FALLING;
        rb2d.velocity = new Vector2(0, -fallingSpeed);
        transform.position += new Vector3(0, 0.2f);
    }
}

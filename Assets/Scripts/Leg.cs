using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leg : MonoBehaviour {
    private Constants.Outfit pantType;
    private Constants.Shoe shoeType;

    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rigidBody;
    private BoxCollider2D boxCollider;

    public void SetParams( Constants.Outfit pantType, Constants.Shoe shoeType ) {
        this.pantType = pantType;
        this.shoeType = shoeType;
    }

    public void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidBody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
    }
}

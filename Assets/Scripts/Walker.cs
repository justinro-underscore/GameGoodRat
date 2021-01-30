using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Walker : MonoBehaviour {
    private int walkingDirection = -1;
    
    private Constants.Outfit pantType;
    private Constants.Shoe shoeType;
    
    [Range(0, 2)]
    [SerializeField]
    private float walkingSpeed = 2f;

    public GameObject leg;

    public void Start() {
        SetParams( walkingSpeed, Constants.Outfit.JEANS, Constants.Shoe.SLIPPERS );
    }

    public void SetParams( float walkingSpeed, Constants.Outfit pantType, Constants.Shoe shoeType ) {
        this.walkingSpeed = walkingSpeed;
        this.pantType = pantType;
        this.shoeType = shoeType;

        Instantiate( leg, new Vector2( 2, 10 ), Quaternion.identity );
        Instantiate( leg, new Vector2( 5, 0 ), Quaternion.identity );

        // Oh god why
        leg.GetComponent<Leg>().SetParams( walkingSpeed, walkingDirection, pantType, shoeType );    
    }
}

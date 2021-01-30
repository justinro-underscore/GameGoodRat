using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Walker : MonoBehaviour {
    private Constants.Outfit pantType;
    private Constants.Shoe shoeType;
    
    [Range(0, 10)]
    [SerializeField]
    private float walkingSpeed = 10f;

    public GameObject leg;

    public void Start() {
        SetParams( walkingSpeed, Constants.Outfit.JEANS, Constants.Shoe.SLIPPERS );
    }

    public void SetParams( float walkingSpeed, Constants.Outfit pantType, Constants.Shoe shoeType ) {
        this.walkingSpeed = walkingSpeed;
        this.pantType = pantType;
        this.shoeType = shoeType;

        Instantiate( leg, new Vector2( Constants.OFFSCREEN_X - 2, Constants.OFFSCREEN_Y * 4 ), Quaternion.identity );
        // Instantiate( leg, new Vector2( Constants.OFFSCREEN_X + 2, 0 ), Quaternion.identity );
        Instantiate( leg, new Vector2( 0, 2 ), Quaternion.identity );

        // Oh god why
        leg.GetComponent<Leg>().SetParams( walkingSpeed, true, pantType, shoeType );    
    }
}

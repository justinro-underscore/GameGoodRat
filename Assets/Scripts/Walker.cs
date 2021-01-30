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

    private Leg legFG;
    private Leg legBG;

    public void SetParams( float walkingSpeed, Constants.Outfit pantType, Constants.Shoe shoeType ) {
        this.walkingSpeed = walkingSpeed;
        this.pantType = pantType;
        this.shoeType = shoeType;

        legFG = new Leg();
        legBG = new Leg();

        legFG.SetParams( walkingSpeed, walkingDirection, pantType, shoeType );
        legBG.SetParams( walkingSpeed, walkingDirection, pantType, shoeType );
    }
}

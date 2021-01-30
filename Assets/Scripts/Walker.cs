using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Walker : MonoBehaviour {
    private int walkingDirection;
    
    private Constants.Outfit pantType;
    private Constants.Shoe shoeType;

    private float walkingSpeed;

    private Leg legFG;
    private Leg legBG;

    public void SetParams( float walkingSpeed, Constants.Outfit pantType, Constants.Shoe shoeType ) {
        this.walkingSpeed = walkingSpeed;
        this.pantType = pantType;
        this.shoeType = shoeType;

        legFG = new Leg();
        legBG = new Leg();

        legFG.SetParams( pantType, shoeType );
        legBG.SetParams( pantType, shoeType );

        walkingDirection = -1;
    }

    // Walk function
    public void Walk() {
        
    }
}

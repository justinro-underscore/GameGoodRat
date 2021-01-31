using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Walker : MonoBehaviour {
    [Range(0, 10)]
    [SerializeField]
    private float movementX = 0f;

    public GameObject leg;

    public void Init (float walkingSpeed, bool walkingLeft, Constants.People personType) {
        // float distanceX = movementX + UnityEngine.Random.Range(0, 3);
        // float offset = UnityEngine.Random.Range(0, distanceX / 3f);
        // float secondStartingX = walkingLeft ? Constants.OFFSCREEN_X - offset : -Constants.OFFSCREEN_X + offset; // Onscreen
        // float firstStartingX = secondStartingX + (walkingLeft ? distanceX / 2f : -distanceX / 2f); // Offscreen

        // Leg leg1 = Instantiate( leg, new Vector2( firstStartingX, Constants.OFFSCREEN_Y ), Quaternion.identity ).GetComponent<Leg>();
        // Leg leg2 = Instantiate( leg, new Vector2( secondStartingX, Constants.OFFSCREEN_Y * 2 ), Quaternion.identity ).GetComponent<Leg>();

        // leg1.Init(leg2, walkingSpeed, walkingLeft, personType, distanceX);
        // leg2.Init(leg1, walkingSpeed, walkingLeft, personType, distanceX);

        Leg leg2 = Instantiate( leg, new Vector2( 0, Constants.OFFSCREEN_Y ), Quaternion.identity ).GetComponent<Leg>();

        leg2.Init(null, walkingSpeed, walkingLeft, Constants.People.BUSINESS_MAN, movementX);

        Leg leg3 = Instantiate( leg, new Vector2( 4, Constants.OFFSCREEN_Y ), Quaternion.identity ).GetComponent<Leg>();

        leg3.Init(null, walkingSpeed, walkingLeft, Constants.People.SWIMMER, movementX);

        Leg leg5 = Instantiate( leg, new Vector2( -4, Constants.OFFSCREEN_Y ), Quaternion.identity ).GetComponent<Leg>();

        leg5.Init(null, walkingSpeed, walkingLeft, Constants.People.WOMAN, movementX);
    }
}

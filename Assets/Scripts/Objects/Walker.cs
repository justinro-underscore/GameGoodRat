using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Walker : MonoBehaviour {
    public GameObject leg;

    public void Init (float walkingSpeed, bool walkingLeft, Constants.People personType) {
        GameObject leg1 = Instantiate( leg, new Vector2( Constants.OFFSCREEN_X - 2, Constants.OFFSCREEN_Y * 4 ), Quaternion.identity );
        GameObject leg2 = Instantiate( leg, new Vector2( Constants.OFFSCREEN_X + 2, 0 ), Quaternion.identity );

        (Constants.Outfit outfit, Constants.Shoe shoe) = Constants.personToOutfit[personType];

        leg1.GetComponent<Leg>().SetParams(walkingSpeed, walkingLeft, personType, outfit, shoe);    
        leg2.GetComponent<Leg>().SetParams(walkingSpeed, walkingLeft, personType, outfit, shoe);    
    }
}

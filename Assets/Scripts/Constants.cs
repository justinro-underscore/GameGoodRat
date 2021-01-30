using System.Collections;
using System.Collections.Generic;

public class Constants {
    public enum Items {
        SIX_SHOOTER,
        BRIEFCASE,
        WEDDING_RING,
        PHONE,
        ICE_CREAM,
        TEDDY_BEAR,
        SUNSCREEN,
        DOG_TAGS,
        WHEAT,
        PURSE,
        PRISON_KEY
    };
    
    public enum People {
        COWBOY,
        BUSINESS_MAN,
        BRIDE,
        GUY,
        KID,
        SLEEPY_PERSON,
        BEACH_GOER,
        ARMY_PERSON,
        FARMER,
        GIRL,
        PRISONER
    };

    static public IDictionary<Items, People> itemToPerson = new Dictionary<Items, People>() {
        {Items.SIX_SHOOTER, People.COWBOY},
        {Items.BRIEFCASE, People.BUSINESS_MAN},
        {Items.WEDDING_RING, People.BRIDE},
        {Items.PHONE, People.GUY},
        {Items.ICE_CREAM, People.KID},
        {Items.TEDDY_BEAR, People.SLEEPY_PERSON},
        {Items.SUNSCREEN, People.BEACH_GOER},
        {Items.DOG_TAGS, People.ARMY_PERSON},
        {Items.WHEAT, People.FARMER},
        {Items.PURSE, People.GIRL},
        {Items.PRISON_KEY, People.PRISONER}
    };
}
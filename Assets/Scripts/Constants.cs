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

    public enum Outfit {
        JEANS,
        BUSINESS_PANTS,
        WEDDING_DRESS,
        CARGO_PANTS,
        SHORTS,
        SWEATPANTS,
        SWIM_TRUNKS,
        CAMO_PANTS,
        OVERALLS,
        SKIRT,
        PRISON_OVERALLS
    };

    public enum Shoe {
        COWBOY_BOOTS,
        DRESS_SHOES,
        MYSTERY,
        SNEAKERS,
        TENNIS_SHOES,
        SLIPPERS,
        FLIP_FLOPS,
        ARMY_BOOTS,
        FARMER_BOOTS,
        HIGH_HEELS,
        SHACKLES
    };

    static public IDictionary<People, Items> personToItem = new Dictionary<People, Items>() {
        {People.COWBOY, Items.SIX_SHOOTER},
        {People.BUSINESS_MAN, Items.BRIEFCASE},
        {People.BRIDE, Items.WEDDING_RING},
        {People.GUY, Items.PHONE},
        {People.KID, Items.ICE_CREAM},
        {People.SLEEPY_PERSON, Items.TEDDY_BEAR},
        {People.BEACH_GOER, Items.SUNSCREEN},
        {People.ARMY_PERSON, Items.DOG_TAGS},
        {People.FARMER, Items.WHEAT},
        {People.GIRL, Items.PURSE},
        {People.PRISONER, Items.PRISON_KEY}
    };

    static public IDictionary<People, (Outfit, Shoe)> personToOutfit = new Dictionary<People, (Outfit, Shoe)>() {
        {People.COWBOY, (Outfit.JEANS, Shoe.COWBOY_BOOTS)},
        {People.BUSINESS_MAN, (Outfit.BUSINESS_PANTS, Shoe.DRESS_SHOES)},
        {People.BRIDE, (Outfit.WEDDING_DRESS, Shoe.MYSTERY)},
        {People.GUY, (Outfit.CARGO_PANTS, Shoe.SNEAKERS)},
        {People.KID, (Outfit.SHORTS, Shoe.TENNIS_SHOES)},
        {People.SLEEPY_PERSON, (Outfit.SWEATPANTS, Shoe.SLIPPERS)},
        {People.BEACH_GOER, (Outfit.SWIM_TRUNKS, Shoe.FLIP_FLOPS)},
        {People.ARMY_PERSON, (Outfit.CAMO_PANTS, Shoe.ARMY_BOOTS)},
        {People.FARMER, (Outfit.OVERALLS, Shoe.FARMER_BOOTS)},
        {People.GIRL, (Outfit.SKIRT, Shoe.HIGH_HEELS)},
        {People.PRISONER, (Outfit.PRISON_OVERALLS, Shoe.SHACKLES)}
    };

    public const int OFFSCREEN_X = 11;
    public const int OFFSCREEN_Y = 5;
}

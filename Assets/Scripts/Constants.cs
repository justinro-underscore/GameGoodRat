using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constants {
    public enum Items {
        SIX_SHOOTER,
        WATCH,
        RING,
        SUNSCREEN
    };
    
    public enum People {
        COWBOY,
        BUSINESS_MAN,
        WOMAN,
        SWIMMER
    };

    public enum Outfit {
        JEANS_W_COWBOY_BOOTS,
        BUSINESS_PANTS,
        RED_DRESS,
        SWIM_TRUNKS
    };

    static public List<Outfit> bareLegs = new List<Outfit>(){ Outfit.RED_DRESS, Outfit.SWIM_TRUNKS };

    static public IDictionary<People, Items> personToItem = new Dictionary<People, Items>() {
        {People.COWBOY, Items.SIX_SHOOTER},
        {People.BUSINESS_MAN, Items.WATCH},
        {People.WOMAN, Items.RING},
        {People.SWIMMER, Items.SUNSCREEN}
    };

    static public IDictionary<Items, Sprite> itemSprites = new Dictionary<Items, Sprite>() {
        {Items.SIX_SHOOTER, SpriteController.instance.sixShooter},
        {Items.WATCH, SpriteController.instance.watch},
        {Items.RING, SpriteController.instance.ring},
        {Items.SUNSCREEN, SpriteController.instance.sunscreen}
    };

    static public IDictionary<People, Outfit> personToOutfit = new Dictionary<People, Outfit>() {
        {People.COWBOY, Outfit.JEANS_W_COWBOY_BOOTS},
        {People.BUSINESS_MAN, Outfit.BUSINESS_PANTS},
        {People.WOMAN, Outfit.RED_DRESS},
        {People.SWIMMER, Outfit.SWIM_TRUNKS}
    };

    static public IDictionary<Outfit, Sprite> outfitSprites = new Dictionary<Outfit, Sprite>() {
        {Outfit.JEANS_W_COWBOY_BOOTS, SpriteController.instance.cowboy},
        {Outfit.BUSINESS_PANTS, SpriteController.instance.businessMan},
        {Outfit.RED_DRESS, SpriteController.instance.woman},
        {Outfit.SWIM_TRUNKS, SpriteController.instance.swimmer}
    };

    public const int OFFSCREEN_X = 11;
    public const int OFFSCREEN_Y = 5;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constants {
    public enum Items {
        SIX_SHOOTER,
        WATCH,
        RING,
        SUNSCREEN,
        WRENCH,
        PHONE
    };

    public enum People {
        COWBOY,
        BUSINESS_MAN,
        WOMAN,
        SWIMMER,
        CONSTRUCTION_WORKER,
        RUNNER
    };

    public enum Outfit {
        JEANS_W_COWBOY_BOOTS,
        BUSINESS_PANTS,
        RED_DRESS,
        SWIM_TRUNKS,
        CONSTRUCTION,
        JOGGING_SHORTS
    };

    static public List<Outfit> bareLegs = new List<Outfit>(){
        Outfit.RED_DRESS,
        Outfit.SWIM_TRUNKS,
        Outfit.JOGGING_SHORTS
    };

    static public IDictionary<People, Items> personToItem = new Dictionary<People, Items>() {
        {People.COWBOY, Items.SIX_SHOOTER},
        {People.BUSINESS_MAN, Items.WATCH},
        {People.WOMAN, Items.RING},
        {People.SWIMMER, Items.SUNSCREEN},
        {People.CONSTRUCTION_WORKER, Items.WRENCH},
        {People.RUNNER, Items.PHONE}
    };

    static public IDictionary<Items, Sprite> itemSprites = new Dictionary<Items, Sprite>() {
        {Items.SIX_SHOOTER, SpriteController.instance.sixShooter},
        {Items.WATCH, SpriteController.instance.watch},
        {Items.RING, SpriteController.instance.ring},
        {Items.SUNSCREEN, SpriteController.instance.sunscreen},
        {Items.WRENCH, SpriteController.instance.wrench},
        {Items.PHONE, SpriteController.instance.phone}
    };

    static public IDictionary<People, Outfit> personToOutfit = new Dictionary<People, Outfit>() {
        {People.COWBOY, Outfit.JEANS_W_COWBOY_BOOTS},
        {People.BUSINESS_MAN, Outfit.BUSINESS_PANTS},
        {People.WOMAN, Outfit.RED_DRESS},
        {People.SWIMMER, Outfit.SWIM_TRUNKS},
        {People.CONSTRUCTION_WORKER, Outfit.CONSTRUCTION},
        {People.RUNNER, Outfit.JOGGING_SHORTS}
    };

    static public IDictionary<Outfit, Sprite> outfitSprites = new Dictionary<Outfit, Sprite>() {
        {Outfit.JEANS_W_COWBOY_BOOTS, SpriteController.instance.cowboy},
        {Outfit.BUSINESS_PANTS, SpriteController.instance.businessMan},
        {Outfit.RED_DRESS, SpriteController.instance.woman},
        {Outfit.SWIM_TRUNKS, SpriteController.instance.swimmer},
        {Outfit.CONSTRUCTION, SpriteController.instance.worker},
        {Outfit.JOGGING_SHORTS, SpriteController.instance.runner}
    };

    public const int OFFSCREEN_X = 11;
    public const int OFFSCREEN_Y = 5;
}

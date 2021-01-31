using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteController : MonoBehaviour {
    public static SpriteController instance = null;

    public Sprite sixShooter;
    public Sprite watch;
    public Sprite ring;
    public Sprite sunscreen;
    public Sprite wrench;

    public Sprite cowboy;
    public Sprite businessMan;
    public Sprite woman;
    public Sprite swimmer;
    public Sprite worker;

    void Start() {
        if (instance == null) {
            instance = this;
        }
        else if (instance != this) {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
}

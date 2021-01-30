using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour {
    public enum TextObject {
        ITEM_TEXT,
        SCORE_TEXT,
        HI_SCORE_TEXT
    }
    
    public static UIController instance = null; // So this instance can be used in other classes

    public TMP_Text itemText;

    public TMP_Text scoreText;
    public TMP_Text hiScoreText;

    public GameObject heartGameObject;
    private List<GameObject> hearts;
    
    public Sprite heartFull;
    public Sprite heartEmpty;

    void Awake()
    {
        if (instance == null) {
            instance = this;
        }
        else if (instance != this) {
            Destroy(gameObject);
        }
    
        hearts = new List<GameObject>();
        foreach ( Transform child in heartGameObject.transform ) {
            hearts.Add( child.gameObject );
        }
    }

    public void SetText(string text, TextObject obj) {
        TMP_Text textObj = null;
        switch (obj) {
            case TextObject.ITEM_TEXT:
                textObj = itemText;
                break;
            case TextObject.SCORE_TEXT:
                textObj = scoreText;
                break;
            case TextObject.HI_SCORE_TEXT:
                textObj = hiScoreText;
                break;
        }
        textObj.text = text;
    }

    public void SetHearts( bool lifeLost ) {
        foreach ( GameObject heart in hearts ) {
            Image heartImage = heart.GetComponent<Image>();
            if ( lifeLost ) {
                if ( heartImage.sprite == heartFull ) {
                    heartImage.sprite = heartEmpty;
                    break;
                }
            } else {
                if ( heartImage.sprite == heartEmpty ) {
                    heartImage.sprite = heartFull;
                    break;
                }
            }
        }
    }
}

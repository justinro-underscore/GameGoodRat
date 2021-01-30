using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    void Awake()
    {
        if (instance == null) {
            instance = this;
        }
        else if (instance != this) {
            Destroy(gameObject);
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
}

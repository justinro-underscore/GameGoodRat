using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour {
    public enum TextObject {
        ITEM_TEXT,
        SCORE_TEXT,
        HI_SCORE_TEXT,
        USER_HIGH_SCORE_TEXT,
        PLAYER_INITIALS_TEXT,
        LEADERBOARD_SCORE_TEXT,
        LEADERBOARD_TEXT
    }
    
    public static UIController instance = null; // So this instance can be used in other classes

    public TMP_Text itemText;

    public TMP_Text scoreText;
    public TMP_Text hiScoreText;

    public TMP_Text userHighScoreText;
    public TMP_Text playerInitialsText;

    public TMP_Text leaderBoardScoreText;
    public TMP_Text leaderBoardText;

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
            case TextObject.USER_HIGH_SCORE_TEXT:
                textObj = userHighScoreText;
                break;
            case TextObject.PLAYER_INITIALS_TEXT:
                textObj = playerInitialsText;
                break;
            case TextObject.LEADERBOARD_SCORE_TEXT:
                textObj = leaderBoardScoreText;
                break;
            case TextObject.LEADERBOARD_TEXT:
                textObj = leaderBoardText;
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

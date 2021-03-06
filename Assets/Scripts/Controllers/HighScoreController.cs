using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HighScoreController : MonoBehaviour {
    public static HighScoreController hsInstance = null;
    
    private bool running;
    
    private int[] initials;
    private int initialIndex;
    private bool cursorBlink;

    private int userScore = 0;

    public void Awake() {
        if ( null == hsInstance ) {
            hsInstance = this;
            DontDestroyOnLoad( this.gameObject );
        } else {
            Destroy( this.gameObject );
        }

        running = false;
        initials = new[]{ (int)'A', (int)'A', (int)'A' };
        initialIndex = 0;
        cursorBlink = false;
    }

    public void ShowHighScore( int scoreVal ) {
        running = true;
        userScore = scoreVal;
        UIController.instance.SetText( "<b>New High Score: " + scoreVal + "</b>", UIController.TextObject.USER_HIGH_SCORE_TEXT );
        InvokeRepeating( "CursorBlinkToggle", 0.0f, 0.5f );
    }

    public void Update() {
        if ( running ) {
            UIController.instance.SetText( "Enter Initials: " + GetInitials( true ), UIController.TextObject.PLAYER_INITIALS_TEXT );

            if ( Input.GetKeyDown( KeyCode.UpArrow ) || Input.GetKeyDown( KeyCode.DownArrow ) ) {
                bool moveUp = Input.GetKeyDown( KeyCode.UpArrow );
                ChangeCharacter( moveUp );
            }

            if ( Input.GetKeyDown( KeyCode.RightArrow ) || Input.GetKeyDown( KeyCode.LeftArrow ) ) {
                bool moveRight = Input.GetKeyDown( KeyCode.RightArrow );
                MoveCursor( moveRight );
            }
        }
    }

    private void ChangeCharacter( bool moveUp ) {
        initials[initialIndex] = initials[initialIndex] + ( moveUp ? -1 : 1 );
        
        if ( initials[initialIndex] > 'Z' ) {
            initials[initialIndex] = 'A';
        } else if ( initials[initialIndex] < 'A' ) {
            initials[initialIndex] = 'Z';
        }
        SetCursorBlinkTrue();
    }

    private void MoveCursor( bool moveRight ) {
        initialIndex += ( moveRight ? 1 : -1 );

        if ( initialIndex > 2 ) {
            initialIndex = 0;
        } else if ( initialIndex < 0 ) {
            initialIndex = 2;
        }
        SetCursorBlinkTrue();
    }

    public string GetInitials( bool editing ) {
        string result = "";
        string spacer = "";
        string prefix = "";
        string suffix = "";

        string firstInitial = ( (char)initials[0] ).ToString();
        string middleInitial = ( (char)initials[1] ).ToString();
        string lastInitial = ( (char)initials[2] ).ToString();

        if ( editing ) {
            spacer = " ";
            prefix = "<b>";
            suffix = "</b>";
        }
        
        if ( 0 == initialIndex ) {
            firstInitial = prefix + firstInitial + suffix;
        } else if ( 1 == initialIndex ) {
            middleInitial = prefix + middleInitial + suffix;
        } else {
            lastInitial = prefix + lastInitial + suffix;
        }

        result = firstInitial + spacer + middleInitial + spacer + lastInitial;

        return ( result );
    }

    private void CursorBlinkToggle() {
        cursorBlink = !( cursorBlink );
    }

    private void SetCursorBlinkTrue() {
        CancelInvoke( "CursorBlinkToggle" );
        cursorBlink = true;
        InvokeRepeating( "CursorBlinkToggle", 0.0f, 0.5f );
    }

    public void Reset() {
        running = false;
        CancelInvoke( "CursorBlinkToggle" );
        UIController.instance.SetText( "", UIController.TextObject.USER_HIGH_SCORE_TEXT );
        UIController.instance.SetText( "", UIController.TextObject.PLAYER_INITIALS_TEXT );
    }
}

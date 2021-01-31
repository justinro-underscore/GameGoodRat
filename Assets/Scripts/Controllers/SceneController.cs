using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour {
    public static SceneController scInstance = null;

    public string menuScene;
    public string mainLevelScene;
    public string endingScene;

    public void Awake() {
        if ( null == scInstance ) {
            scInstance = this;
            DontDestroyOnLoad( this.gameObject );
        } else {
            Destroy( this.gameObject );
        }
    }

    public void Update() {
        string currScene = SceneManager.GetActiveScene().name;

        if ( menuScene == currScene ) {
            menuUpdate();
        } else if ( mainLevelScene == currScene ) {
            mainLevelUpdate();
        } else if ( endingScene == currScene ) {
            endingUpdate();
        } 
    }

    private void menuUpdate() {
        if ( Input.GetKeyDown( KeyCode.Return ) ) {
            LoadLevel( mainLevelScene );
        }  
    }

    private void mainLevelUpdate() {}

    private void endingUpdate() {
        if ( Input.GetKeyDown( KeyCode.Return ) ) {
            LoadLevel( menuScene );
        }
    }

    public static void LoadLevel( string levelName ) {
        SceneManager.LoadScene( levelName );
    }
}

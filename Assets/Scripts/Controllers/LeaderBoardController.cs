using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LeaderBoardController : MonoBehaviour {
    public static LeaderBoardController lbInstance = null;

    public void Awake() {
        if ( null == lbInstance ) {
            lbInstance = this;
            DontDestroyOnLoad( this.gameObject );
        } else {
            Destroy( this.gameObject );
        }
    }

    public void Update() {
        if ( Input.GetKeyDown( KeyCode.Return ) ) {
            SceneController.LoadLevel( "MainMenu" );
        }
    }
}

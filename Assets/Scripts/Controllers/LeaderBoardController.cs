using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using TMPro;

public struct Score {
    public string playerInitials;
    public int score;
}

// Thanks to: Generic Toast
// https://blog.generistgames.com/creating-a-simple-unity-online-leaderboard/
public class LeaderBoardController : MonoBehaviour {
    public static LeaderBoardController lbInstance = null;

    private const string db_url = "https://gamegoodrat.000webhostapp.com/highscore.php";

    private List<Score> scores;

    private bool isRunning;

    public void Awake() {
        if ( null == lbInstance ) {
            lbInstance = this;
            DontDestroyOnLoad( this.gameObject );
        } else {
            Destroy( this.gameObject );
        }

        scores = RetrieveScores();
        isRunning = false;
    }

    public void Update() {}

    public void ShowLeaderBoard( int userScore, string userName ) {
        isRunning = true;

        UIController.instance.SetText( "Your score: " + userScore, UIController.TextObject.LEADERBOARD_SCORE_TEXT );
        
        bool hasDisplayedPlayerScore = false;
        int scoreIndex = 1;
        string prefix = "";
        string suffix = "";

        string leaderBoardText = "";
        foreach ( Score highScore in scores ) {
            if ( false == hasDisplayedPlayerScore && userScore == highScore.score && userName == highScore.playerInitials ) {
                prefix = "<b>";
                suffix = "</b>";
                hasDisplayedPlayerScore = true;
            } else {
                prefix = "";
                suffix = "";
            }

            leaderBoardText += prefix + scoreIndex.ToString() + " " + highScore.playerInitials + " : " + highScore.score + suffix + "\n";
            scoreIndex++;
        }

        UIController.instance.SetText( leaderBoardText, UIController.TextObject.LEADERBOARD_TEXT );
    }

    public List<Score> GetCurrentScores() {
        return ( scores );
    }

    public void UpdateScores() {
        scores = RetrieveScores();
    }

    public List<Score> RetrieveScores() {
        List<Score> scores = new List<Score>();
        StartCoroutine( GetScores( scores ) );
        return ( scores );
    }

    public void PublishScore( string playerInitials, int score ) {
        StartCoroutine( PostScore( playerInitials, score ) );
    }

    private IEnumerator GetScores( List<Score> scores ) {
        WWWForm form = new WWWForm();
        form.AddField( "retrieve_leaderboard", "true" );
        
        using ( UnityWebRequest www = UnityWebRequest.Post( db_url, form ) ) {
            yield return www.SendWebRequest();

            if ( www.isNetworkError || www.isHttpError ) {
                Debug.Log( www.error );
            } else {
                string contents = www.downloadHandler.text;
                using ( StringReader reader = new StringReader( contents ) ) {
                    string line;
                    while ( ( line = reader.ReadLine() ) != null ) {
                        Score entry = new Score();
                        entry.playerInitials = line;
                        entry.score = Int32.Parse( reader.ReadLine() );
                        scores.Add(entry);
                    }
                }
            }
        }
    }

    private IEnumerator PostScore( string initials, int score ) {
        WWWForm form = new WWWForm();
        form.AddField( "post_leaderboard", "true" );
        form.AddField( "name", initials );
        form.AddField( "score", score );

        using ( UnityWebRequest www = UnityWebRequest.Post( db_url, form ) ) {
            yield return www.SendWebRequest();
            
            if ( www.isNetworkError || www.isHttpError ) {
                Debug.Log( www.error );
            } else {
                Debug.Log( "Score Published!" );
            }
        }
    }

    public void Reset() {
        isRunning = false;
        UIController.instance.SetText( "", UIController.TextObject.LEADERBOARD_SCORE_TEXT );
        UIController.instance.SetText( "", UIController.TextObject.LEADERBOARD_TEXT );
    }
}

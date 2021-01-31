using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
    public static GameController instance = null;

    [Range(1, 5)]
    [SerializeField]
    public int minNumberOfLegs = 2;
    [Range(5, 15)]
    [SerializeField]
    public int maxNumberOfLegs = 10;
    public int currNumLegs = 0;
    private int numLegs;
    [Range(1, 10)]
    [SerializeField]
    private float startWalkerSpawnerTime = 8;
    [Range(1, 5)]
    [SerializeField]
    private float minWalkerSpawnerTime = 1;
    private float walkerSpawnerTime = 2;

    private int score;
    private string playerInitials;
    
    public GameObject rat;
    public GameObject panelText;

    public GameObject playerPrefab;
    public GameObject itemPrefab;
    public GameObject walkerPrefab;
    public GameObject menuBG;

    public bool gameOver = false;
    private bool isRunning;

    private SceneController.Level currentLevel;

    void Start() {
        if (instance == null) {
            instance = this;
        }
        else if (instance != this) {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

        score = 0;
        playerInitials = "";

        // Start with menu level
        SceneController.LoadLevel( SceneController.Level.MAIN_MENU );
        playerInitials = "AAA";
    }

    void Update() {
        if ( Input.GetKeyDown( KeyCode.Escape ) ) {
            Application.Quit();
        }

        switch( SceneController.GetCurrentLevel() ) {
            case SceneController.Level.MAIN_MENU:
                RunMenu();
                break;
            case SceneController.Level.MAIN_LEVEL:
                RunGame();
                break;
            case SceneController.Level.HIGH_SCORE:
                RunHighScore();
                break;
            case SceneController.Level.LEADERBOARD:
                RunLeaderBoard();
                break;
        }
    }

    void StartGame() {
        gameOver = false;
        score = 0;
        numLegs = minNumberOfLegs * 2;
        walkerSpawnerTime = startWalkerSpawnerTime;
        currNumLegs = 0;
        rat.SetActive(true);
        rat.GetComponent<PlayerController>()._Start();
        rat.transform.position = new Vector2(0, 0);
        panelText.SetActive(true);
        SoundController.instance.StartMusic();
        Invoke("TrySpawningWalker", 0.5f);
    }

    void TrySpawningWalker() {
        int currNumLegsDiv = (int)Mathf.Floor(currNumLegs / 2);
        int numLegsDiv = (int)Mathf.Floor(numLegs / 2);
        if (currNumLegsDiv < numLegsDiv) {
            SpawnWalker();
            numLegs = Mathf.Min(numLegs + 1, maxNumberOfLegs);
            walkerSpawnerTime = Mathf.Max(minWalkerSpawnerTime, walkerSpawnerTime - 0.1f);
        }

        Invoke("TrySpawningWalker", walkerSpawnerTime);
    }

    void StopGame() {
        CancelInvoke( "TrySpawningWalker" );
    }

    void RunMenu() {
        if ( Input.GetKeyDown( KeyCode.Return ) ) {
            SceneController.LoadLevel( SceneController.Level.MAIN_LEVEL );

            menuBG.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
            PlayerController.StartGame();
            StartGame();
        }
    }

    void RunGame() {
        UIController.instance.SetText( score.ToString(), UIController.TextObject.SCORE_TEXT );
        if ( LeaderBoardController.lbInstance.GetCurrentScores().Capacity > 0 ) {
            UIController.instance.SetText( LeaderBoardController.lbInstance.GetCurrentScores()[0].score.ToString(), UIController.TextObject.HI_SCORE_TEXT );
        }

        if ( gameOver ) {
            if ( IsHighScore() ) {
                SceneController.LoadLevel( SceneController.Level.HIGH_SCORE );
                HighScoreController.hsInstance.ShowHighScore( score );
            } else {
                SceneController.LoadLevel( SceneController.Level.LEADERBOARD );
                LeaderBoardController.lbInstance.ShowLeaderBoard( score, playerInitials ); 
            }

            PlayerController.StopGame();
            StopGame();
        }
    }

    void RunHighScore() {
        if ( Input.GetKeyDown( KeyCode.Return ) ) {
            SceneController.LoadLevel( SceneController.Level.LEADERBOARD );
            playerInitials = HighScoreController.hsInstance.GetInitials( false );
            HighScoreController.hsInstance.Reset();

            LeaderBoardController.lbInstance.PublishScore( playerInitials, score );
            Invoke( "UpdateScores", 0.25f );
            Invoke( "ShowLeaderBoard", 1.0f );   
        }
    }

    void UpdateScores() {
        LeaderBoardController.lbInstance.UpdateScores();
    }

    void ShowLeaderBoard() {
        LeaderBoardController.lbInstance.ShowLeaderBoard( score, playerInitials );
    }

    void RunLeaderBoard() {
        if ( Input.GetKeyDown( KeyCode.Return ) ) {
            rat.SetActive(false);
            panelText.SetActive(false);
            GameObject[] legs = GameObject.FindGameObjectsWithTag("FullLeg");
            foreach(GameObject leg in legs) {
                GameObject.Destroy(leg);
            }

            SceneController.LoadLevel( SceneController.Level.MAIN_MENU );
            LeaderBoardController.lbInstance.Reset();

            menuBG.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        }
    }

    public void DropOffItem(GameObject itemObject, GameObject legObject) {
        Constants.Items itemType = (itemObject.GetComponent<Item>() as Item).itemType;
        Constants.People personType = (legObject.GetComponent<Leg>() as Leg).personType;

        if (Constants.personToItem[personType] == itemType) {
            // TODO: make certain items worth more / store point val in item
            SoundController.instance.PlaySingle("itemCorrect");
            score += 100;
        }
        else {
            SoundController.instance.PlaySingle("itemWrong");
            score -= 50;
        }
        UIController.instance.SetText( score.ToString(), UIController.TextObject.SCORE_TEXT );
    }

    public void SpawnWalker() {
        GameObject walker = Instantiate(walkerPrefab, new Vector3(0, 0), Quaternion.identity);
        Array peopleTypes = Enum.GetValues(typeof(Constants.People));
        Constants.People personType = (Constants.People)peopleTypes.GetValue((int)Mathf.Floor(UnityEngine.Random.value * peopleTypes.Length));
        (walker.GetComponent<Walker>() as Walker).Init(UnityEngine.Random.Range(5f, 20f), UnityEngine.Random.value <= 0.5f, personType);
    }

    private bool IsHighScore() {
        bool isHighScore = false;

        foreach ( Score highScore in LeaderBoardController.lbInstance.GetCurrentScores() ) {
            if ( score > highScore.score ) {
                isHighScore = true;
            }   
        }

        if ( 9 > LeaderBoardController.lbInstance.GetCurrentScores().Capacity ) {
            isHighScore = true;
        }

        return ( isHighScore );
    }
}

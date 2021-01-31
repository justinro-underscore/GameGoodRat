using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
    public static GameController instance = null;

    private int score;
    private string playerInitials;

    public GameObject playerPrefab;
    public GameObject itemPrefab;
    public GameObject walkerPrefab;

    public bool gameOver = false;

    void Start() {
        if (instance == null) {
            instance = this;
        }
        else if (instance != this) {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

        score = 0;
        playerInitials = "AAA";

        // SpawnWalker();
    }

    void Update() {
        if ( false == gameOver ) {
            UIController.instance.SetText( score.ToString(), UIController.TextObject.SCORE_TEXT );
        } else {
            SceneController.LoadLevel( "HighScore" );
            HighScoreController.hsInstance.ShowHighScore( score );
            Destroy( this.gameObject );
        }
    }

    void StartGame() {
    }

    public void DropOffItem(GameObject itemObject, GameObject legObject) {
        Constants.Items itemType = (itemObject.GetComponent<Item>() as Item).itemType;
        Constants.People personType = (legObject.GetComponent<Leg>() as Leg).personType;

        if (Constants.personToItem[personType] == itemType) {
            // TODO: make certain items worth more / store point val in item
            score += 100;
        }
        else {
            score -= 50;
        }
    }

    public void SpawnWalker() {
        GameObject walker = Instantiate(walkerPrefab, new Vector3(0, 0), Quaternion.identity);
        Array peopleTypes = Enum.GetValues(typeof(Constants.People));
        Constants.People personType = (Constants.People)peopleTypes.GetValue((int)Mathf.Floor(UnityEngine.Random.value * peopleTypes.Length));
        (walker.GetComponent<Walker>() as Walker).Init(UnityEngine.Random.Range(5f, 20f), UnityEngine.Random.value <= 0.5f, personType);
    }
}

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

        // StartGame();
    }

    void Update() {
        if ( gameOver ) {
            SceneController.LoadLevel( "HighScore" );
            HighScoreController.hsInstance.ShowHighScore( score );
            Destroy( this.gameObject );
        }
    }

    void StartGame() {
        numLegs = minNumberOfLegs * 2;
        walkerSpawnerTime = startWalkerSpawnerTime;
        currNumLegs = 0;
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
        UIController.instance.SetText( score.ToString(), UIController.TextObject.SCORE_TEXT );
    }

    public void SpawnWalker() {
        GameObject walker = Instantiate(walkerPrefab, new Vector3(0, 0), Quaternion.identity);
        Array peopleTypes = Enum.GetValues(typeof(Constants.People));
        Constants.People personType = (Constants.People)peopleTypes.GetValue((int)Mathf.Floor(UnityEngine.Random.value * peopleTypes.Length));
        (walker.GetComponent<Walker>() as Walker).Init(UnityEngine.Random.Range(5f, 20f), UnityEngine.Random.value <= 0.5f, personType);
    }
}

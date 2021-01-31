using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
    private int score;

    public GameObject playerPrefab;
    public GameObject itemPrefab;
    public GameObject walkerPrefab;

    void Start() {
        SpawnWalker();
    }

    void Update() {
        // UIController.instance.SetText(DateTime.Now.Second.ToString(), UIController.TextObject.SCORE_TEXT);
    }

    void StartGame() {
    }

    public void SpawnWalker() {
        GameObject walker = Instantiate(walkerPrefab, new Vector3(0, 0), Quaternion.identity);
        Array peopleTypes = Enum.GetValues(typeof(Constants.People));
        Constants.People personType = (Constants.People)peopleTypes.GetValue((int)Mathf.Floor(UnityEngine.Random.value * peopleTypes.Length));
        (walker.GetComponent<Walker>() as Walker).Init(UnityEngine.Random.Range(5f, 20f), UnityEngine.Random.value <= 0.5f, personType);
    }
}

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

    public void SpawnItem() {
        GameObject item = Instantiate(itemPrefab, new Vector3(UnityEngine.Random.Range(-Constants.OFFSCREEN_X, Constants.OFFSCREEN_X), Constants.OFFSCREEN_Y), Quaternion.identity);
        Array itemTypes = Enum.GetValues(typeof(Constants.Items));
        (item.GetComponent<Item>() as Item).itemTag = (Constants.Items)itemTypes.GetValue((int)Mathf.Floor(UnityEngine.Random.value * itemTypes.Length));
    }

    public void SpawnWalker() {
        GameObject walker = Instantiate(walkerPrefab, new Vector3(0, 0), Quaternion.identity);
        // Array itemTypes = Enum.GetValues(typeof(Constants.Items));
        // (item.GetComponent<Item>() as Item).itemTag = (Constants.Items)itemTypes.GetValue((int)Mathf.Floor(UnityEngine.Random.value * itemTypes.Length));
    }
}

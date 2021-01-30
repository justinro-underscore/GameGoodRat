using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
    private int score;

    public GameObject playerPrefab;
    public GameObject itemPrefab;

    public int OFFSCREEN_X = 11;
    public int OFFSCREEN_Y = 5;

    void Start() {
    }

    void Update() {
        // UIController.instance.SetText(DateTime.Now.Second.ToString(), UIController.TextObject.SCORE_TEXT);
    }

    void StartGame() {
    }

    public void SpawnItem() {
        GameObject item = Instantiate(itemPrefab, new Vector3(UnityEngine.Random.Range(-OFFSCREEN_X, OFFSCREEN_X), OFFSCREEN_Y), Quaternion.identity);
        Array itemTypes = Enum.GetValues(typeof(Constants.Items));
        (item.GetComponent<Item>() as Item).itemTag = (Constants.Items)itemTypes.GetValue((int)Mathf.Floor(UnityEngine.Random.value * itemTypes.Length));
    }
}

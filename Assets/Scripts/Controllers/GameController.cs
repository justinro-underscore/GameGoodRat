using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; // TODO Delete

public class GameController : MonoBehaviour {
    private int score;

    void Start() {
    }

    void Update() {
        UIController.instance.SetText(DateTime.Now.Second.ToString(), UIController.TextObject.SCORE_TEXT);
    }
}

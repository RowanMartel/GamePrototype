using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    static string baseText;
    static TMP_Text scoreText;
    static int score;

    void Start()
    {
        score = 0;
        scoreText = GetComponent<TMP_Text>();
        baseText = scoreText.text;
        modifyScore(0);
    }

    static public void modifyScore(int modifier)
    {
        score += modifier;
        scoreText.text = baseText + score;
    }
}
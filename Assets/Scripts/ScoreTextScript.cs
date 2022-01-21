using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreTextScript : MonoBehaviour
{
    private Text text;
    public static int coinAmount;

    private void Start()
    {
        text = GameObject.Find("Score").GetComponent<Text>();
    }

    private void Update()
    {
        text.text = "Score: " + coinAmount.ToString();
    }
}

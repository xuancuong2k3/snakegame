using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class scoreWindow : MonoBehaviour
{
    private TextMeshProUGUI scoreText;
    // Start is called before the first frame update
    private void Awake()
    {
        scoreText = transform.Find("scoreText").GetComponent<TextMeshProUGUI>();
    }
    private void Update()
    {
        scoreText.text = GameHandler.GetScore().ToString();
    }
}

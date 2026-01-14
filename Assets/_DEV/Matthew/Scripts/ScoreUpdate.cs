using UnityEngine;

public class ScoreUpdate : MonoBehaviour
{
    [SerializeField]
    private int score = 4;

    [SerializeField]
    private TMPro.TMP_Text scoreText;

    public void AddScore()
    {
        score -= 1;

        scoreText.text = "Orders To Complete: " + score.ToString();
    }
}
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    
    public static ScoreManager instance;

    [SerializeField]public TMPro.TextMeshProUGUI scoreText;
    [SerializeField]public TMPro.TextMeshProUGUI scoreShadow;

    int score = 0;

    private void Awake()
    {
        instance = this;
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        scoreText.text = score.ToString();
        scoreShadow.text = score.ToString();
    }

    public void AddPoints()
    {
        score += 100;
        scoreText.text = score.ToString();
        scoreShadow.text = score.ToString();
    }

    public void LosePoints()
    {
        score -= 300;
        scoreText.text = score.ToString();
        scoreShadow.text = score.ToString();
    }
}

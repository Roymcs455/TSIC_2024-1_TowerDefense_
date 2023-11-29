using UnityEngine;
using TMPro;
public class GameManager : MonoBehaviour
{
    // Singleton instance
    private static GameManager _instance;
    public static GameManager Instance => _instance;

    public TextMeshProUGUI scoreText;
    public GameObject StartScreen;
    public GameObject TutorialScreen;
    public GameObject GameOverScreen;

    public GameObject GameHUD;
    private static int playerScore = 0;
    [SerializeField]private GameObject[] towers;
    public enum GameStates
    {
        StartScreen,
        Playing,
        GameOver,
    }
    public static GameStates currentState;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);
        StartScreen.SetActive(true);
        //scoreText = GetComponent<TextMeshProUGUI>();
        towers = GameObject.FindGameObjectsWithTag("Tower");

    }
    public void StartGame()
    {
        currentState = GameStates.Playing;
        StartScreen.SetActive(false);
        TutorialScreen.SetActive(false);
        GameOverScreen.SetActive(false);
        GameHUD.SetActive(true);
        playerScore = 0;

    }
    public void AddScore(int score)
    {
        playerScore += score;
        
    }
    void Update()
    {
        if(scoreText != null)
        {
            scoreText.text = "Score: "+playerScore;
        }
    }
    public void GameOver()
    {
        currentState = GameStates.GameOver;
        DestroyAllEnemies();
        GameOverScreen.SetActive(true);
    }
    public void DestroyAllEnemies()
    {
        GameObject [] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var item in enemies)
        {
            Destroy(item);
        }
    }
    public void Quit()
    {
        System.Diagnostics.Process.GetCurrentProcess().Kill();
    }
    
    
}

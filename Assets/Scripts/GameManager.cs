using UnityEngine;
using TMPro;
public class GameManager : MonoBehaviour
{
    // Singleton instance
    private static GameManager _instance;
    public static GameManager Instance => _instance;

    public TextMeshProUGUI scoreText;
    public GameObject StartScreen;
    public GameObject GameOverScreen;
    public GameObject GameHUD;
    public GameObject[] towers;
    public static int playerScore = 0;
    public static float spawnRate = 2.0f;
    
    public enum GameStates
    {
        StartScreen,
        Playing,
        GameOver,
    }
    public static GameStates currentState;
    [SerializeField]Castle castle;

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
        
        towers = GameObject.FindGameObjectsWithTag("Tower");
        

    }
    public void StartGame()
    {
        currentState = GameStates.Playing;
        StartScreen.SetActive(false);
        GameOverScreen.SetActive(false);
        GameHUD.SetActive(true);
        playerScore = 0;
        castle.StartGame();
        spawnRate = 2.0f;
        foreach (var item in towers)
        {
            item.SendMessage("Restart");
        }

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
        GameHUD.SetActive(false);
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
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        //System.Diagnostics.Process.GetCurrentProcess().Kill();
        Application.Quit();
        #endif
    }
    
    
}

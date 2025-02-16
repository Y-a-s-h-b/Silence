using MoreMountains.CorgiEngine;
using MoreMountains.Tools;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterHandleDeath : MonoBehaviour, MMEventListener<CorgiEngineEvent>
{
    //[SerializeField] private int initialRequiredPoints = 10; // Required points to survive -> If points less than threshold || Minimum Points to avoid losing life
    [SerializeField] private int pointIncreaseThreshold = 40; // Threshold after which required points to survive will increase
    [SerializeField] private int afterThresholdRequiredPoints = 20; //  Required points to survive -> If points greater than threshold

    private Character character;
    private CheckPoint hellCheckPoint;
    private GameManager gameManager;
    private CoinDropHandler coinDropHandler;

    private bool isDead = false;
    private bool isGameOver = false;
    private int currentPoints = 0;

    private void OnEnable() => this.MMEventStartListening<CorgiEngineEvent>();
    private void OnDisable() => this.MMEventStopListening<CorgiEngineEvent>();

    private void Awake()
    {
        character = GetComponent<Character>();
        coinDropHandler = GetComponent<CoinDropHandler>();
        //SceneManager.LoadScene("Hell", LoadSceneMode.Additive);
    }

    private void Start()
    {
        for (int j = 0; j < SceneManager.sceneCount; j++)
        {
            Scene loadedScene = SceneManager.GetSceneAt(j);
            if (loadedScene.name == "Hell")
            {
                hellCheckPoint = GameObject.FindGameObjectWithTag("HellCheckPoint").GetComponent<CheckPoint>();
            }
        }
        gameManager = GameManager.Instance;
    }

    private void Update()
    {
    }

    public void OnMMEvent(CorgiEngineEvent eventType)
    {
        // Checks If Character has enough points on death
        if (eventType.EventType == CorgiEngineEventTypes.PlayerDeath)
        {
            if (gameManager.Points > 0)
            {
                int pointsToSurvive = gameManager.Points >= pointIncreaseThreshold ? afterThresholdRequiredPoints : gameManager.Points;
                gameManager.AddPoints(-pointsToSurvive);
                currentPoints = gameManager.Points;
                coinDropHandler.DropRandomCoins(pointsToSurvive);
            }
            else
            {
                isDead = true;
            }
        }

        if (eventType.EventType == CorgiEngineEventTypes.Respawn && isGameOver)
        {
            GameManager.Instance.CurrentLives = 1;
            CorgiEngineEvent.Trigger(CorgiEngineEventTypes.LivesCountChanged);
            isGameOver = false;
        }

        // Set Current Lives to 1 on GameOver
        if (eventType.EventType == CorgiEngineEventTypes.GameOver)
        {
            //LevelManager.Instance.SetCurrentCheckpoint(LevelManager.Instance.DebugSpawn);
            isGameOver = true;
        }

        // Spawns Character to Hell if character is declared dead
        if (eventType.EventType == CorgiEngineEventTypes.Respawn && isDead)
        {
            if (hellCheckPoint == null)
            {
                hellCheckPoint = GameObject.FindGameObjectWithTag("HellCheckPoint").GetComponent<CheckPoint>();
            }

            if (hellCheckPoint == null)
            {
                Debug.LogError("No Hell Check Point in the current Scene");
                LevelManager.Instance.CurrentCheckPoint.SpawnPlayer(character);
            }
            else
            {
                hellCheckPoint.SpawnPlayer(character);
            }
            isDead = false;
        }

        if (eventType.EventType == CorgiEngineEventTypes.Respawn && !isDead)
        {
            gameManager.SetPoints(currentPoints);
        }

    }
}

using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement; 

public class GameManager : MonoBehaviour
{
    public UIDocument UIDoc;
    private Label m_FoodLabel;
    private int m_FoodAmount = 50;

    public static GameManager Instance { get; private set; }

    public BoardManager BoardManager;
    public PlayerController PlayerController;

    public TurnManager TurnManager { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    void Start()
    {
        TurnManager = new TurnManager(); 
        TurnManager.OnTick += OnTurnHappen; 

        BoardManager.Init(); // BoardManager'ý baþlat.
        PlayerController.Spawn(BoardManager, new Vector2Int(1, 1)); 

        m_FoodLabel = UIDoc.rootVisualElement.Q<Label>("FoodLabel");
        m_FoodLabel.text = "Health : " + m_FoodAmount;
    }

    void OnTurnHappen()
    {
        ChangeFood(-1);
    }

    public void ChangeFood(int amount)
    {
        m_FoodAmount += amount;
        m_FoodLabel.text = "Health : " + m_FoodAmount;

        
        if (m_FoodAmount <= 0)
        {
            RestartScene();
        }
    }

    private void RestartScene()
    {
        // Sahneyi yeniden yükleyin
        Debug.Log("Health is 0. Restarting the scene.");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
    }
}

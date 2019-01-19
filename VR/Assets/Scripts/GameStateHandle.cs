using UnityEngine;
public class GameStateHandle : MonoBehaviour
{
    public GameStateContent prefabGameState;
    public GameStateContent GameState
    {
        get
        {
            makeGameState();
            return gameState;
        }
    }
    private GameStateContent gameState;
    private void makeGameState()
    {
        if (gameState == null)
        {
            gameState = GameObject.FindObjectOfType<GameStateContent>();
        }
        if (gameState == null)
        {
            gameState = Instantiate(prefabGameState);
            DontDestroyOnLoad(gameState.gameObject);
        }

    }
    
    void Start()
    {
        makeGameState();
    }
}

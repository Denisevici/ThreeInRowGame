using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField]
    private GameObject gameBoard;

    [field: SerializeField]
    public Panel Panel { get; private set; }

    public Player[] Players { get; private set; }

    public void PlayWithBot()
    {
        Players = new Player[] { Player.player1, Player.bot };
        Instantiate(gameBoard);
        GameObject.FindGameObjectWithTag("GameBoard").GetComponent<GameBoard>().CreateGameBoard();
    }

    public void PlayWithFriendOnline()
    {
        Players = new Player[] { Player.player1, Player.player2 };
        Instantiate(gameBoard);
        GameObject.FindGameObjectWithTag("GameBoard").GetComponent<GameBoard>().CreateGameBoard();
    }

    public void PlayWithFriendOffline()
    {
        Players = new Player[] { Player.player1, Player.player2 };
        Instantiate(gameBoard);
        GameObject.FindGameObjectWithTag("GameBoard").GetComponent<GameBoard>().CreateGameBoard();
    }
}

public enum Player
{
    nobody,
    player1,
    player2,
    bot
}

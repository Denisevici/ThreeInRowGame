using UnityEngine;

public class GameLogic : MonoBehaviour
{
    [SerializeField]
    private Figure[] figures;

    [SerializeField]
    private Tile[] tiles;

    private Panel panel;

    private Player[] players;

    private Player[,] table = new Player[3, 3];

    private int player1TurnsLast;

    private int player2TurnsLast;

    private int player1Wins = 0;

    private int player2Wins = 0;

    private GameState CheckWin()
    {
        int countHorizontal1Player1 = 0;
        int countHorizontal1Player2 = 0;
        int countHorizontal2Player1 = 0;
        int countHorizontal2Player2 = 0;

        for (int i = 0; i < 3; i++)
        {
            if (table[i, i] == players[0])
                countHorizontal1Player1++;
            else if (table[i, i] == players[1])
                countHorizontal1Player2++;
            if (table[i, 2 - i] == players[0])
                countHorizontal2Player1++;
            else if (table[i, 2 - i] == players[1])
                countHorizontal2Player2++;

            int countRowPlayer1 = 0;
            int countRowPlayer2 = 0;
            int countColumnPlayer1 = 0;
            int countColumnPlayer2 = 0;

            for (int j = 0; j < 3; j++)
            {
                if (table[i, j] == players[0])
                    countColumnPlayer1++;
                else if (table[i, j] == players[1])
                    countColumnPlayer2++;
                if (table[j, i] == players[0])
                    countRowPlayer1++;
                else if (table[j, i] == players[1])
                    countRowPlayer2++;
            }

            if (countRowPlayer1 == 3 || countColumnPlayer1 == 3)
            {
                Player1Win();
                return GameState.player1Win;
            }
            else if (countRowPlayer2 == 3 || countColumnPlayer2 == 3)
            {
                Player2Win();
                return GameState.player2Win;
            }
        }

        if (countHorizontal1Player1 == 3 || countHorizontal2Player1 == 3)
        {
            Player1Win();
            return GameState.player1Win;
        }
        if (countHorizontal1Player2 == 3 || countHorizontal2Player2 == 3)
        {
            Player2Win();
            return GameState.player2Win;
        }
        if (player1TurnsLast == 0 && player2TurnsLast == 0)
        {
            Draw();
            return GameState.draw;
        }
        return GameState.battle;
    }

    private void Player1Win()
    {
        player1Wins++;
        panel.GameOver(player1Wins, player2Wins);
    }

    private void Player2Win()
    {
        player2Wins++;
        panel.GameOver(player1Wins, player2Wins);
    }

    public void Draw()
    {
        panel.GameOver(player1Wins, player2Wins);
    }

    public void SetFiguresTilesPlayersPanel(Figure[] figures, Tile[] tiles, Player[] players, Panel panel)
    {
        this.figures = figures;
        this.tiles = tiles;
        this.players = players;
        this.panel = panel;
    }

    public GameState UpdateBoard(Vector2Int coordinates, Player player, float value)
    {
        player1TurnsLast = 7;
        player2TurnsLast = 7;

        foreach (Figure figure in figures)
        {
            if (!figure.TilesForCheck[coordinates.x, coordinates.y])
            {
                if (figure.Height >= value && !figure.WasMoved)
                {
                    figure.PossibleTurnsAmountDecrease(coordinates);
                }
            }
            if (figure.PossibleTurnsAmount <= 0)
            {
                if (figure.Owner == players[0])
                    player1TurnsLast--;
                else
                    player2TurnsLast--;
            }
        }

        table[coordinates.x, coordinates.y] = player;
        return CheckWin();
    }

    public Vector2Int AmountPlayersTurnsLast()
    {
        return new Vector2Int(player1TurnsLast, player2TurnsLast);
    }

    public void Restart()
    {
        foreach (Figure figure in figures)
            figure.ResetFigure();
        foreach (Tile tile in tiles)
            tile.ResetTile();  
          
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                table[i, j] = Player.nobody;
            }
        }
    }

    public void BackToMenu()
    {
        foreach (Figure figure in figures)
            Destroy(figure.gameObject);
        foreach (Tile tile in tiles)
            Destroy(tile.gameObject);
    }
}

public enum GameState
{
    battle,
    player1Win,
    player2Win,
    draw
}

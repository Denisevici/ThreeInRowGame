using UnityEngine;

public class MoveFigures : MonoBehaviour
{
    [SerializeField]
    private GameLogic gameLogic;

    [SerializeField]
    private Bot bot;

    private Figure humanFigure, botFigure;

    private Figure[] figures;

    private Tile humanTile, botTile;

    private Tile[] tiles;

    private Panel panel;

    private Player playerTurn;

    private Player[] players;

    private GameState gameState;

    private Vector2Int amountPlayersTurnsLast;

    private bool figureIsSelected = false;

    private void Update()
    {
        if (gameState == GameState.battle && Input.GetMouseButtonDown(0))
            SelectFigure();
        if (gameState == GameState.battle && figureIsSelected)
            FigureFollowsCursor();
    }

    private RaycastHit2D ThrowRaycastFromCursor()
    {
        return Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
    }

    private void SelectFigure()
    {
        RaycastHit2D hit = ThrowRaycastFromCursor();
        if (CheckTagFromCollider(hit, "Figure"))
        {
            humanFigure = hit.collider.GetComponentInParent<Figure>();
            if (humanFigure.Owner == playerTurn)
            {
                if (!humanFigure.WasMoved)
                    figureIsSelected = true;
                else
                    humanFigure = null;
            }
        }
    }

    private void FigureFollowsCursor()
    {
        bool flag = true;
        if (Input.GetMouseButtonUp(0))
        {
            humanFigure.GetComponentInChildren<Collider2D>().enabled = false;
            RaycastHit2D hit = ThrowRaycastFromCursor();
            if (CheckTagFromCollider(hit, "Tile"))
            {
                humanTile = hit.collider.GetComponentInParent<Tile>();
                if (humanTile.Height > humanFigure.Height)
                {
                    PlaceFigureOnTile(humanTile, humanFigure);
                    flag = false;
                }
            }
            if (flag)
            {
                humanFigure.GetComponentInChildren<Collider2D>().enabled = true;
                humanFigure.MoveBackToStartPosition();
                figureIsSelected = false;
            }
        }
        else
            humanFigure.FollowCursor(Camera.main.ScreenToWorldPoint(Input.mousePosition));
    }

    private void PlaceFigureOnTile(Tile tile, Figure figure)
    {
        tile.SetTile(figure.Height, figure.Owner);
        figure.MoveOnTile(tile.GetComponentInChildren<Transform>().position);
        figureIsSelected = false;
        gameState = gameLogic.UpdateBoard(tile.TableCoordainates, tile.Player, tile.Height);
        amountPlayersTurnsLast = gameLogic.AmountPlayersTurnsLast();

        if (gameState == GameState.battle)
        {
            if (playerTurn == players[0] && amountPlayersTurnsLast.y > 0)
                NextTurn(players[1], false, true);
            else if (playerTurn == players[1] && amountPlayersTurnsLast.x > 0)
                NextTurn(players[0], true, false);
            else if (playerTurn == players[0] && amountPlayersTurnsLast.x > 0)
                NextTurn(players[0], true, false);
            else if (playerTurn == players[1] && amountPlayersTurnsLast.y > 0)
                NextTurn(players[1], false, true);
            else
            {
                gameState = GameState.draw;
                gameLogic.Draw();
            }
        }
    }
    private void NextTurn(Player player, bool firstBool, bool secondBool)
    {
        playerTurn = player;
        panel.ChangeTurnText(firstBool, secondBool);
        if (player == Player.bot)
        {
            BotsMove();
            PlaceFigureOnTile(botTile, botFigure);
        }
    }

    private void BotsMove()
    {
        Vector3 answer = bot.MakeMove(humanFigure, humanTile);
        foreach (Tile tile in tiles)
        {
            if (tile.TableCoordainates == new Vector2Int((int)answer.x, (int)answer.y))
            {
                botTile = tile;
                break;
            }
        }
        foreach (Figure figure in figures)
        {
            if (figure.Height == answer.z && !figure.WasMoved && figure.Owner == Player.bot)
            {
                botFigure = figure;
                break;
            }
        }
    }

    private bool CheckTagFromCollider(RaycastHit2D hit, string tag)
    {
        if (hit.transform != null)
            return hit.collider.CompareTag(tag);
        return false;
    }

    public void FirstMove()
    {
        gameState = GameState.battle;

        int index = Random.Range(0, 2);
        playerTurn = players[index];

        if (index == 0)
            panel.ChangeTurnText(true, false);
        else
            panel.ChangeTurnText(false, true);

        if (playerTurn == Player.bot)
        {
            BotsMove();
            PlaceFigureOnTile(botTile, botFigure);
        }
    }

    public void Restart()
    {
        humanFigure = null;
        humanTile = null;
        figureIsSelected = false;
        gameState = GameState.battle;
        gameLogic.Restart();
        bot.Restart();
        FirstMove();
    }

    public void BackToMenu()
    {
        gameLogic.BackToMenu();
        Destroy(gameObject);
    }

    public void SetFiguresTilesPlayersPanel(Figure[] figures, Tile[] tiles, Player[] players, Panel panel)
    {
        this.figures = figures;
        this.tiles = tiles;
        this.players = players;
        this.panel = panel;
    }
}
using UnityEngine;

public class GameBoard : MonoBehaviour
{
    [SerializeField]
    private GameObject tilePrefab;

    [SerializeField]
    private GameObject player1Big;

    [SerializeField]
    private GameObject player1Middle;

    [SerializeField]
    private GameObject player1Small;

    [SerializeField]
    private GameObject player2Big;

    [SerializeField]
    private GameObject player2Middle;

    [SerializeField]
    private GameObject player2Small;

    [SerializeField]
    private Transform gameBoard;

    [SerializeField]
    private GameLogic gameLogic;

    [SerializeField]
    private MoveFigures moveFigures;

    private Transform cam;

    private Panel panel;

    private GameObject camGameObject;
    
    private Vector3 tileLocalScale;

    private Figure[] figures;

    private Tile[] tiles;

    private Player[] players;

    private float realBoardWidth;

    private void CreateTiles()
    {
        tiles = new Tile[9];
        GameObject tile;
        int index = 0;
        for (int i = -1; i < 2; i++)
        {
            for (int j = -1; j < 2; j++)
            {
                tile = Instantiate(tilePrefab, new Vector3(gameBoard.position.x + tileLocalScale.x * i, gameBoard.position.y + tileLocalScale.y * j, -0.1f), Quaternion.identity);
                tiles[index] = tile.GetComponent<Tile>();
                tiles[index].SetTableCoordiantes(new Vector2Int(i + 1, j + 1));
                tiles[index].ChangeScale(95 * tileLocalScale / 100);
                index++;
            }
        }
        PlaceFigures();
    }

    private void PlaceFigures()
    {
        figures = new Figure[14];
        GameObject figure;
        int index = 0;
        Vector2 basePoint = new Vector2(gameBoard.position.x - gameBoard.localScale.x / 2 - tileLocalScale.x / 2, gameBoard.position.y);
        for (int i = 0; i < 2; i++)
        {
            figure = Instantiate(player1Big, new Vector3(basePoint.x + tileLocalScale.x / 2 + tileLocalScale.x * i * 3, basePoint.y - tileLocalScale.y * 3, -0.1f), Quaternion.identity);
            index = SetFigure(figure, players[0], -5, 90, index);
            figure = Instantiate(player2Big, new Vector3(basePoint.x + tileLocalScale.x / 2 + tileLocalScale.x * i * 3, basePoint.y + tileLocalScale.y * 3, -0.1f), Quaternion.identity);
            index = SetFigure(figure, players[1], -5, 90, index);
        }
        for (int i = 2; i < 5; i++)
        {
            figure = Instantiate(player1Middle, new Vector3(basePoint.x + tileLocalScale.x * (i - 1), basePoint.y - tileLocalScale.y * 2, -0.1f), Quaternion.identity);
            index = SetFigure(figure, players[0], -4, 70, index);
            figure = Instantiate(player2Middle, new Vector3(basePoint.x + tileLocalScale.x * (i - 1), basePoint.y + tileLocalScale.y * 2, -0.1f), Quaternion.identity);
            index = SetFigure(figure, players[1], -4, 70, index);
        }
        for (int i = 5; i < 7; i++)
        {
            figure = Instantiate(player1Small, new Vector3(basePoint.x + tileLocalScale.x * (i - 3.5f), basePoint.y - tileLocalScale.y * 3, -0.1f), Quaternion.identity);
            index = SetFigure(figure, players[0], -3, 50, index);
            figure = Instantiate(player2Small, new Vector3(basePoint.x + tileLocalScale.x * (i - 3.5f), basePoint.y + tileLocalScale.y * 3, -0.1f), Quaternion.identity);
            index = SetFigure(figure, players[1], -3, 50, index);
        }
        gameLogic.SetFiguresTilesPlayersPanel(figures, tiles, players, panel);
        moveFigures.SetFiguresTilesPlayersPanel(figures, tiles, players, panel);
        moveFigures.FirstMove();
    }

    private int SetFigure(GameObject figure, Player player, float height, float scalePercent, int index)
    {
        figures[index] = figure.GetComponent<Figure>();
        figures[index].SetFigure(player, height);
        figures[index].ChangeScale(scalePercent * tileLocalScale / 100);
        index++;
        return index;
    }

    public void CreateGameBoard()
    {
        camGameObject = GameObject.Find("Main Camera");
        cam = camGameObject.GetComponent<Transform>();
        players = cam.GetComponent<Menu>().Players;
        panel = cam.GetComponent<Menu>().Panel;

        gameBoard.position = cam.position - new Vector3(0f, 0f, cam.position.z);
        realBoardWidth = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x - Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).x;
        tileLocalScale = new Vector3(realBoardWidth/5, realBoardWidth/5, 0.01f);
        gameBoard.transform.localScale = 3 * tileLocalScale;
        CreateTiles();
    }
}

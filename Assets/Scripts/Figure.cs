using UnityEngine;

public class Figure : MonoBehaviour
{
    [SerializeField]
    private Transform figure;

    private Vector3 startPosition;

    private int possibleTurnsAmount = 9;

    public Player Owner { get; private set; }

    public int PossibleTurnsAmount { get { return possibleTurnsAmount; } }

    public float Height { get; private set; }

    public bool WasMoved { get; private set; } = false;

    public bool[,] TilesForCheck { get; private set; } = new bool[3, 3];

    public void MoveOnTile(Vector3 point)
    {
        figure.position = point + new Vector3(0f, 0f, Height);
        WasMoved = true;
        possibleTurnsAmount = 0;
        figure.GetComponentInParent<Collider2D>().enabled = false;
    }

    public void FollowCursor(Vector3 point)
    {
        figure.position = point - new Vector3(0f, 0f, Height + 1f);
    }

    public void MoveBackToStartPosition()
    {
        figure.position = startPosition;
    }

    public void PossibleTurnsAmountDecrease(Vector2Int coordinates)
    {
        TilesForCheck[coordinates.x, coordinates.y] = true;
        possibleTurnsAmount--;
    }

    public void ResetFigure()
    {
        figure.GetComponentInParent<Collider2D>().enabled = true;
        possibleTurnsAmount = 9;
        WasMoved = false;
        TilesForCheck = new bool[3, 3];
        MoveBackToStartPosition();
    }

    public void SetFigure(Player player, float height)
    {
        Owner = player;
        Height = height;
        startPosition = transform.position;
    }

    public void ChangeScale(Vector3 scale)
    {
        figure.localScale = scale;
    }
}
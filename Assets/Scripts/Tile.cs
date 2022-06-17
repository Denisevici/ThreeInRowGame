using UnityEngine;

public class Tile : MonoBehaviour
{
    private Transform tileTransform;

    public Player Player { get; private set; } = Player.nobody;

    public Vector2Int TableCoordainates { get; private set; }

    public float Height { get; private set; } = 0f;

    public void SetTile(float height, Player player)
    {
        Height = height;
        Player = player;
    }

    public void SetTableCoordiantes(Vector2Int coordinates)
    {
        TableCoordainates = coordinates;
    }

    public void ResetTile()
    {
        Height = 0f;
        Player = Player.nobody;
    }

    public void ChangeScale(Vector3 scale)
    {
        tileTransform = gameObject.GetComponent<Transform>();
        tileTransform.localScale = scale;
    }
}

using UnityEngine;

public class Bot : MonoBehaviour
{
    private Player[,] ownersTable = new Player[3, 3];

    private int botFiguresLeft = 7;

    private int humanFiguresLeft = 7;

    private float[,] heightTable = new float[3, 3];

    private float[] humanFigures = { -5, -5, -4, -4, -4, -3, -3 };

    private float[] botFigures = { -5, -5, -4, -4, -4, -3, -3 };

    public Vector3 MakeMove(Figure figure, Tile tile) 
    {
        Vector3 answer;
        if (figure != null && tile != null)
        {
            ownersTable[tile.TableCoordainates.x, tile.TableCoordainates.y] = Player.player1;
            heightTable[tile.TableCoordainates.x, tile.TableCoordainates.y] = figure.Height;

            for (int i = 0; i < humanFiguresLeft; i++)
            {
                if (humanFigures[i] == figure.Height)
                {
                    humanFiguresLeft = DecriseFigures(i, humanFiguresLeft, humanFigures);
                    break;
                }
            }
        }

        answer = AttackDefence(true);
        if (answer != new Vector3(0, 0, 0))
            return answer;

        answer = AttackDefence(false);
        if (answer != new Vector3(0, 0, 0))
            return answer;

        answer = CheckCenterTile();
        if (answer != new Vector3(0, 0, 0))
            return answer;

        return FindRandomTile();
    }

    private Vector3 CheckCenterTile()
    {
        float random = Random.Range(0f, 1f);
        if (random >= 0.5f)
        {
            if (ownersTable[1, 1] != Player.bot && DoesHasFigureBigger(heightTable[1,1], botFiguresLeft, botFigures))
            {
                float height = GetHeightBigger(heightTable[1, 1]);
                UpdateInfo(height, new Vector2Int(1, 1));
                return new Vector3(1, 1, height);
            }
        }
        return new Vector3(0, 0, 0);
    }

    private Vector3 AttackDefence(bool isAttack)
    {
        int horizontal1Count = 0;
        int horizontal2Count = 0;
        Vector2Int horizontal1Coordinate = new Vector2Int(4, 4);
        Vector2Int horizontal2Coordinate = new Vector2Int(4, 4);
        bool bestHorizonatal1PositionWasFound = false;
        bool bestHorizonatal2PositionWasFound = false;

        for (int i = 0; i < 3; i++)
        {
            Switch(ref horizontal1Count, new Vector2Int(i, i), ref horizontal1Coordinate, ref bestHorizonatal1PositionWasFound, isAttack);
            Switch(ref horizontal2Count, new Vector2Int(i, 2 - i), ref horizontal2Coordinate, ref bestHorizonatal2PositionWasFound, isAttack);

            int rowCount = 0;
            int columnCount = 0;
            Vector2Int rowCoordinate = new Vector2Int(4, 4);
            Vector2Int columnCoordinate = new Vector2Int(4, 4);
            bool bestColumnPositionWasFound = false;
            bool bestRowPositionWasFound = false;

            for (int j = 0; j < 3; j++)
            {
                Switch(ref columnCount, new Vector2Int(i, j), ref columnCoordinate, ref bestColumnPositionWasFound, isAttack);
                Switch(ref rowCount, new Vector2Int(j, i), ref rowCoordinate, ref bestRowPositionWasFound, isAttack);
            }

            if (columnCount == 2 && columnCoordinate != new Vector2Int(4, 4))
            {
                float height = GetHeightBigger(humanFigures[0]);
                UpdateInfo(height, columnCoordinate);
                return new Vector3(columnCoordinate.x, columnCoordinate.y, height);
            }
            if (rowCount == 2 && rowCoordinate != new Vector2Int(4, 4))
            {
                float height = GetHeightBigger(humanFigures[0]);
                UpdateInfo(height, rowCoordinate);
                return new Vector3(rowCoordinate.x, rowCoordinate.y, height);
            }
        }

        if (horizontal1Count == 2 && horizontal1Coordinate != new Vector2Int(4, 4))
        {
            float height = GetHeightBigger(humanFigures[0]);
            UpdateInfo(height, horizontal1Coordinate);
            return new Vector3(horizontal1Coordinate.x, horizontal1Coordinate.y, height);
        }
        if (horizontal2Count == 2 && horizontal2Coordinate != new Vector2Int(4, 4))
        {
            float height = GetHeightBigger(humanFigures[0]);
            UpdateInfo(height, horizontal2Coordinate);
            return new Vector3(horizontal2Coordinate.x, horizontal2Coordinate.y, height);
        }

        return new Vector3(0, 0, 0);
    }

    private void Switch(ref int count, Vector2Int tableCoordinate, ref Vector2Int foundCoordinate, ref bool bestCoordinateWasFound, bool isAttack)
    {
        switch (ownersTable[tableCoordinate.x, tableCoordinate.y])
        {
            case Player.player1:
                {
                    if (isAttack)
                    {
                        if (DoesHasFigureBigger(heightTable[tableCoordinate.x, tableCoordinate.y], botFiguresLeft, botFigures))
                            foundCoordinate = tableCoordinate;
                        else
                            count--;
                    }
                    else
                    {
                        count++;
                        if (DoesHasFigureBigger(heightTable[tableCoordinate.x, tableCoordinate.y], botFiguresLeft, botFigures))
                        {
                            bestCoordinateWasFound = true;
                            foundCoordinate = tableCoordinate;
                        }
                    }
                    break;
                }
            case Player.nobody:
                {
                    if (isAttack)
                        foundCoordinate = tableCoordinate;
                    else
                    {
                        if (!bestCoordinateWasFound)
                            foundCoordinate = tableCoordinate;
                    }
                    break;
                }
            case Player.bot:
                {
                    if (isAttack)
                        count++;
                    else
                    {
                        if (DoesHasFigureBigger(heightTable[tableCoordinate.x, tableCoordinate.y], humanFiguresLeft, humanFigures))
                        {
                            if (!bestCoordinateWasFound)
                            {
                                if (DoesHasFigureBigger(heightTable[tableCoordinate.x, tableCoordinate.y], botFiguresLeft, botFigures))
                                    foundCoordinate = tableCoordinate;
                            }
                        }
                        else
                            count--;
                    }
                    break;
                }
        }
    }

    private Vector3 FindRandomTile()
    {
        int kx = Random.Range(0, 2);
        int ky = Random.Range(0, 2);
        int firstX = 0;
        int lastX = 2;
        bool flagX = true;
        int firstY = 0;
        int lastY = 2;
        bool flagY = true;

        if (kx == 1)
        {
            firstX = 2;
            lastX = 0;
            flagX = false;
        }
        if (ky == 1)
        {
            firstY = 2;
            lastY = 0;
            flagY = false;
        }
                
        for (int x = firstX; flagX ? x < lastX + 1 : x >= lastX; x = flagX? x + 1 : x - 1)
        {
            for (int y = firstY; flagY ? y < lastY + 1 : y >= lastY; y = flagY? y + 1: y - 1)
            {
                if (ownersTable[x, y] != Player.bot )
                {
                    if (ownersTable[x, y] == Player.nobody)
                    {
                        float height = GetHeightBigger(heightTable[x, y]);
                        UpdateInfo(height, new Vector2Int(x, y));
                        return new Vector3(x, y, height);
                    }
                    else if (heightTable[x, y] != -5)
                    {
                        if (DoesHasFigureBigger(heightTable[x, y], botFiguresLeft, botFigures))
                        {
                            float height = GetHeightBigger(heightTable[x, y]);
                            UpdateInfo(height, new Vector2Int(x, y));
                            return new Vector3(x, y, height);
                        }
                    }
                }
            }
        }
        return new Vector3(0, 0, 0);
    }

    private float GetHeightBigger(float height)
    {
        float tempVar;
        for (int i = botFiguresLeft - 1; i >= 0; i--)
        {
            if (height > botFigures[i])
            {
                tempVar = botFigures[i];
                botFiguresLeft = DecriseFigures(i, botFiguresLeft, botFigures);
                return tempVar;
            }
        }
        tempVar = botFigures[0];
        botFiguresLeft = DecriseFigures(0, botFiguresLeft, botFigures);
        return tempVar;
    }
    

    private int DecriseFigures(int index, int figuresLeft, float[] figures)
    {
        if (index == 6)
        {
            figures[figuresLeft - 1] = 0;
            return figuresLeft--;
        }
        for (int i = index + 1; i < figuresLeft; i++)
            figures[i - 1] = figures[i];
        figures[figuresLeft - 1] = 0;
        return figuresLeft--;
    }

    private bool DoesHasFigureBigger(float height, int figuresLeft, float[] figures)
    {
        for (int i = 0; i < figuresLeft; i++)
        {
            if (figures[i] < height)
                return true;
        }
        return false;
    }

    private void UpdateInfo(float height, Vector2Int coordinates)
    {
        ownersTable[coordinates.x, coordinates.y] = Player.bot;
        heightTable[coordinates.x, coordinates.y] = height;
    }

    public void Restart()
    {
        ownersTable = new Player[3, 3];
        botFiguresLeft = 7;
        humanFiguresLeft = 7;
        heightTable = new float[3, 3];
        humanFigures = new float[] { -5, -5, -4, -4, -4, -3, -3 };
        botFigures = new float[] { -5, -5, -4, -4, -4, -3, -3 };
    }
}
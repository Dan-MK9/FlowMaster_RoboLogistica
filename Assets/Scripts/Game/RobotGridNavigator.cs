using System.Collections;
using UnityEngine;

public class RobotGridNavigator : MonoBehaviour
{
    public GridManager gridManager;

    public int x = 0;
    public int y = 0;

    public float moveSpeed = 4f;
    public float rotationSpeed = 180f;

    public enum Direction { North, East, South, West }
    public Direction facing = Direction.North;

    private void Start()
    {
        // posiciona no TILE CERTO dependendo do tamanho real do tile
        transform.position = new Vector3(
            x * gridManager.tileSize,
            0.5f,
            y * gridManager.tileSize
        );
    }

    public IEnumerator MoveForward()
    {
        int targetX = x;
        int targetY = y;

        switch (facing)
        {
            case Direction.North: targetY++; break;
            case Direction.East: targetX++; break;
            case Direction.South: targetY--; break;
            case Direction.West: targetX--; break;
        }

        Tile nextTile = gridManager.GetTile(targetX, targetY);

        if (nextTile == null)
        {
            Debug.Log("Bateu na parede!");
            yield break;
        }

        x = targetX;
        y = targetY;

        Vector3 targetPos = new Vector3(
            x * gridManager.tileSize,
            0.5f,
            y * gridManager.tileSize
        );

        while (Vector3.Distance(transform.position, targetPos) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                targetPos,
                moveSpeed * Time.deltaTime
            );
            yield return null;
        }

        transform.position = targetPos;
    }

    public IEnumerator TurnLeft()
    {
        facing = (Direction)(((int)facing + 3) % 4);
        yield return Rotate(-90);
    }

    public IEnumerator TurnRight()
    {
        facing = (Direction)(((int)facing + 1) % 4);
        yield return Rotate(90);
    }

    IEnumerator Rotate(float angle)
    {
        float rotated = 0;

        while (rotated < Mathf.Abs(angle))
        {
            float step = rotationSpeed * Time.deltaTime;
            transform.Rotate(0, step * Mathf.Sign(angle), 0);
            rotated += step;
            yield return null;
        }
    }
}

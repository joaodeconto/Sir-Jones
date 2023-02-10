using UnityEngine;

public class CreateReferenceSquares : MonoBehaviour
{
    public int gridWidth = 4;
    public int gridHeight = 5;
    public float squareSize = 1f;
    public Color[] squareColors = { Color.red, Color.blue, Color.green, Color.yellow };

    private GameObject[,] gridSquares;

    void Start()
    {
        gridSquares = new GameObject[gridWidth, gridHeight];

        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                GameObject square = GameObject.CreatePrimitive(PrimitiveType.Plane);
                square.transform.position = new Vector3(x * squareSize, 0f, y * squareSize);
                square.GetComponent<Renderer>().material.color = squareColors[(x + y) % squareColors.Length];
                gridSquares[x, y] = square;
            }
        }
    }
}

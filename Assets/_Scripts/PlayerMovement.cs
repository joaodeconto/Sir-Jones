using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public int gridWidth = 4;
    public int gridHeight = 5;
    public float squareSize = 1f;
    public float movementSpeed = 1f;

    private Vector3[] perimeterPositions;
    private int currentPositionIndex = 0;
    private GameObject[,] gridSquares;
    private bool isMoving = false;

    private void Start()
    {
        perimeterPositions = GetPerimeterPositions();
        //transform.position = perimeterPositions[currentPositionIndex];
    }

    private Vector3[] GetPerimeterPositions()
    {
        List<Vector3> perimeterPositions = new List<Vector3>();

        // top row
        for (int x = 0; x < gridWidth; x++)
        {
            perimeterPositions.Add(new Vector3(x * squareSize, 0f, gridHeight * squareSize));
        }

        // right column
        for (int y = gridHeight - 1; y >= 0; y--)
        {
            perimeterPositions.Add(new Vector3(gridWidth * squareSize, 0f, y * squareSize));
        }

        // bottom row
        for (int x = gridWidth - 1; x >= 0; x--)
        {
            perimeterPositions.Add(new Vector3(x * squareSize, 0f, 0f));
        }

        // left column
        for (int y = 0; y < gridHeight; y++)
        {
            perimeterPositions.Add(new Vector3(0f, 0f, y * squareSize));
        }

        return perimeterPositions.ToArray();
    }

    private void Update()
    {
        if (!isMoving && Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                int nextPositionIndex = currentPositionIndex + 1;

                // If next position is not out of bounds
                if (nextPositionIndex < perimeterPositions.Length)
                {
                    StartCoroutine(MoveToNextPosition(nextPositionIndex));
                }
            }
        }
    }

    private IEnumerator MoveToNextPosition(int nextPositionIndex)
    {
        isMoving = true;

        Vector3 currentPosition = transform.position;
        Vector3 nextPosition = perimeterPositions[nextPositionIndex];

        float elapsedTime = 0f;

        while (elapsedTime < movementSpeed)
        {
            elapsedTime += Time.deltaTime;
            transform.position = Vector3.Lerp(currentPosition, nextPosition, elapsedTime / movementSpeed);
            yield return null;
        }

        currentPositionIndex = nextPositionIndex;
        isMoving = false;
    }
}
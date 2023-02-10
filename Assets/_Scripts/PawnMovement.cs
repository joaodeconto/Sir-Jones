using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AI;

public class PawnMovement : MonoBehaviour
{
    public NavMeshAgent agent;

    private Vector2 touchPosition;
    public float distanceTraveled;

    private void OnTouch(InputAction.CallbackContext context)
    {
        Vector2 touchPosition = context.ReadValue<Vector2>();
        Ray ray = Camera.main.ScreenPointToRay(touchPosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            agent.destination = hit.point;
        }
    }

    private void OnEnable()
    {
        GetComponent<PlayerInput>().actions["Touch"].started += OnTouch;
    }

    private void OnDisable()
    {
        GetComponent<PlayerInput>().actions["Touch"].started -= OnTouch;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            touchPosition = Input.mousePosition;
        }

        if (touchPosition != Vector2.zero)
        {
            Ray ray = Camera.main.ScreenPointToRay(touchPosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                agent.destination = hit.point;
            }

            touchPosition = Vector2.zero;
        }
    }
}


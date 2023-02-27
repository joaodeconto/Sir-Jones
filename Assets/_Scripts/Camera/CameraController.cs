using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [SerializeField] private InputActionReference moveAction;
    [SerializeField] private InputActionReference lookAction;
    [SerializeField] private InputActionReference zoomAction;

    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float lookSpeed = 10f;
    [SerializeField] private float zoomSpeed = 10f;

    private void OnEnable()
    {
        moveAction.action.Enable();
        lookAction.action.Enable();
        zoomAction.action.Enable();
    }

    private void OnDisable()
    {
        moveAction.action.Disable();
        lookAction.action.Disable();
        zoomAction.action.Disable();
    }

    private void Update()
    {
        // Handle camera movement
        Vector2 moveInput = moveAction.action.ReadValue<Vector2>();
        Vector3 moveDirection = new Vector3(moveInput.x, 0, moveInput.y);
        transform.position += moveDirection * moveSpeed * Time.deltaTime;

        // Handle camera look
        Vector2 lookInput = lookAction.action.ReadValue<Vector2>();
        Vector3 lookDirection = new Vector3(-lookInput.y, lookInput.x, 0);
        transform.eulerAngles += lookDirection * lookSpeed * Time.deltaTime;

        // Handle camera zoom
        float zoomInput = zoomAction.action.ReadValue<float>();
        transform.position += transform.forward * zoomInput * zoomSpeed * Time.deltaTime;
    }
}
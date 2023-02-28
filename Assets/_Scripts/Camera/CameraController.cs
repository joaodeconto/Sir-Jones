using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.GraphicsBuffer;

namespace BWV
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private InputActionReference moveAction;
        [SerializeField] private InputActionReference lookAction;
        [SerializeField] private InputActionReference zoomAction;

        [SerializeField] private float moveSpeed = 10f;
        [SerializeField] private float lookSpeed = 10f;
        [SerializeField] private float zoomSpeed = 10f;

        private bool isLooking;

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
            MoveCamera();

            if (Mouse.current.rightButton.isPressed)
            {
                LookCamera();
            }
            else if (isLooking)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.identity, Time.deltaTime * 10f);
                isLooking = false;
            }
            // Handle camera zoom
            float zoomInput = zoomAction.action.ReadValue<Vector2>().y;
            transform.position += transform.forward * zoomInput * zoomSpeed * Time.deltaTime;
            
        }
        private void MoveCamera()
        {
            // Handle camera movement
            Vector2 moveInput = moveAction.action.ReadValue<Vector2>();
            Vector3 moveDirection = new Vector3(moveInput.x, 0, moveInput.y);
            float distanceToMove = moveSpeed * Time.deltaTime;
            float sphereRadius = 0.5f;
            RaycastHit hit;
            if (Physics.SphereCast(transform.position, sphereRadius, moveDirection, out hit, distanceToMove))
            {
                // A collider was hit, don't move the camera
            }
            else
            {
                // No collider was hit, move the camera
                transform.position += moveDirection * distanceToMove;
            }
        }

        private void LookCamera()
        {
            Vector2 lookInput = lookAction.action.ReadValue<Vector2>();
            Vector3 lookDirection = new Vector3(-lookInput.y, lookInput.x, 0);
            transform.eulerAngles += lookDirection * lookSpeed * Time.deltaTime;
            isLooking = true;
        }
        
    }
}
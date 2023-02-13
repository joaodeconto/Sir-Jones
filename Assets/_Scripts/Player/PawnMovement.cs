using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

namespace BWV
{
    public class PawnMovement : MonoBehaviour
    {
        public NavMeshAgent agent;
        private Vector2 touchPosition;
        public float distanceTraveled;
        public StructureType targetStrucure;

        private void OnClick()
        {
            touchPosition = Mouse.current.position.ReadValue();
        }

        private void TouchPerformed(InputAction.CallbackContext context)
        {
            touchPosition = context.ReadValue<Vector2>();
        }

        private void Update()
        {
            if (GameState.state != GameState.State.InGame) return;

            if (touchPosition != Vector2.zero)
            {
                Ray ray = Camera.main.ScreenPointToRay(touchPosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                {
                    Debug.Log("Hit " + hit.transform.name);
                    agent.destination = hit.point;
                    if (hit.collider.CompareTag("Structure"))
                    {
                        //TODO check for entranceStructure  and set as current destination
                        StructureType si = hit.transform.GetComponent<StructureInteraction>().dataStructure.structureType;
                        targetStrucure = si;
                        Debug.Log("Hit structure " + si.ToString());
                    }
                    else targetStrucure = StructureType.none;
                }

                touchPosition = Vector2.zero;
            }
        }
    }
}

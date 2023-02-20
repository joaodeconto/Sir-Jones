using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

namespace BWV
{
    public class PawnMovement : MonoBehaviour
    {
        public NavMeshAgent agent;
        private Vector2 touchPosition;
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
            if (!GameState.IsInGame) touchPosition = Vector2.zero;

            if (touchPosition != Vector2.zero)
            {
                Ray ray = Camera.main.ScreenPointToRay(touchPosition);

                if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
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

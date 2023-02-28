using UnityEditorInternal;
using UnityEngine;

namespace BWV
{
    public class FollowPawnCentered : MonoBehaviour
    {
        public Transform pawnTransform;
        public float followSpeed = 10f;
        [Range(0.01f,0.49f)] public float followThreshold = 0.33f;
        public float extraThreshold = 10f;
        [Range(-40, -5)] public float zThreshold = -30f;

        private void OnEnable()
        {
            RulesManager.OnTurnEnd += ClearPawn;
        }
        private void OnDisable()
        {
            RulesManager.OnTurnEnd -= ClearPawn;
        }
        private void Update()
        {
            if (!GameState.IsInGame) return;

            if (pawnTransform != null)
            {
                // Check if pawn is beyond follow threshold
                float screenThreshold = Screen.width * followThreshold;
                float pawnScreenPosX = Camera.main.WorldToScreenPoint(pawnTransform.position).x;
                bool shouldFollowX = pawnScreenPosX < screenThreshold || pawnScreenPosX > Screen.width - screenThreshold;

                if (shouldFollowX)
                {
                    // Calculate direction to move camera and collider
                    float pawnDirectionX = Mathf.Sign(pawnScreenPosX - Screen.width / 2f);
                    Vector3 followDirection = new Vector3(pawnDirectionX, 0f, 0f);

                    // Move camera and collider
                    transform.position += followDirection * followSpeed * Time.deltaTime;
                }

                transform.position = new Vector3(transform.position.x,transform.position.y, pawnTransform.position.z + zThreshold);
            }
            else
            {
                pawnTransform = RulesManager._pawn.transform;
                transform.position = new Vector3(pawnTransform.position.x, transform.position.y, pawnTransform.position.z + zThreshold);
            }
        }

        private void ClearPawn()
        {
            pawnTransform = null; 
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FHE.QuestInteractionPresets.Movement
{
    public class OVRTeleportationMovementController : MonoBehaviour
    {
        [SerializeField] private Transform aimHand;
        [SerializeField] private LayerMask groundLayer;

        private bool _isAiming = true;

        private void Update()
        {
            if (!_isAiming) return;

            DrawAimRay();
        }

        private void DrawAimRay()
        {
            Debug.DrawRay(aimHand.position, -aimHand.right);
        }

    }
}

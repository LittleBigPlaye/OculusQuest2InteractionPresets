using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FHE.QuestInteractionPresets.Movement
{
    public enum ControlType
    {
        NONE,
        CONTINUOUS_MOVEMENT,
        TELEPORTATION
    }

    [RequireComponent(typeof(OVRContinuousMovementController))]
    [RequireComponent(typeof(OVRTeleportationMovementController))]
    public class OVRControllerSelector : MonoBehaviour
    {
        [Tooltip("Defines the movement type that is enabled by default.")]
        [SerializeField] private ControlType defaultControlType;

        public ControlType CurrentControlType { get; private set; }

        private OVRContinuousMovementController _continuousMovementController;
        private OVRTeleportationMovementController _teleportationMovementController;

        private void Awake()
        {
            _continuousMovementController = GetComponent<OVRContinuousMovementController>();
            _teleportationMovementController = GetComponent<OVRTeleportationMovementController>();
            SetControlType(defaultControlType);
        }

        public void SetControlType(ControlType controlType)
        {
            CurrentControlType = controlType;

            switch (controlType)
            {
                case ControlType.NONE:
                    _continuousMovementController.enabled = false;
                    _teleportationMovementController.enabled = false;
                    break;

                case ControlType.CONTINUOUS_MOVEMENT:
                    _continuousMovementController.enabled = true;
                    _teleportationMovementController.enabled = false;
                    break;

                case ControlType.TELEPORTATION:
                    _continuousMovementController.enabled = false;
                    _teleportationMovementController.enabled = true;
                    break;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FHE.QuestInteractionPresets.Movement
{
    [RequireComponent(typeof(CharacterController))]
    public class OVRContinuousMovementController : MonoBehaviour
    {
        [SerializeField] private float movementSpeed;
        [SerializeField] private float rotationSpeed;
        [SerializeField] private float gravity = Physics.gravity.y;

        private CharacterController _characterController;
        private OVRCameraRig _cameraRig;

        private bool _shouldTurnLeft;
        private bool _shouldTurnRight;
        private bool _shouldWalkForward;

        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
            _cameraRig = GetComponent<OVRCameraRig>();
        }

        private void Update()
        {
            UpdateMovement();
            UpdateRotation();
        }

        private void UpdateMovement()
        {
            if (!_shouldWalkForward) return;

            Vector3 movementDirection = Time.deltaTime * movementSpeed * transform.forward;
            if (_characterController.isGrounded)
            {
                movementDirection.y = -0.5f;
            }
            else
            {
                movementDirection.y = gravity;
            }

            _characterController.Move(movementDirection);
        }

        private void UpdateRotation()
        {
            if (!_shouldTurnLeft && !_shouldTurnRight) return;

            float currentRotationSpeed = (_shouldTurnLeft) ? -rotationSpeed : rotationSpeed;

            Debug.Log("Test");

            Vector3 targetRotation = transform.rotation.eulerAngles;
            targetRotation.y += currentRotationSpeed * Time.deltaTime;
            transform.rotation = Quaternion.Euler(targetRotation);
        }

        public void SetLeftTurn(bool shouldTurnLeft)
        {
            _shouldTurnLeft = shouldTurnLeft;
            if (shouldTurnLeft)
            {
                _shouldTurnRight = false;
                _shouldWalkForward = false;
            }
        }

        public void SetRightTurn(bool shouldTurnRight)
        {
            _shouldTurnRight = shouldTurnRight;
            if (shouldTurnRight)
            {
                _shouldTurnLeft = false;
                _shouldWalkForward = false;
            }
        }

        public void SetForwardWalk(bool shouldWalkForward)
        {
            _shouldWalkForward = shouldWalkForward;
            if (shouldWalkForward)
            {
                _shouldTurnLeft = false;
                _shouldTurnRight = false;
            }
        }
    }
}


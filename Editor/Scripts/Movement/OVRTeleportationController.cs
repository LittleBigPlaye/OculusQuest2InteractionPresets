using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace FHE.QuestInteractionPresets.Movement
{
    public enum TeleportationState
    {
        NONE,
        AIM,
        TELEPORT,
    }

    public class OVRTeleportationController : MonoBehaviour
    {
        [SerializeField] private Transform handTransform;
        [SerializeField] private LineRenderer lineRenderer;
        [SerializeField] private LayerMask groundLayerMask;
        [SerializeField] private float lineRendererTolerance = .2f;

        private TeleportationState _currentState = TeleportationState.AIM;
        private NavMeshAgent _agent;
        private Vector3 _currentTargetPosition;

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            switch (_currentState)
            {
                case TeleportationState.AIM:
                    Aim();
                    break;

                case TeleportationState.TELEPORT:
                    _agent.Warp(_currentTargetPosition);
                    _currentState = TeleportationState.NONE;
                    break;
            }
        }

        private void Aim()
        {
            RaycastHit hit;

            SetLineRendererPosition(0, handTransform.position);

            if (Physics.Raycast(handTransform.position, -handTransform.right, out hit, Mathf.Infinity, groundLayerMask))
            {
                SetLineRendererPosition(1, hit.point);
                _currentTargetPosition = hit.point;
            }
            else
            {
                SetLineRendererPosition(1, handTransform.position - handTransform.right * 5);
            }
        }

        private void SetLineRendererPosition(int positionIndex, Vector3 targetPosition)
        {
            Vector3 previousTargetPosition = lineRenderer.GetPosition(positionIndex);
            if (Vector3.Distance(previousTargetPosition, targetPosition) > lineRendererTolerance)
            {
                lineRenderer.SetPosition(positionIndex, targetPosition);
            }
        }

        public void StartTeleportation()
        {
            Debug.Log("Test");
            if (_currentState != TeleportationState.AIM) return;

            _currentState = TeleportationState.TELEPORT;
        }
    }
}
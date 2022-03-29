using System.Collections;
using System.Collections.Generic;
using Oculus.Interaction;
using UnityEngine;

namespace FHE.QuestInteractionPresets.Menu
{
    public class HandMenu : MonoBehaviour
    {
        [SerializeField] private OVRCameraRig cameraRig;
        [SerializeField] private Transform targetHand;
        [SerializeField] private Vector3 offsetFromHand = new Vector3(0.5f, 0.5f, 0.5f);

        [SerializeField] private GameObject menuContent;

        private bool _isVisible;

        private void Awake()
        {

        }

        private void Update()
        {
            if (!_isVisible) return;

            UpdateMenuPosition();
            UpdateMenuRotation();
        }

        private void UpdateMenuRotation()
        {
            Vector3 eyePosition = cameraRig.centerEyeAnchor.transform.position;
            transform.rotation = Quaternion.LookRotation(transform.position - eyePosition, Vector3.up);
        }

        private void UpdateMenuPosition()
        {
            Debug.Log("Test");
            Vector3 handPosition = targetHand.transform.position;
            transform.position = handPosition + offsetFromHand;
        }


        public void ShowMenu()
        {
            _isVisible = true;
            menuContent.SetActive(true);
        }

        public void HideMenu()
        {
            _isVisible = false;
            menuContent.SetActive(false);
        }
    }
}

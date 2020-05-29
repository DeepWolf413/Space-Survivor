using System;
using UnityEngine;

namespace DeepWolf.SpaceSurvivor.UI
{
    public class DangerIndicator : MonoBehaviour
    {
        [SerializeField]
        private Transform target = null;

        [SerializeField]
        private RectTransform indicatorPieceOne = null;

        [SerializeField, Tooltip("This extra piece is used for corner wrapping.")]
        private RectTransform indicatorPieceTwo = null;

        private Vector3 targetPos = Vector3.zero;
        private Vector3 screenBounds = Vector3.zero;
        private Camera cameraComponent = null;

        public Transform Target
        { set => target = value; }

        private void Start()
        {
            cameraComponent = Camera.main;
            screenBounds = cameraComponent.ViewportToWorldPoint(new Vector3(1.0f, 1.0f));
        }

        private void LateUpdate()
        {
            if (!target)
            { return; }
            
            if (!target.gameObject.activeSelf)
            {
                gameObject.SetActive(false);
                return;
            }
            
            targetPos = target.position;
            bool isTargetOutOfScreen = IsTargetOutOfScreen();
            indicatorPieceOne.gameObject.SetActive(isTargetOutOfScreen);
            
            if (isTargetOutOfScreen)
            { UpdatePosition(); }

            //Debug.Log($"OOS: {IsTargetOutOfScreen()} | TargetPos: {targetPos} | ClampedPos: {GetClampedScreenPos()} | ScreenBounds: {screenBounds}");
        }

        private void UpdatePosition()
        {
            indicatorPieceOne.position = GetClampedScreenPos();

            float zRotation = targetPos.x > screenBounds.x || targetPos.x < -screenBounds.x ? 90.0f : 0.0f;
            indicatorPieceOne.rotation = Quaternion.Euler(0.0f, 0.0f, zRotation);
        }

        private Vector3 GetClampedScreenPos()
        {
            Vector3 clampedPos = targetPos;
            clampedPos.x = Mathf.Clamp(clampedPos.x, -screenBounds.x, screenBounds.x);
            clampedPos.y = Mathf.Clamp(clampedPos.y, -screenBounds.y, screenBounds.y);
            clampedPos.z = 0.0f;
            return clampedPos;
        }

        private bool IsTargetOutOfScreen()
        {
            return targetPos.x > screenBounds.x || targetPos.x < -screenBounds.x || targetPos.y > screenBounds.y || targetPos.y < -screenBounds.y;
        }
    }
}
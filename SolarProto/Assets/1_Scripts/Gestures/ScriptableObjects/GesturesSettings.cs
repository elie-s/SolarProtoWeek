using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SolarProto
{
    [CreateAssetMenu(menuName = "CultManager/Gestures/Manager Settings")]
    public class GesturesSettings : ScriptableObject
    {
        public float quickTouchDelay = 0.15f;
        public float doubleTapDelay = 0.20f;
        public float swipeDuration = 0.25f;
        public float movingDetectionThreshold = 0.1f;
        [Header("MultipleTouches settings")]
        public float RotationAngleDetectionThreshold = 5.0f;
        public float pinchDistanceDetectionThreshold = 0.1f;
        public float pinchMinDistance = 0.2f;
        public float pinchMaxDistance = 1.25f;
    }
}
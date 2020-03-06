using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SolarProto
{
    public static class Gesture
    {
        public static bool QuickTouch;
        public static bool LongTouch;
        public static bool DoubleTouch;
        public static bool ReleasedMovementTouch;
        public static Vector2 Movement;
        public static Vector2 DeltaMovement;
        public static float PinchValue;
        public static float PinchDeltaValue;
        public static bool Pinching;
        public static float RotationValue;
        public static bool Rotating;
        public static bool TouchDown => Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began;
        public static bool GettingTouch => Input.touchCount > 0;
    }
}
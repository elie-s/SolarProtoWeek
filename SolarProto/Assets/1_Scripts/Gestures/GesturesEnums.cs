

namespace SolarProto
{
    public enum GestureState
    {
        none,
        Start,
        Doing,
        Canceled,
        Done
    }

    [System.Flags]
    public enum GestureStatus
    {
        none = 0,
        Touch = 1,
        MultipleTouch = 2,

    }
}
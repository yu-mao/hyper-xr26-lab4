using UnityEngine;

public interface IControllerInput
{
    Transform GetTransform();

    bool IsTriggerPressed();
    bool IsButtonPressed(ControllerButtonId buttonId);

    Vector2 Joystick { get; }
}

using UnityEngine;

public interface IInputProvider
{
    bool TryInitialize();

    Transform GetRigTransform();
    Transform GetHeadTransform();

    IControllerInput GetLeftController();
    IControllerInput GetRightController();
}

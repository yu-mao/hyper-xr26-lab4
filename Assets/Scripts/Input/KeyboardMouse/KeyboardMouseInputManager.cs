using UnityEngine;
using UnityEngine.InputSystem;

public class KeyboardMouseInputManager : MonoBehaviour, IInputProvider
{
    [SerializeField]
    private KeyboardMouseControllerInput leftController;
    [SerializeField]
    private KeyboardMouseControllerInput rightController;
    [SerializeField]
    private Transform cameraTransform;
    [SerializeField]
    private Transform rigTransform;
    [SerializeField]
    private float sensitivity;

    private float yaw;
    private float pitch;

    public Transform GetHeadTransform()
    {
        return cameraTransform;
    }

    public IControllerInput GetLeftController()
    {
        return leftController;
    }

    public IControllerInput GetRightController()
    {
        return rightController;
    }

    public Transform GetRigTransform()
    {
        return rigTransform;
    }

    public bool TryInitialize()
    {
        return true;
    }

    private void Update()
    {
        if (Mouse.current.middleButton.isPressed)
        {
            var delta = Mouse.current.delta.ReadValue();
            pitch = pitch - delta.y * sensitivity;
            yaw += delta.x * sensitivity;
            cameraTransform.eulerAngles = new Vector3(pitch, yaw, 0f);

            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }

        var direction = Vector3.zero;
        direction += Keyboard.current.wKey.isPressed ? cameraTransform.forward : Vector3.zero;
        direction -= Keyboard.current.sKey.isPressed ? cameraTransform.forward : Vector3.zero;
        direction += Keyboard.current.dKey.isPressed ? cameraTransform.right : Vector3.zero;
        direction -= Keyboard.current.aKey.isPressed ? cameraTransform.right : Vector3.zero;
        direction.y = 0;
        if (direction.sqrMagnitude > float.Epsilon)
        {
            direction.Normalize();
            cameraTransform.position += direction * Time.deltaTime;
        }
    }
}

using UnityEngine;

public class LaserPointer : MonoBehaviour
{
    [SerializeField]
    private LineRenderer lineRenderer;
    [SerializeField]
    private GameObject dot;

    private IControllerInput controller;
    private Transform controllerTransform;

    private RaycastTargetButton currentButton = null;
    private bool wasTriggerPressedLastFrame = false;

    public void Initialize(IControllerInput controller)
    {
        this.controller = controller;
        controllerTransform = controller.GetTransform();
        dot.SetActive(false);
    }

    private void LateUpdate()
    {
        transform.position = controllerTransform.position;
        transform.rotation = controllerTransform.rotation;

        RaycastTargetButton newButton = null;

        var ray = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(ray, out var hitInfo))
        {
            //Adjust length of the laser line
            Vector3 localHitPoint = Vector3.forward * hitInfo.distance;
            lineRenderer.SetPosition(1, localHitPoint);

            //Update laser dot
            dot.transform.localPosition = localHitPoint;
            dot.SetActive(true);

            newButton = hitInfo.collider.GetComponentInParent<RaycastTargetButton>();
        }
        else
        {
            dot.SetActive(false);
            lineRenderer.SetPosition(1, Vector3.forward * 100f);
        }

        //Update which button is in focus
        if (newButton != currentButton)
        {
            if (currentButton != null)
            {
                currentButton.OnHoverExit();
            }

            currentButton = newButton;

            if (currentButton != null)
            {
                currentButton.OnHoverEnter();
            }
        }

        //Check if the trigger was pressed this frame
        if (controller.IsTriggerPressed() && !wasTriggerPressedLastFrame)
        {
            if(currentButton != null)
            {
                currentButton.OnClicked();
            }
        }

        wasTriggerPressedLastFrame = controller.IsTriggerPressed();
    }
}

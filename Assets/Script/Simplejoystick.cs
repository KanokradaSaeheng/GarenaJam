using UnityEngine;
using UnityEngine.EventSystems;

public class SimpleJoysticks : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public RectTransform joystickBG;
    public RectTransform joystickHandle;
    public float deadZone = 0.2f;

    private Vector2 inputDirection = Vector2.zero;
    private float radius;
    private bool isTouching = false;

    private void Start()
    {
        radius = joystickBG.sizeDelta.x * 0.5f;
        joystickHandle.anchoredPosition = Vector2.zero;
    }

    private void Update()
    {
        // Auto-reset if finger lifted off screen
        if (isTouching && Input.touchCount == 0)
        {
            ResetJoystick();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isTouching = true;
        OnDrag(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        isTouching = true;

        Vector2 pos;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(joystickBG, eventData.position, eventData.pressEventCamera, out pos))
        {
            Vector2 normalized = pos / radius;

            inputDirection = Vector2.ClampMagnitude(normalized, 1f);

            float magnitude = inputDirection.magnitude;
            if (magnitude < deadZone)
            {
                inputDirection = Vector2.zero;
            }
            else
            {
                inputDirection = inputDirection.normalized * ((magnitude - deadZone) / (1f - deadZone));
            }

            joystickHandle.anchoredPosition = inputDirection * radius;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        ResetJoystick();
    }

    private void ResetJoystick()
    {
        isTouching = false;
        inputDirection = Vector2.zero;
        joystickHandle.anchoredPosition = Vector2.zero;
    }

    public float Horizontal => inputDirection.x;
    public float Vertical => inputDirection.y;
    public Vector2 Direction => inputDirection;
}

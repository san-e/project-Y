using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class FancyButtonScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private float startWidth = 1.35f;
    private float endWidth = 195.35f;
    private float height = 44.6f;
    private Color startColor = new Color(1, 1, 1);
    private Color endColor = new Color(0, 0, 0);
    private Color variableStartColor;
    private Color variableEndColor;
    private float variableStartWidth;
    private float variableEndWidth;
    private RectTransform imageRect;
    private TextMeshProUGUI text;
    private float timer;
    private float lerpDuration = 0.25f;
    private bool stopAnimating;

    private void Start()
    {
        timer = 0f;
        text = transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>();
        text.color = startColor;
        imageRect = transform.GetChild(0).gameObject.GetComponent<RectTransform>();
        imageRect.sizeDelta = new Vector2(startWidth, height);
        stopAnimating = true;
    }

    private void Update()
    {
        if (!stopAnimating)
        {
            if (timer < lerpDuration)
            {
                float lerped = Mathf.SmoothStep(variableStartWidth, variableEndWidth, timer / lerpDuration);
                Color lerpedColor = Color.Lerp(variableStartColor, variableEndColor, timer / lerpDuration);
                imageRect.sizeDelta = new Vector2(lerped, height);
                text.color = lerpedColor;
                timer += Time.deltaTime;
            } else
            {
                imageRect.sizeDelta = new Vector2(variableEndWidth, height);
                text.color = variableEndColor;
                stopAnimating = true;
            }
        }
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        timer = 0f;
        stopAnimating = false;
        variableStartWidth = startWidth;
        variableEndWidth = endWidth;
        variableStartColor = startColor;
        variableEndColor = endColor;
    }

    //Detect when Cursor leaves the GameObject
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        timer = 0f;
        if (stopAnimating) 
        { 
            variableStartWidth = endWidth;
            variableStartColor = endColor;
        } else
        {
            variableStartColor = text.color;
            variableStartWidth = imageRect.sizeDelta.x;
        }
        variableEndWidth = startWidth;
        variableEndColor = startColor;
        stopAnimating = false;
    }
}

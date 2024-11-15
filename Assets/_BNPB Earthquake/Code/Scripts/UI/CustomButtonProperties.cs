using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CustomButtonProperties : MonoBehaviour
{
    public enum Type {
        primary,
        secondary
    }

    public Type type;
    public Color primaryColor;
    public Color secondaryColor;
    public Color highlightColor;
    public Color pressedColor;
    public Color selectedColor;
    public Color disableColor;
    public Color enableColorContent;
    public Color disableColorContent;
    [SerializeField]
    private Image background;
    [SerializeField]
    private Image iconButton;
    [SerializeField]
    private TMP_Text textButton;
    private CustomButton customButton;

    private void Awake() {
        customButton = this.GetComponent<CustomButton>();
    }

    private void OnEnable() {
        customButton.SetProperties(this);
    }

    public void SetColor(Color colorBg, Color colorContent) {
        if (background != null) background.color = colorBg;
        if (iconButton != null)
            iconButton.color = colorContent;
        if (textButton != null)
            textButton.color = colorContent;
    }

    public void ToggleIcon(bool _state)
    {
        if (iconButton != null)
            iconButton.gameObject.SetActive(_state);
    }

    public void ResetButton()
    {
        type = Type.primary;
        customButton.SetProperties(this);
    }

    public void ChangeButtonType(Type _type)
    {
        type = _type == Type.primary? Type.primary : Type.secondary;
        customButton.SetProperties(this);
    }
}

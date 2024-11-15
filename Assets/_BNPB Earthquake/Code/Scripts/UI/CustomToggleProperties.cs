using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CustomToggleProperties : MonoBehaviour
{
    public enum Type
    {
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
    public Color defaultFontColor;

    [SerializeField]
    private Image background;
    [SerializeField]
    private GameObject iconButton, iconButton2;
    [SerializeField]
    private TMP_Text textButton;
    private CustomToggle customToggle;

    private void Awake()
    {
        customToggle = this.GetComponent<CustomToggle>();
    }

    private void OnEnable()
    {
        customToggle.SetProperties(this);
    }

    public void SetColor(Color colorBg, Color colorContent)
    {
        if (background != null) background.color = colorBg;
        if (textButton != null)
        {
            textButton.color = colorContent;
            // Reapply font to ensure visibility
            if (textButton.font == null)
            {
                textButton.font = TMP_Settings.defaultFontAsset; // Set a default if font is missing
            }
        }

    }

    public void SetFontStyle(bool bold = false)
    {
        if (textButton == null) return;
        textButton.fontStyle = bold ? FontStyles.Bold : FontStyles.Normal;
    }

    public void ToggleFontStyleOnClick()
    {
        if (textButton == null) return;
        // Toggle font style between Bold and Normal
        textButton.fontStyle = textButton.fontStyle == FontStyles.Bold ? FontStyles.Normal : FontStyles.Bold;
    }

    public void ToggleCheckIcon(bool _state)
    {
        if (iconButton == null) return;
        iconButton.SetActive(_state);
        if (iconButton2 == null) return;
        iconButton2.SetActive(_state);
      
    }

    public void ApplySelectedStyle()
    {
        SetColor(selectedColor, Color.white);
        SetFontStyle(true);
    }

    public void ApplyNormalStyle()
    {
        SetColor(type == Type.primary ? primaryColor : secondaryColor, defaultFontColor);
        SetFontStyle(false);
        ToggleCheckIcon(false);
    }


}

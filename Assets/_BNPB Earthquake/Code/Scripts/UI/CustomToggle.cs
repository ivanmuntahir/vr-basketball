using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(CustomToggleProperties))]
public class CustomToggle : Toggle, IPointerEnterHandler, IPointerDownHandler, IPointerClickHandler, IPointerExitHandler
{
    private Color defaultColor;
    private CustomToggleProperties properties;

    // Initialize the toggle with properties and set the default color
    public void SetProperties(CustomToggleProperties _properties)
    {
        properties = _properties;
        defaultColor = properties.type == CustomToggleProperties.Type.primary
            ? properties.primaryColor
            : properties.secondaryColor;

        UpdateColorAndFont(interactable);
        properties.ApplyNormalStyle();
    }

    // Change color theme based on the given type
    public void ChangeColor(CustomToggleProperties.Type type)
    {
        properties.type = type;
        SetProperties(properties);
    }

    // Set states with their corresponding color and font style
    public void SetPressedState()
    {
        DoStateTransition(SelectionState.Pressed, false);
    }

    public void SetSelectedState()
    {
        Select();
        isOn = true;
        DoStateTransition(SelectionState.Selected, false);
    }

    public void SetNormalState()
    {
        isOn = false;
        DoStateTransition(SelectionState.Normal, true);
    }

    // Centralize transition logic and styling updates
    protected override void DoStateTransition(SelectionState state, bool instant)
    {
        if (properties == null) return;

        switch (state)
        {
            case SelectionState.Normal:
                HandleNormalState();
                break;
            case SelectionState.Highlighted:
                HandleHighlightedState();
                break;
            case SelectionState.Pressed:
                HandlePressedState();
                break;
            case SelectionState.Selected:
                HandleSelectedState();
                break;
            case SelectionState.Disabled:
                HandleDisabledState();
                break;
        }

        properties.ToggleCheckIcon(isOn);
    }

    // Methods for specific state handling
    private void HandleNormalState()
    {
        if (!isOn)
        {
            properties.SetColor(defaultColor, properties.defaultFontColor);
            properties.SetFontStyle(false);
        }
    }

    private void HandleHighlightedState()
    {
        if (!isOn)
        {
            properties.SetColor(properties.highlightColor, properties.defaultFontColor);
        }
    }

    private void HandlePressedState()
    {
        properties.SetColor(properties.pressedColor, properties.defaultFontColor);
        isOn = true;
    }

    private void HandleSelectedState()
    {
        properties.SetColor(properties.selectedColor, Color.white);
        /*properties.SetFontStyle(true);*/
        properties.ApplySelectedStyle();
        isOn = true;
    }

    private void HandleDisabledState()
    {
        properties.SetColor(properties.disableColor, properties.disableColorContent);
        isOn = false;
    }

    // Update color and font based on interactable status
    private void UpdateColorAndFont(bool isInteractable)
    {
        if (isInteractable)
        {
            properties.SetColor(defaultColor, properties.defaultFontColor);
        }
        else
        {
            properties.SetColor(properties.disableColor, properties.disableColorContent);
        }
    }

    // Override pointer events if additional audio or visual cues are required
    public override void OnPointerDown(PointerEventData eventData)
    {
        // Example for audio playback or additional logic on pointer down
        // AudioHandler.Instance.PlayButtonClicked();
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        // Example for audio playback or additional logic on pointer enter
        // AudioHandler.Instance.PlayButtonHighlight();
        base.OnPointerEnter(eventData);
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        properties.ToggleFontStyleOnClick();
        base.OnPointerClick(eventData);
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);
    }
}

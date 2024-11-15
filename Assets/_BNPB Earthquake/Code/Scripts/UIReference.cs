using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class TextReference
{
    public string key;
    public TextMeshProUGUI text;
}

[Serializable]
public class ImageReference
{
    public string key;
    public Image image;
}

[Serializable]
public class ButtonReference
{
    public string key;
    public Button button;
}

[Serializable]
public class ToggleReference
{
    public string key;
    public Toggle toggle;
}

[Serializable]
public class GameObjectReference
{
    public string key;
    public GameObject gameObject;
}

public class UIReference : MonoBehaviour
{
    public List<TextReference> textReferences;
    public List<ImageReference> imageReferences;
    public List<ButtonReference> buttonReferences;
    public List<ToggleReference> toggleReferences;
    public List<GameObjectReference> gameObjectReference;

    public TextMeshProUGUI FindText(string key)
    {
        foreach (var item in textReferences)
        {
            if (item.key == key) 
                return item.text;
        }
        return null;
    }

    public Image FindImage(string key)
    {
        foreach (var item in imageReferences)
        {
            if (item.key == key)
                return item.image;
        }
        return null;
    }

    public Button FindButton(string key)
    {
        foreach (var item in buttonReferences)
        {
            if (item.key == key)
                return item.button;
        }
        return null;
    }

    public Toggle FindToggle(string key)
    {
        foreach (var item in toggleReferences)
        {
            if (item.key == key)
                return item.toggle;
        }
        return null;
    }

    public GameObject FindGameObject(string key)
    {
        foreach (var item in gameObjectReference)
        {
            if (item.key == key)
                return item.gameObject;
        }
        return null;
    }
}

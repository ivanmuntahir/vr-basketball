using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StepUIObject : MonoBehaviour
{
    public GameObject check;
    public GameObject uncheck;
    public TextMeshProUGUI contentText;

    public void SetText(string content)
    {
        contentText.text = content;
    }

    public void SetCondition(bool cond)
    {
        uncheck.SetActive(!cond);
        check.SetActive(cond);
    }
}

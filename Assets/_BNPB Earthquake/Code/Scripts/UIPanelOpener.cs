using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPanelOpener : MonoBehaviour
{
    public GameObject Panel;
    public GameObject wrongUIPanel;
    public GameObject[] apdLapangan;
    public GameObject buttonSelesai;

    public float activeDuration = 1f;

    void Update()
    {
        buttonSelesai.SetActive(AllGameObjectsActive());
    }
    
    bool AllGameObjectsActive()
    {
        foreach (GameObject obj in apdLapangan)
        {
            if (!obj.activeInHierarchy)
                return false;
        }
        return true;
    }

    public void OnButtonClick()
    {
        StartCoroutine(ActiveWrongUI());
    }

    IEnumerator ActiveWrongUI()
    {
        wrongUIPanel.SetActive(true);
        Debug.Log("Wrong UI activated at: " + Time.time);
        yield return new WaitForSeconds(activeDuration);
        wrongUIPanel.SetActive(false);
        Debug.Log("Wrong UI deactived at: " + Time.time);
    }

    public void OpenPanel()
    {
        if (Panel != null)
        {
            bool isActive = Panel.activeSelf;

            Panel.SetActive(!isActive);
        }
    }
}

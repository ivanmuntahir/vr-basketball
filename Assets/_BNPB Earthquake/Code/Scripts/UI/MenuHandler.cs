using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MenuHandler : MonoBehaviour
{
    public List<CustomToggle> toggleCollection = new List<CustomToggle>();
    public List<GameObject> panelCollection = new List<GameObject>();
    private int currentIndex = -1;

    public void OnEnable()
    {
        // Populate toggleCollection if empty
        if (toggleCollection == null || toggleCollection.Count == 0)
            toggleCollection = GetComponentsInChildren<CustomToggle>().ToList();

        // Initialize event listeners for each toggle button
        for (int i = 0; i < toggleCollection.Count; i++)
        {
            int cachedIndex = i;
            toggleCollection[i].onValueChanged.RemoveAllListeners();
            toggleCollection[i].onValueChanged.AddListener(delegate { EnableMenu(cachedIndex); });
        }

        // Enable the first menu by default if desired
        if (toggleCollection.Count > 0)
            EnableMenu(0);
    }

    public void DisableAllMenu()
    {
        // Deactivate all toggles and panels
        foreach (var item in toggleCollection)
        {
            item.SetNormalState();
        }
        foreach (var panel in panelCollection)
        {
            if (panel != null)
                panel.SetActive(false);
        }
    }

    public void EnableMenu(int _index)
    {
        // Only proceed if a new button is clicked
        if (_index == currentIndex) return;

        // Deactivate previous toggle and panel
        DisableAllMenu();

        // Update current index
        currentIndex = _index;

        // Activate the selected toggle and panel only if valid
        if (currentIndex >= 0 && currentIndex < toggleCollection.Count)
        {
            toggleCollection[currentIndex].SetSelectedState();
        }

        if (currentIndex >= 0 && currentIndex < panelCollection.Count && panelCollection[currentIndex] != null)
        {
            panelCollection[currentIndex].SetActive(true);
        }
    }

    public void ResetMenuState()
    {
        // Resets all menu states, useful if switching contexts or resetting UI
        currentIndex = -1;
        DisableAllMenu();
    }
}

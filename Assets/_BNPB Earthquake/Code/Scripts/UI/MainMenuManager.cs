using UnityEngine;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [Header("Login")]
    public TMP_InputField nameInputField;
    public TMP_Dropdown ageDropdown;
    public TMP_Dropdown genderDropdown;
    public TMP_Dropdown locationDropdown;
    public TMP_InputField customLocationInput;   // New input field for custom location
    public TMP_Text selectedLabelDisplay;
    public GameObject panelLogin, panelMainMenu;
    private APIManager apiManager;

    [Header("Menu")]
    public GameObject panelOptionModule;
    public GameObject panelCity;

    private void Start()
    {
        PopulateAgeDropdown();
        genderDropdown.onValueChanged.AddListener(UpdateSelectedLabel);
        UpdateSelectedLabel(genderDropdown.value);

        locationDropdown.onValueChanged.AddListener(delegate { OnLocationDropdownChanged(); });
        customLocationInput.gameObject.SetActive(false); // Hide custom input initially
    }

    private void PopulateAgeDropdown()
    {
        List<string> ageOptions = new List<string>();
        for (int i = 1; i <= 100; i++)
        {
            ageOptions.Add(i.ToString());
        }
        ageDropdown.AddOptions(ageOptions);
    }

    public void OnLoginButtonClick()
    {
        // Get data from input fields and dropdowns
        StaticDataVR.name = nameInputField.text;
        StaticDataVR.age = int.Parse(ageDropdown.options[ageDropdown.value].text);
        StaticDataVR.gender = genderDropdown.options[genderDropdown.value].text;

        // Capture location based on "Lainnya" selection
        if (locationDropdown.options[locationDropdown.value].text == "Lainnya" && !string.IsNullOrEmpty(customLocationInput.text))
        {
            StaticDataVR.location = customLocationInput.text;
        }
        else
        {
            StaticDataVR.location = locationDropdown.options[locationDropdown.value].text;
        }

        // Prepare data as StoreVRData object
        StoreVRData storeData = new StoreVRData(StaticDataVR.name, StaticDataVR.age, StaticDataVR.gender, StaticDataVR.location);

        // Convert StoreVRData object to JSON
        string jsonString = JsonUtility.ToJson(storeData);
        Debug.Log("JSON Data: " + jsonString);

        // Call API
        StartCoroutine(APIManager.instance.PostDataCoroutine("vr-session", jsonString, OnApiResponseReceived));

        // Hide login panel and show main menu panel
        panelLogin.SetActive(false);
        panelMainMenu.SetActive(true);
        Debug.Log("User data has been sent to API and stored in StaticDataVR.");
    }

    private void OnLocationDropdownChanged()
    {
        if (locationDropdown.options[locationDropdown.value].text == "Lainnya")
        {
            customLocationInput.gameObject.SetActive(true);
            customLocationInput.text = "";
        }
        else
        {
            customLocationInput.gameObject.SetActive(false);
            customLocationInput.text = "";
        }
    }

    private void OnApiResponseReceived(string response)
    {
        Debug.Log("API Response: " + response);
    }

    private void UpdateSelectedLabel(int index)
    {
        string selectedLabel = genderDropdown.options[index].text;
        selectedLabelDisplay.text = selectedLabel;
    }

    public void ToggleOptionCity()
    {
        panelOptionModule.SetActive(false);
        panelCity.SetActive(true);
        Debug.Log("panel city is activated");
    }

    public void ToggleBackModule()
    {
        panelOptionModule.SetActive(true);
        panelCity.SetActive(false);
        Debug.Log("panel module is activated");
    }

    public void StartScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}

[Serializable]
public class StoreVRData
{
    public string name;
    public int age;
    public string gender;
    public string location;

    public StoreVRData(string name, int age, string gender, string location)
    {
        this.name = name;
        this.age = age;
        this.gender = gender;
        this.location = location;
    }
}

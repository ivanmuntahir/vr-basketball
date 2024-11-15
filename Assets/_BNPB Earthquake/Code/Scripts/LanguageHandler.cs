using System;
using UnityEngine;
using TMPro; // Untuk TextMeshPro
/*using Assets.SimpleLocalization.Scripts;*/

public class LanguageHandler : MonoBehaviour
{
    public TMP_Dropdown languageDropdown; // Referensi ke TMP_Dropdown
    public string language;

   /* public void Awake()
    {
        // Membaca data lokal
        LocalizationManager.Read();

        // Mendeteksi bahasa sistem
        switch (Application.systemLanguage)
        {
            case SystemLanguage.Indonesian:
                LocalizationManager.Language = "id-ID"; 
                break;
            default:
                LocalizationManager.Language = "en-US";
                break;
        }

        language = PlayerPrefs.GetString("Language");
        if (string.IsNullOrEmpty(language))
        {
            PlayerPrefs.SetString("Language", "id-ID");
            language = "id-ID"; // Pastikan ini juga sesuai dengan file localization
        }
        SetLocalization(language);
        SetDropdownValue(language);

        languageDropdown.onValueChanged.AddListener(OnDropdownValueChanged);
    }*/

  /*  private void OnDropdownValueChanged(int value)
    {
        string selectedLanguage = languageDropdown.options[value].text;
        SetLocalization(selectedLanguage);
        PlayerPrefs.SetString("Language", selectedLanguage);
    }*/

    /// <summary>
    /// Change localization at runtime.
    /// </summary>
   /* public void SetLocalization(string localization)
    {
        // Pastikan localization sesuai dengan yang ada di file resource
        LocalizationManager.Language = localization;
    }*/

    // Set TMP dropdown value sesuai bahasa yang tersimpan
    private void SetDropdownValue(string savedLanguage)
    {
        for (int i = 0; i < languageDropdown.options.Count; i++)
        {
            if (languageDropdown.options[i].text == savedLanguage)
            {
                languageDropdown.value = i;
                break;
            }
        }
    }
}

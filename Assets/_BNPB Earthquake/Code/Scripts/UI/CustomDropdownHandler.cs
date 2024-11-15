using UnityEngine;
using TMPro;

namespace BNG
{
    public class CustomDropdownHandler : MonoBehaviour
    {
        public TMP_Dropdown dropdown;
        public VRTextInput vrTextInput;
        public GameObject vrKeyboardObject, inputObject;
        public TMP_InputField locationInput;

        void Start()
        {
            // Ensure VR Keyboard is initially inactive
            vrKeyboardObject.SetActive(false);

            // Add listener to handle dropdown value changes
            dropdown.onValueChanged.AddListener(delegate { OnDropdownValueChanged(); });
        }

        void OnDropdownValueChanged()
        {
            // Check if the selected option is "Lainnya"
            if (dropdown.options[dropdown.value].text == "Lainnya")
            {
                inputObject.SetActive(true);
                vrKeyboardObject.SetActive(true);
                vrTextInput.OnInputSelect();
            }
            else
            {
                inputObject.SetActive(true);
                vrKeyboardObject.SetActive(false);
                vrTextInput.OnInputDeselect();
            }
        }

    }
}

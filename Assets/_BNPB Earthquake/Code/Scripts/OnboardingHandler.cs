using System;
using System.Collections;
using System.Collections.Generic;
/*using Assets.SimpleLocalization.Scripts;*/
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using SimpleJSON;
using TMPro;

[Serializable]
public class ScenarioDatum
{
    [HideInInspector] public int id;
    public string name_ID;
    public string name_EN;
    public string description_ID;
    public string description_EN;
    public string thumbnail;
    public int status;
    [HideInInspector] public DateTime created_at;
    [HideInInspector] public DateTime updated_at;
    public GameObject selection_obj;
    public GameObject description_obj;
}

[Serializable]
public class Scenario
{
    public bool success;
    public List<ScenarioDatum> data;
}

public class OnboardingHandler : MonoBehaviour
{
    [Header("Data Collection")]
    public Scenario scenario;

    [Header("Sign In System")]
    public TMP_InputField nameInputField;
    public TMP_InputField passInputField;
    public TextMeshProUGUI errorLog;
    public Button signInButton, guestButton;
    public GameObject loginPanel, forgotPassPanel;
    public UnityEvent whenSignedIn;

    [Header("Main Menu System")]
    public GameObject scenarioSelectionPrefab, scenarioDescriptionPrefab;
    public Transform scenarioSelectionParent, scenarioDescriptionParent;

    private void SetSignInState(bool state) => SetInteractable(state, nameInputField, passInputField, signInButton, guestButton);

    private void SetInteractable(bool state, params Selectable[] elements)
    {
        foreach (var element in elements) element.interactable = state;
    }

    public void SetPassState()
    {
        passInputField.contentType = passInputField.contentType == TMP_InputField.ContentType.Password
            ? TMP_InputField.ContentType.Standard
            : TMP_InputField.ContentType.Password;

        passInputField.ForceLabelUpdate();
    }

    /*public void SignIn(bool isGuest)
    {
        if (isGuest) GetAllScenarios();
        else
        {
            SetSignInState(false);
            string jsonString = $"{{\"username\":\"{nameInputField.text}\",\"password\":\"{passInputField.text}\"}}";
            StartCoroutine(APIManager.instance.PostDataCoroutine("api/login", jsonString, HandleLoginResponse));
        }
    }*/

    private void HandleLoginResponse(string res)
    {
        SetSignInState(true);
        var jsonNode = JSON.Parse(res);
        if (jsonNode["success"].AsBool)
        {
            StaticData.username = jsonNode["user"]["username"];
            StaticData.fullname = jsonNode["user"]["fullname"];
            StaticData.email = jsonNode["user"]["email"];
            StaticData.role_id = jsonNode["user"]["role_id"];
            StaticData.token = jsonNode["token"];
            /*GetAllScenarios();*/
        }
        else
        {
            errorLog.text = jsonNode["message"];
        }
    }

   /* public void ResetPassword()
    {
        SetSignInState(false);
        loginPanel.SetActive(false);

        string jsonString = $"{{\"username\":\"{nameInputField.text}\"}}";
        StartCoroutine(APIManager.instance.PostDataCoroutine("api/request-reset-password", jsonString, res =>
        {
            forgotPassPanel.SetActive(true);
            SetSignInState(true);
        }));
    }*/

   /* public void GetAllScenarios()
    {
        StartCoroutine(APIManager.instance.GetDataCoroutine("api/scenario", res =>
        {
            scenario = JsonUtility.FromJson<Scenario>(res);
            bool isId = LocalizationManager.Language == "id-ID";

            foreach (var scenarioData in scenario.data)
            {
                InitializeScenarioUI(scenarioData, isId);
            }

            // Select first scenario by default
            scenario.data[0].selection_obj.GetComponent<UIReference>().FindButton("Select").onClick.Invoke();
            whenSignedIn.Invoke();
        }));
    }*/

    private void InitializeScenarioUI(ScenarioDatum scenarioData, bool isId)
    {
        string select = isId ? scenarioData.name_ID : scenarioData.name_EN;
        string desc = isId ? scenarioData.description_ID : scenarioData.description_EN;

        scenarioData.selection_obj = Instantiate(scenarioSelectionPrefab, scenarioSelectionParent);
        scenarioData.description_obj = Instantiate(scenarioDescriptionPrefab, scenarioDescriptionParent);
        ActivateObjects(scenarioData.selection_obj, scenarioData.description_obj);

        var selectUI = scenarioData.selection_obj.GetComponent<UIReference>();
        var descUI = scenarioData.description_obj.GetComponent<UIReference>();

        SetText(selectUI, "Label", select);
        SetText(descUI, "Title", select);
        SetText(descUI, "Content", desc);

        StartCoroutine(APIManager.instance.DownloadImageCoroutine(scenarioData.thumbnail, res =>
        {
            descUI.FindImage("Thumbnail").sprite = res;
        }));

        AddSelectionListener(selectUI, scenarioData, selectUI);
        AddPlayButtonListener(descUI, scenarioData);
    }

    private void ActivateObjects(params GameObject[] objects)
    {
        foreach (var obj in objects) obj.SetActive(true);
    }

    private void SetText(UIReference uiRef, string key, string value)
    {
        uiRef.FindText(key).text = value;
    }

    private void AddSelectionListener(UIReference selectUI, ScenarioDatum scenarioData, UIReference selectUIRef)
    {
        selectUI.FindButton("Select").onClick.AddListener(() =>
        {
            scenarioData.description_obj.SetActive(true);
            selectUIRef.FindGameObject("Clicked").SetActive(true);

            foreach (var item in scenario.data)
            {
                if (item != scenarioData)
                {
                    item.description_obj.SetActive(false);
                    item.selection_obj.GetComponent<UIReference>().FindGameObject("Clicked").SetActive(false);
                }
            }
        });
    }

    private void AddPlayButtonListener(UIReference descUI, ScenarioDatum scenarioData)
    {
        descUI.FindButton("Play").onClick.AddListener(() =>
        {
            ScenarioSetter.instance.SetScenarioId(scenarioData.id.ToString());
            ScenarioSetter.instance.SetScenarioKey(scenarioData.name_ID.Replace(" ", ""));
            ScenarioSetter.instance.ChangeScene("01-All Scenario");
        });
    }
}

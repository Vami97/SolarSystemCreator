using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BodyModification : MonoBehaviour
{
    [Header("Panels")]
    public GameObject mainEditPanel;
    public GameObject[] panels;

    [Header("All Bodies")]
    public InputField bodyName;
    public Slider bodySpeed;

    [Header("Default Settings")]
    public Slider bodySize;
    public FlexibleColorPicker bodyColor;
    public Image colorIMG;
    public Slider bodyPosition;

    [Header("Sun Settings")]
    public Dropdown sunClass;

    [Header("Planet Settings")]
    public Dropdown planetTemp;
    public Dropdown planetComp;
    public Dropdown planetSize;
    public Toggle planetRings;
    public Slider planetPosition;

    [Header("Moon Settings")]
    public Dropdown moonPlanet;
    public Dropdown moonType;
    public Slider moonPosition;

    [Header("AsteroidBelt Settings")]

    private Body body;

    //RESET VARIABLES
    string rbName;
    float rbSpeed;

    float rbSize;
    Color rbColor;
    float rbPosition;

    int rsClass;

    int rpTemp;
    int rpComp;
    int rpSize;
    bool rpRings;
    float rpPosition;

    int rmPlanet;
    int rmType;
    float rmPosition;

    public List<Body> planets;

    private bool planetSet = false;

    private void Start()
    {
        planets = new List<Body>();

        bodyName.onValueChanged.AddListener(UpdateNameInputValue);
        bodySpeed.onValueChanged.AddListener(UpdateDefaultSliderValue);

        bodySize.onValueChanged.AddListener(UpdateDefaultSliderValue);
        bodyColor.onColorChange.AddListener(UpdateDefaultColorValue);
        bodyPosition.onValueChanged.AddListener(UpdateDefaultSliderValue);

        sunClass.onValueChanged.AddListener(UpdateDefaultDropdownValue);

        planetTemp.onValueChanged.AddListener(UpdateDefaultDropdownValue);
        planetComp.onValueChanged.AddListener(UpdateDefaultDropdownValue);
        planetSize.onValueChanged.AddListener(UpdateDefaultDropdownValue);
        planetRings.onValueChanged.AddListener(UpdateDefaultToggleValue);
        planetPosition.onValueChanged.AddListener(UpdateDefaultSliderValue);

        moonPlanet.onValueChanged.AddListener(UpdateDefaultDropdownValue);
        moonType.onValueChanged.AddListener(UpdateDefaultDropdownValue);
        moonPosition.onValueChanged.AddListener(UpdateDefaultSliderValue);
    }

    void UpdateNameInputValue(string value)
    {
        if(planetSet) UpdateBody();
    }

    void UpdateDefaultDropdownValue(int value)
    {
        if (planetSet) UpdateBody();
    }

    void UpdateDefaultSliderValue(float value)
    {
        if (planetSet) UpdateBody();
    }

    void UpdateDefaultToggleValue(bool value)
    {
        if (planetSet) UpdateBody();
    }

    void UpdateDefaultColorValue(Color color)
    {
        if (planetSet) UpdateBody();

        colorIMG.color = color;
    }

    //Toggle the color picker panel
    public void ToggleColorPicker()
    {
        GameObject panel = bodyColor.transform.parent.gameObject;

        panel.SetActive(!panel.activeSelf);

        colorIMG.color = bodyColor.color;
    }


    public void SetBodyToEdit(Body _body)
    {
        body = _body;

        if(body!=null)
        {
            mainEditPanel.SetActive(true);
            SetPanelOptions();

            ClosePanels();
            panels[body.bType].SetActive(true);

            UpdateBody();
        }       
    }

    void ClosePanels()
    {
        foreach (GameObject panel in panels)
        {
            panel.SetActive(false);
        }
    }

    void SetPanelOptions()
    {
        if(body!=null)
        {
            bodyName.text = body.bName;
            rbName = body.bName;

            bodySpeed.value = body.bSpeed;
            rbSpeed = body.bSpeed;


            bodySize.value = body.bSize;
            rbSize = body.bSize;

            bodyColor.color = body.bColor;
            rbColor = body.bColor;

            bodyPosition.value = body.bLocation;
            rbPosition = body.bLocation;


            sunClass.value = body.sType;
            rsClass = body.sType;

            planetTemp.value = body.pTemp;
            rpTemp = body.pTemp;

            planetComp.value = body.pComposition;
            rpComp = body.pComposition;

            planetSize.value = body.pSize;
            rpSize = body.pSize;

            planetRings.isOn = body.pHasRings;
            rpRings = body.pHasRings;

            planetPosition.value = body.pLocation;
            rpPosition = body.pLocation;


            moonPlanet.value = int.Parse(body.mPlanetID);
            rmPlanet = int.Parse(body.mPlanetID);

            moonType.value = body.mType;
            rmType = body.mType;

            moonPosition.value = body.mLocation;
            rmPosition = body.mLocation;

            planetSet = true;
        }       
    }

    void UpdateBody()
    {
        //Temporary variable in case the body is a moon, to determine its position
        Body temp = null;
        int tempID = 0;

        if (planets != null && planets.Count > 0)
        {
            temp = planets[moonPlanet.value];
            tempID = int.Parse(planets[moonPlanet.value].name);
        }

        body.SetParameters(bodyName.text, body.bType, bodySize.value, bodySpeed.value, bodyColor.color, bodyPosition.value, sunClass.value, planetTemp.value, planetComp.value, planetSize.value, planetRings, planetPosition.value, temp, tempID.ToString(), moonType.value, moonPosition.value);
        body.SetUpVisuals();
    }

    public void SetEdit()
    {
        rbName = body.bName;
        rbSpeed = body.bSpeed;

        rbSize = body.bSize;
        rbColor = body.bColor;
        rbPosition = body.bLocation;

        rsClass = body.sType;

        rpTemp = body.pTemp;
        rpComp = body.pComposition;
        rpSize = body.pSize;
        rpRings = body.pHasRings;
        rpPosition = body.pLocation;

        rmPlanet = int.Parse(body.mPlanetID);
        rmType = body.mType;
        rmPosition = body.mLocation;

        ClickManager.CallCloseAllPanels();
    }

    public void CompleteEdit()
    {
        //Temporary variable in case the body is a moon, to determine its position
        Body temp = null;
        int tempID = 0;

        if (planets != null && planets.Count > 0)
        {
            temp = planets[rmPlanet];
            tempID = int.Parse(planets[rmPlanet].name);
        }

        body.SetParameters(rbName, body.bType, rbSize, rbSpeed, rbColor, rbPosition, rsClass, rpTemp, rpComp, rpSize, rpRings, rpPosition, temp, tempID.ToString(), rmType, rmPosition);
        body.SetUpVisuals();

        planetSet = false;
        SetBodyToEdit(null);
    }
}

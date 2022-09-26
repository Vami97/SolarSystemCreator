using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BodyCreation : MonoBehaviour
{
    [Header("Panels")]
    public GameObject[] panels;

    [Header("All Bodies")]
    public InputField bodyName;
    public Dropdown bodyType;
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


    [Header("Prefabs")]
    public GameObject bodyPrefab;
    private Body newBody;

    private List<Body> planets;

    private int id = 0;

    private bool orbiting = false;

    public List<Body> Planets { get => planets; }

    private void Start()
    {
        planets = new List<Body>();

        bodyName.onValueChanged.AddListener(UpdateNameInputValue);
        bodyType.onValueChanged.AddListener(UpdateBodyTypeDropdownValue);
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

    private void OnEnable()
    {
        ResetForm();
        newBody = (Instantiate(bodyPrefab) as GameObject).GetComponent<Body>();

        //Temporary variable in case the body is a moon, to determine its position
        Body temp = null; 
        int tempID = 0;

        if (planets != null && planets.Count > 0)
        {
            temp = planets[moonPlanet.value];
            tempID = int.Parse(planets[moonPlanet.value].name);
        }

        newBody.SetParameters(bodyName.text, bodyType.value, bodySize.value, bodySpeed.value, bodyColor.color, bodyPosition.value, sunClass.value, planetTemp.value, planetComp.value, planetSize.value, planetRings, planetPosition.value, temp, tempID.ToString(), moonType.value, moonPosition.value);
        newBody.SetUpVisuals();
    }

    private void OnDisable()
    {
        Destroy(newBody.gameObject);
        newBody = null;
    }

    public void CreateBody()
    {
        //The actual body that will exist
        Body realBody;
        realBody = (Instantiate(bodyPrefab) as GameObject).GetComponent<Body>();
        realBody.name = id.ToString();
        id++;

        //Temporary variable in case the body is a moon, to determine its position
        Body temp = null; 
        int tempID = 0;

        if (planets != null && planets.Count > 0){ 
            temp = planets[moonPlanet.value];
            tempID = int.Parse(planets[moonPlanet.value].name);
        }

        //Set the parameters for the body based on input
        if (bodyName.text != "")
            realBody.SetParameters(bodyName.text, bodyType.value, bodySize.value, bodySpeed.value, bodyColor.color, bodyPosition.value, sunClass.value, planetTemp.value, planetComp.value, planetSize.value, planetRings, planetPosition.value, temp, tempID.ToString(), moonType.value, moonPosition.value);
        else
            realBody.SetParameters(bodyType.value, bodySize.value, bodySpeed.value, bodyColor.color, bodyPosition.value, sunClass.value, planetTemp.value, planetComp.value, planetSize.value, planetRings, planetPosition.value, temp, tempID.ToString(), moonType.value, moonPosition.value);

        //Set up the body's visuals so you can see what it looks like
        realBody.SetUpVisuals();

        //Add planets to list of planets
        if(realBody.bType == 2 || realBody.bType == 0) //if Planet
        {
            if(planets.Count == 0)
            {
                Dropdown.OptionData moonOption = new Dropdown.OptionData();
                moonOption.text = "Moon/Satellite";

                bodyType.options.Add(moonOption);
            }

            planets.Add(realBody);
            GameObject.FindObjectOfType<BodyModification>().planets.Add(realBody);

            //Add planets to moon dropdown
            moonPlanet.options.Add(new Dropdown.OptionData(realBody.bName));
        }

        ClickManager.CallCloseAllPanels();
    }

    //NEED TO UPDATE THIS WITH ALL INPUTS
    public void ResetForm()
    {
        bodyName.text = "";
        bodyType.value = 0;
        bodySize.value = 0;
        bodyPosition.value = 0;
    }

    
    void UpdateNameInputValue(string value)
    {
        UpdateBody();
    }

    void UpdateBodyTypeDropdownValue(int value)
    {
        ClosePanels();
        panels[value].SetActive(true);

        UpdateBody();
    }

    void ClosePanels()
    {
        foreach(GameObject panel in panels)
        {
            panel.SetActive(false);
        }
    }

    void UpdateDefaultDropdownValue(int value)
    {
        UpdateBody();
    }

    void UpdateDefaultSliderValue(float value)
    {
        UpdateBody();
    }

    void UpdateDefaultToggleValue(bool value)
    {
        UpdateBody();
    }

    void UpdateDefaultColorValue(Color color)
    {
        UpdateBody();

        colorIMG.color = color;
    }

    //Toggle the color picker panel
    public void ToggleColorPicker()
    {
        GameObject panel = bodyColor.transform.parent.gameObject;

        panel.SetActive(!panel.activeSelf);

        colorIMG.color = bodyColor.color;
    }


    //Toggle Rotate
    public void ToggleRotate()
    {
        orbiting = !orbiting;

        Body[] bodies = FindObjectsOfType<Body>();

        foreach (Body body in bodies)
        {
            if (orbiting) { body.StartRotate(); }
            else { body.PauseRotate(); }
        }           
    }

    public void ResetPositions()
    {
        if(orbiting)
        {
            ToggleRotate();
        }

        Body[] bodies = FindObjectsOfType<Body>();

        foreach(Body body in bodies)
        {
            body.SetUpVisuals();
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

        newBody?.SetParameters(bodyName.text, bodyType.value, bodySize.value, bodySpeed.value, bodyColor.color, bodyPosition.value, sunClass.value, planetTemp.value, planetComp.value, planetSize.value, planetRings, planetPosition.value, temp, tempID.ToString(), moonType.value, moonPosition.value);
        newBody?.SetUpVisuals();
    }
}

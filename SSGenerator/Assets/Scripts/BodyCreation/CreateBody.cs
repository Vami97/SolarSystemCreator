using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CreateBody : MonoBehaviour
{
    [Header("Panels")]
    public GameObject[] panels;

    [Header("All Bodies")]
    public InputField bodyName;
    public Slider bodySpeed;
    public Dropdown bodyType;

    [Header("Custom Settings")]
    

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
    public Sprite circle;
    public Sprite ringCircle;

    //Trackers
    private SpaceBody newBody;
    private List<Body> planets;
    private int id = 0;

    private void Start()
    {
        planets = new List<Body>();

        bodyName.onValueChanged.AddListener(UpdateNameInputValue);
        bodyType.onValueChanged.AddListener(UpdateBodyTypeDropdownValue);

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
        newBody = (Instantiate(bodyPrefab) as GameObject).GetComponent<SpaceBody>();
        newBody.transform.position = new Vector3(Camera.main.transform.position.x, 0f, 0f);
        
        

        
        
    }

    private void OnDisable()
    {
        Destroy(newBody.gameObject);
        newBody = null;
    }

    public void BodyCreate()
    {
        //The actual body that will exist
        Body realBody;
        realBody = (Instantiate(bodyPrefab) as GameObject).GetComponent<Body>();
        realBody.name = id.ToString();
        id++;

        
        //Set up the body's visuals so you can see what it looks like
        realBody.SetUpVisuals();

        //Add planets to list of planets
        if (realBody.bType == 2) //if Planet
        {
            planets.Add(realBody);

            //Add planets to moon dropdown
            moonPlanet.options.Add(new Dropdown.OptionData(realBody.bName));
        }

        ClickManager.CallCloseAllPanels();
    }

    //NEED TO UPDATE THIS WILL ALL INPUTS
    public void ResetForm()
    {
        bodyName.text = "";
        bodyType.value = 0;
        bodySpeed.value = 1;
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
        foreach (GameObject panel in panels)
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

    void UpdateBody()
    {
        
    }
}

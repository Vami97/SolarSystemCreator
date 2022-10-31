using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BodyCreation : MonoBehaviour
{
    //START PANELS
    //These are the panels for each type of Body that can be created 
    //This needs to be edited in the inspector
    [Header("Panels")]
    public GameObject[] panels;
    //END PANELS

    //START SETTINGS
    //These are inputs for the body settings
    //These need to be added in the inspector
    //Each heading specifies what body type they are for
    [Header("All Bodies")] 
    public InputField bodyName; //Name of the Body
    public Dropdown bodyType; //Type of body [Custom/Default | Sun | Planet | Moon]
    public Slider bodySpeed; //Speed of body as it rotates around its anchor [Planet around sun | Moon/Satellite around Planet]

    [Header("Custom Planet Settings")]
    public Slider bodySize; //Size of the Custom Planet
    public FlexibleColorPicker bodyColor; //Color of Custom Planet 
    public Image colorIMG; //Visual for color
    public Slider bodyPosition; //Position of Custom Planet relative to the center of the system

    [Header("Sun Settings")]
    public Dropdown sunClass; //Type of Sun, picked from main sequence stars

    [Header("Planet Settings")]
    public Dropdown planetTemp; //General Temperature of Planet
    public Dropdown planetComp; //General Composition of Planet
    public Dropdown planetSize; //General Size of Planet
    public Toggle planetRings; // +++++ YET TO BE IMPLEMENTED
    public Slider planetPosition; //Position of Planet relative to center of the system

    [Header("Moon/Satellite Settings")]
    public Dropdown moonPlanet; //Which Planet/Custom Planet does the Moon/Satellite belong to
    public Dropdown moonType; //Type of Moon/Satellite
    public Slider moonPosition; //Position of Moon/Satellite relative to its Planet/Custom Planet

    [Header("AsteroidBelt Settings")]
    // +++++ ASTEROID BELT NOT YET IMPLEMENTED
    //END SETTINGS

    //START PREFABS
    [Header("Prefabs")]
    public GameObject bodyPrefab; //Body prefab to create bodies from
    private Body newBody; //Object to store the options for the body which are later copied onto the actual body

    //ID for the bodies that are created
    private int id = 0;
    //Bool to determine if planets are orbiting or not
    private bool orbiting = false;


    private void Start()
    {
        //SET LISTENERS FOR INPUTS
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
        //END SET LISTENERS FOR INPUTS
    }

    //OnEnable happens when the user presses the + button to start creating a new body
    private void OnEnable()
    {
        ResetForm(true); //Reset values in the input
        newBody = (Instantiate(bodyPrefab) as GameObject).GetComponent<Body>(); //Create temporary body gameobject

        //Temporary variable in case the body is a moon, to determine its position
        Body temp = null; //Planet that the moon will go to
        int tempID = 0; //ID of the planet

        if (BodyTypeReturn.planets != null && BodyTypeReturn.planets.Count > 0)
        {
            SetPlanetList();
            temp = BodyTypeReturn.planets[PlanetSearch(moonPlanet.value)]; // search planet from planets, get Body
            tempID = int.Parse(BodyTypeReturn.planets[PlanetSearch(moonPlanet.value)].name); // search planet from planets, get id
        }

        if(BodyTypeReturn.planets.Count <= 0)
        { 
            if(bodyType.options.Count > 4)
            {
                bodyType.options.RemoveAt(4);
            }
        }

        //Get Values From Fields +++++ DO FOR ALL THAT REQUIRE IDS
        int bType = BodyTypeReturn.BodyType(bodyType.options[bodyType.value].text);


        newBody.SetParameters(bodyName.text, bType, bodySize.value, bodySpeed.value, bodyColor.color, bodyPosition.value, sunClass.value, planetTemp.value, planetComp.value, planetSize.value, planetRings, planetPosition.value, temp, tempID.ToString(), moonType.value, moonPosition.value);
        newBody.SetUpVisuals();
    }


    public void SetPlanetList()
    {
        if(BodyTypeReturn.planets != null && BodyTypeReturn.planets.Count > 0)
        {
            moonPlanet.ClearOptions();
            foreach(Body planet in BodyTypeReturn.planets)
            {
                //Add planets to moon dropdown
                string moonPlanetID = planet.bName + "#" + (planet.gameObject.name); //Create string to have planet name + #id
                moonPlanet.options.Add(new Dropdown.OptionData(moonPlanetID)); //Add option to dropdown for body creation panel
            }
        }      
    }

    //OnDisable happens when the user either clicks off the UI to cancel the creations or Submits the creation
    private void OnDisable()
    {
        //Destroy temporary object and remove the reference to it
        Destroy(newBody.gameObject);
        newBody = null;
    }

    //Get ID of Planet from the id string in the options
    public int PlanetSearch(int val)
    {
        //Get text from moonplanet option
        string idstring = moonPlanet.options[val].text;
        string cutidstring = "";  //String that will have the id
        
        //Remove everything from string before the last # so that all is left is the id num and then put that into the id
        //Loop backward through string        
        for (int i = idstring.Length-1; i >= 0; i--)
        {
            if(idstring[i] != '#') { cutidstring += idstring[i]; } //If the # hasnt been reached, add numbers to the id
            else { i = 0; } //Else terminate the forloop
        }
        //Reverse string
        char[] temp = cutidstring.ToCharArray();
        Array.Reverse(temp);
        cutidstring = new string(temp);

        //Go through planets to find matching id
        for (int i = 0; i < BodyTypeReturn.planets.Count; i++)
        {      
            if (cutidstring == BodyTypeReturn.planets[i].gameObject.name) return i; //Return planet num if the name id matches
        }

        return 0; //If none then RIP
    }

    //CreateBody happens when the user submits the body to be created
    public void CreateBody()
    {
        //The actual body that will exist
        Body realBody; //Reference
        realBody = (Instantiate(bodyPrefab) as GameObject).GetComponent<Body>(); //Instantiate

        realBody.name = id.ToString(); //Change object name to ID
        id++; //Increase ID for next object

        //Temporary variables in case the body is a moon, to determine its position
        Body temp = null; //Planet that the moon will go to
        int tempID = 0; //ID of the planet

        //Check if there are planets
        if (BodyTypeReturn.planets != null && BodyTypeReturn.planets.Count > 0){

            temp = BodyTypeReturn.planets[PlanetSearch(moonPlanet.value)]; // search planet from planets, get Body
            tempID = int.Parse(BodyTypeReturn.planets[PlanetSearch(moonPlanet.value)].name); // search planet from planets, get id
        }

        //Check if the body is a moon
        if (realBody.bType == 4) //if Moon
        {
            realBody.SetPlanet(); //Change parent of mooon to current planet
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
            //If this is the first planet
            if(BodyTypeReturn.planets.Count == 0)
            {
                Dropdown.OptionData moonOption = new Dropdown.OptionData(); //Create dropdown option
                moonOption.text = "Moon/Satellite"; //Add name to dropdown option

                bodyType.options.Add(moonOption); //Add dropdown option to dropdown
            }

            BodyTypeReturn.planets.Add(realBody); //Add planet to planets list

            //Add planets to moon dropdown
            string moonPlanetID = realBody.bName + "#" + (id-1); //Create string to have planet name + #id
            moonPlanet.options.Add(new Dropdown.OptionData(moonPlanetID)); //Add option to dropdown for body creation panel
            FindObjectOfType<BodyModification>().moonPlanet.options.Add(new Dropdown.OptionData(moonPlanetID)); //Add option to dropdown for body modification panel
        }

        //Reset the creation panel form
        ResetForm(true);
        //Close the Creation panel
        ClickManager.CallCloseAllPanels();
    }

    //ResetForm happens when Create panel is opened (true), Create is completed (true), and bodyType Value is changed (false)
    //ResetForm sets all the inputs to their default values
    public void ResetForm(bool regular)
    {
        if (regular) { bodyType.value = 0; }

        bodyName.text = "";
        bodySize.value = 0;
        bodyPosition.value = Camera.main.transform.position.x;
        bodySpeed.value = 0.1f;

        bodyColor.color = Color.red;

        sunClass.value = 4;

        planetTemp.value = 0;
        planetComp.value = 0;
        planetSize.value = 2;
        planetRings.isOn = false;
        planetPosition.value = Camera.main.transform.position.x;

        moonPlanet.value = 0;
        moonType.value = 0;
        moonPosition.value = moonPosition.minValue;
    }

    //Calls UpdateBody() which Updates the Body name value with the name whenever the name is changed in the input box and then updates the visuals
    void UpdateNameInputValue(string value)
    {
        UpdateBody();
    }

    //Closes all options panels then sets correct panel for the options, then resets the form
    //then Calls UpdateBody() which Updates the Body type value with the type whenever the type is changed in the dropdown and then updates the visuals
    void UpdateBodyTypeDropdownValue(int value)
    {
        ClosePanels();
        panels[value].SetActive(true);
        ResetForm(false);

        UpdateBody();
    }

    //Closes all the different body options panels
    void ClosePanels()
    {
        foreach(GameObject panel in panels)
        {
            panel.SetActive(false);
        }
    }

    //START DEFAULT UPDATES
    //Default Updates call UpdateBody() which updates the Body values with their respective inputs and then updates the visuals
    //Dropdown Default
    void UpdateDefaultDropdownValue(int value)
    {
        UpdateBody();
    }

    //Slider Default
    void UpdateDefaultSliderValue(float value)
    {
        UpdateBody();
    }

    //Toggle Default
    void UpdateDefaultToggleValue(bool value)
    {
        UpdateBody();
    }

    //Color Default
    void UpdateDefaultColorValue(Color color)
    {
        UpdateBody();

        colorIMG.color = color; //Updates the color image to show to the user that the color was changed
    }
    //END DEFAULT UPDATES

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

        Body[] bodies = FindObjectsOfType<Body>(); //Get all the bodies

        foreach (Body body in bodies)
        {
            if (orbiting) { body.StartRotate(); }
            else { body.PauseRotate(); }
        }           
    }

    //Reset Body Positions
    public void ResetPositions()
    {
        //Check to make sure they arent orbiting
        if(orbiting)
        {
            ToggleRotate();
        }

        Body[] bodies = FindObjectsOfType<Body>(); //Get all the bodies

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

        //Check if there are planets
        if (BodyTypeReturn.planets != null && BodyTypeReturn.planets.Count > 0)
        {
            temp = BodyTypeReturn.planets[PlanetSearch(moonPlanet.value)]; // search planet from planets, get Body
            tempID = int.Parse(BodyTypeReturn.planets[PlanetSearch(moonPlanet.value)].name); // search planet from planets, get id
        }

        //Check if the body is a moon
        if (newBody?.bType == 4) //if Moon
        {
            newBody?.SetPlanet(); //Set the parent to the current selected planet
        }

        //Set the parameters of the body
        newBody?.SetParameters(bodyName.text, bodyType.value, bodySize.value, bodySpeed.value, bodyColor.color, bodyPosition.value, sunClass.value, planetTemp.value, planetComp.value, planetSize.value, planetRings, planetPosition.value, temp, tempID.ToString(), moonType.value, moonPosition.value);
        newBody?.SetUpVisuals();
    }
}

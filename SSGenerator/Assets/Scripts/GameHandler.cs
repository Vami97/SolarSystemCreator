using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameHandler : MonoBehaviour
{
    [SerializeField] private GameObject bodyPrefab;
    private BodyCreation gameManager;

    private void Awake()
    {
        gameManager = FindObjectOfType<BodyCreation>(true);
        SaveSystem.Init();
        
    }

    private void Start()
    {
        //Load();
    }

    private void Update()
    {
        //Save();


        if (Input.GetKeyDown(KeyCode.S))
        {
            Save();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            Load();
        }
    }

    private void Save()
    {
        // Save
        int currentid = gameManager.Id;
        List<SaveBody> bodies = new List<SaveBody>();

        Body[] tempbodies = FindObjectsOfType<Body>();

        foreach (Body body in tempbodies)
        {
            SaveBody temp = new SaveBody(body.bName, body.bType, body.bSpeed, body.bSize, body.bColor, body.bLocation, body.sType, body.pTemp, body.pComposition, body.pSize, body.pHasRings, body.pLocation, body.mPlanetID, body.mType, body.mLocation);

            bodies.Add(temp);
        }


        SaveObject saveObject = new SaveObject
        {
            currentid = currentid,
            bodies = bodies
        };
        string json = JsonUtility.ToJson(saveObject);
        SaveSystem.Save(json);

        Debug.Log("Saved");
    }

    private void Load()
    {
        // Load
        string saveString = SaveSystem.Load();
        if (saveString != null)
        {
            SaveObject saveObject = JsonUtility.FromJson<SaveObject>(saveString);

            gameManager.Id = saveObject.currentid;
            foreach(SaveBody body in saveObject.bodies)
            {
                Body newBody = (Instantiate(bodyPrefab) as GameObject).GetComponent<Body>();

                newBody.SetParameters(body.bName, body.bType, body.bSize, body.bSpeed, body.bColor, body.bLocation, body.sType, body.pTemp, body.pComposition, body.pSize, body.pHasRings, body.pLocation, null, body.mPlanetID, body.mType, body.mLocation);
                newBody.SetUpVisuals();         

                gameManager.SetPlanetList();
                Debug.Log("TBA");
            }
        }

        Debug.Log("Loaded");
    }


    private class SaveObject
    {
        public int currentid;
        public List<SaveBody> bodies;
    }

    private class SaveBody
    {
        public SaveBody(string _bName, int _bType, float _bSize, float _bSpeed, Color _bColor, float _bLocation, int _sType, int _pTemp, int _pComp, int _pSize, bool _pHasRings, float _pLocation, string _mPlanetID, int _mType, float _mLocation)
        {
            bName = _bName;
            bType = _bType;
            bSpeed = _bSpeed;

            bSize = _bSize;
            bColor = _bColor;
            bLocation = _bLocation;

            sType = _sType;

            pTemp = _pTemp;
            pComposition = _pComp;
            pSize = _pSize;
            pHasRings = _pHasRings;
            pLocation = _pLocation;

            mPlanetID = _mPlanetID;
            mType = _mType;
            mLocation = _mLocation;
        }
        
        //Parameters
        public string bName;
        public int bType;  //Unknown, Sun, Planet, Asteroid Belt, Moon
        public float bSpeed;

        public float bSize;
        public SerializableColor bColor;
        public float bLocation;

        //Sun Specific Parameters
        public int sType; //O B A F G K M

        //Planet Specific Parameters
        public int pTemp; //Cool, Warm
        public int pComposition; //Barren, Gaseous, Lush, Volcanic, Watery
        public int pSize; //xSmall, Small, Medium, Large, xLarge, xxLarge (Reverse)
        public bool pHasRings;
        public float pLocation;
        //public List<Body> satellites; //Which moons it has

        //Moon Specific Parameters
        public string mPlanetID; //Planet id to update location
        public int mType; //Small Moon, Medium Moon, Large Moon, Man-made Small Satellite, Man-made Large Satellite, Orbital Market
        public float mLocation; //Location away from planet
    }
}
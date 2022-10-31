using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body : MonoBehaviour, IClickable
{
    //Parameters
    public string bName;
    public int bType;  //Unknown, Sun, Planet, Asteroid Belt, Moon
    public float bSpeed;

    public float bSize;
    public Color bColor;
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
    public Body mPlanet; //Planet location
    public string mPlanetID; //Planet id to update location
    public int mType; //Small Moon, Medium Moon, Large Moon, Man-made Small Satellite, Man-made Large Satellite, Orbital Market
    public float mLocation; //Location away from planet

    //Asteroid Belt Specific Parameters
    public int aDensity; //Sparse, Low, Average, High, Extreme
    public float aLocation;


    //START DEFAULTS
    //Defaults
    public static string defName = "No Name";
    public static float[] defSizes = new float[10] { 0.5f, 1f, 1.5f, 2f, 2.5f, 3f, 3.5f, 4f, 4.5f, 5f };
    public static Color[] defColors = new Color[7] { Color.white, Color.red, Color.yellow, Color.green, Color.cyan, Color.blue, Color.magenta };
    public static float defLocation = 0f;
    public Sprite defSprite;

    //Sun Specific Defaults
    public static Color32[] defSunColors = new Color32[7] { new Color32(73, 66, 220, 255), new Color32(74, 130, 220, 255), new Color32(142, 222, 236, 255), new Color32(233, 253, 251, 255), new Color32(232, 216, 32, 255), new Color32(255, 175, 10, 255), new Color32(255, 49, 7, 255) };
    public static float[] defSunSizes = new float[7] { 5f, 4.5f, 4f, 3.5f, 3f, 2.5f, 2f };


    //Planet Specific Defaults
    public static Color32[,] defPlanetColors = new Color32[3, 3] {
        {new Color32(110, 113, 122, 255), new Color32(131, 242, 240, 255), new Color32(171, 217, 255, 255)},
        {new Color32(88, 148, 80, 255), new Color32(6, 184, 145, 255), new Color32(212, 232, 97, 255)},
        {new Color32(171, 98, 53, 255), new Color32(59, 94, 247, 255), new Color32(207, 92, 60, 255)}
    };
    public static float[] defPlanetSizes = new float[7] { 3.5f, 3f, 2.5f, 2f, 1.5f, 1f, 0.5f };
    public Sprite[] defPlanetSpritesCool;
    public Sprite[] defPlanetSpritesWarm;

    //Moon Specific Defaults
    public static float[] defMoonSizes = new float[6] { 0.2f, 0.4f, 0.6f, 0.1f, 0.2f, 0.4f};
    public static Color32[] defMoonColors = new Color32[6] { Color.magenta, Color.magenta, Color.magenta, Color.gray, Color.gray, Color.white};
    //END DEFAULTS




    public void SetParameters(string _bName, int _bType, float _bSize, float _bSpeed, Color _bColor, float _bLocation, int _sType, int _pTemp, int _pComp, int _pSize, bool _pHasRings, float _pLocation, Body _mPlanet, string _mPlanetID, int _mType, float _mLocation)
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

        mPlanet = _mPlanet;
        mPlanetID = _mPlanetID;
        mType = _mType;
        mLocation = _mLocation;
    }

    public void SetParameters(int _bType, float _bSize, float _bSpeed, Color _bColor, float _bLocation, int _sType, int _pTemp, int _pComp, int _pSize, bool _pHasRings, float _pLocation, Body _mPlanet, string _mPlanetID, int _mType, float _mLocation)
    {
        bName = defName;
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

        mPlanet = _mPlanet;
        mPlanetID = _mPlanetID;
        mType = _mType;
        mLocation = _mLocation;
    }    

    public void SetUpVisuals()
    {
        switch (bType)
        {
            case 0: //Default
                gameObject.transform.position = new Vector3(bLocation, 0f, 0f);
                gameObject.transform.localScale = new Vector3(bSize, bSize, bSize);
                gameObject.GetComponent<SpriteRenderer>().color = bColor;
                gameObject.GetComponent<SpriteRenderer>().sprite = defSprite;              
                break;
            case 1: //Sun
                gameObject.transform.position = Vector3.zero;
                gameObject.transform.localScale = new Vector3(defSunSizes[sType], defSunSizes[sType], defSunSizes[sType]);
                gameObject.GetComponent<SpriteRenderer>().color = defSunColors[sType];
                gameObject.GetComponent<SpriteRenderer>().sprite = defSprite;
                break;
            case 2: //Planet
                gameObject.transform.position = new Vector3(pLocation, 0f, 0f);
                gameObject.transform.localScale = new Vector3(defPlanetSizes[pSize], defPlanetSizes[pSize], defPlanetSizes[pSize]);
                gameObject.GetComponent<SpriteRenderer>().color = Color.white;

                switch(pTemp)
                {
                    case 0:
                        gameObject.GetComponent<SpriteRenderer>().sprite = defPlanetSpritesCool[pComposition];
                        break;
                    case 1:
                        gameObject.GetComponent<SpriteRenderer>().sprite = defPlanetSpritesWarm[pComposition];
                        break;
                    default:
                        break;
                }                
                break;
            case 3: //Asteroid Belt
                
                break;
            case 4: //Moon
                SetPlanet();
                float mPlanetRadius = mPlanet.GetComponent<CircleCollider2D>().radius;
                //Debug.Log("Radius: " + mPlanetRadius);

                float mPlanetScaleFactor = mPlanet.transform.localScale.x;
                gameObject.transform.position = new Vector3(mPlanet.transform.position.x, (mPlanetRadius + mLocation/(mPlanetScaleFactor * 3.5f)) * mPlanetScaleFactor, 0f);        
                gameObject.transform.localScale = new Vector3(defMoonSizes[mType] / mPlanetScaleFactor, defMoonSizes[mType] / mPlanetScaleFactor, defMoonSizes[mType] / mPlanetScaleFactor);
                gameObject.GetComponent<SpriteRenderer>().color = defMoonColors[mType];
                gameObject.GetComponent<SpriteRenderer>().sprite = defSprite;
                break;
            default:
                break;
        }

        //MODIFY CHILDREN
        if (transform.childCount > 0)
        {
            //Get all moons of the object
            Body[] moons = gameObject.GetComponentsInChildren<Body>();

            //Check if Object has moons
            if (moons.Length > 0)
            {
                //Set Moon Visuals
                foreach (Body moon in moons) { if(moon.bType == 4)moon.SetUpVisuals(); }
            }
        }
       
    }

    public bool rotate;

    public void Click()
    {
        //Open edit panel
        GameObject.FindObjectOfType<BodyModification>().SetBodyToEdit(this);  
    }

    public void StartRotate()
    {
        rotate = true;
        StartCoroutine(RotateBody());
    }

    public void PauseRotate()
    {
        rotate = false;
    }

    public void SetPlanet()
    {
        Transform mPTransform = GameObject.Find(mPlanetID).transform;
        gameObject.transform.parent = mPTransform;
    }

    IEnumerator RotateBody()
    {
        while(rotate)
        {
            switch(bType)
            {
                case 0:
                    gameObject.transform.RotateAround(Vector3.zero, Vector3.back, bSpeed * Time.deltaTime);
                    break;
                case 1:
                    gameObject.transform.RotateAround(Vector3.zero, Vector3.back, bSpeed * Time.deltaTime);
                    break;
                case 2:
                    gameObject.transform.RotateAround(Vector3.zero, Vector3.back, bSpeed * Time.deltaTime);
                    break;
                case 3:
                    break;
                case 4:
                    Vector3 mPlanetPos = GameObject.Find(mPlanetID).transform.position;
                    gameObject.transform.RotateAround(mPlanetPos, Vector3.back, bSpeed * Time.deltaTime);
                    break;
                default:
                    break;
            }

            
            yield return new WaitForFixedUpdate();
        }
    }

    BodyCreation creator; 

    private void Start()
    {
        creator = FindObjectOfType<BodyCreation>();
    }

    public void Delete()
    {
        //remove from planet lists
        BodyTypeReturn.planets.Remove(this);

        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        //destroy moons
        DestroyMoons();
    }

    private void DestroyMoons()
    {
        if (bType == 0 || bType == 2)
        {
            Body[] moons = gameObject.GetComponentsInChildren<Body>();

            foreach (Body moon in moons)
            {
                Destroy(moon.gameObject);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonBody : SpaceBody, IClickable
{
    //Moon Specific Parameters
    public Vector3 moonPlanet; //Planet location
    public string moonPlanetID; //Planet id to update location
    public int moonType; //Small Moon, Medium Moon, Large Moon, Man-made Small Satellite, Man-made Large Satellite, Orbital Market
    public float moonLocation; //Location away from planet

    public void SetMoonParameters(string bName, int bType, int mType)
    {
        bodyName = bName;
        bodyType = bType;

        moonType = mType;
    }

    public void Click()
    {
        throw new System.NotImplementedException();
    }

    public override void SetVisuals()
    {
        gameObject.transform.position = new Vector3(moonPlanet.x, moonLocation, 0f);
        gameObject.transform.localScale = new Vector3(BodyDefaults.defMoonSizes[moonType], BodyDefaults.defMoonSizes[moonType], BodyDefaults.defMoonSizes[moonType]);
        gameObject.GetComponent<SpriteRenderer>().color = BodyDefaults.defMoonColors[moonType];
    }

    public IEnumerator Rotate()
    {
        while (true)
        {
            yield return new WaitForFixedUpdate();

            while (BodyDefaults.rotate)
            {
                moonPlanet = GameObject.Find(moonPlanetID).transform.position;

                gameObject.transform.RotateAround(moonPlanet, Vector3.back, bodySpeed * Time.deltaTime);
                yield return new WaitForFixedUpdate();
            }
        }
    }
}

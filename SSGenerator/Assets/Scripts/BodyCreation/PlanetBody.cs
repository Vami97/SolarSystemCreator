using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetBody : SpaceBody, IClickable
{
    //Planet Specific Parameters
    public int planetTemperature; //Cool, Moderate, Hot
    public int planetComposition; //Rocky, Watery, Gas
    public int planetSize; //xSmall, Small, Medium, Large, xLarge, xxLarge (Reverse)
    public bool planetHasRings;
    public float planetLocation;


    public void SetPlanetParameters(int pTemp, int pComp, int pSize, bool pRings, float pLoc)
    {
        planetTemperature = pTemp;
        planetComposition = pComp;
        planetSize = pSize;

        planetHasRings = pRings;
        planetLocation = pLoc;
    }

    public void Click()
    {
        throw new System.NotImplementedException();
    }

    public override void SetVisuals()
    {
        gameObject.transform.position = new Vector3(planetLocation * 225 / 100, 0f, 0f);
        gameObject.transform.localScale = new Vector3(BodyDefaults.defPlanetSizes[planetSize], BodyDefaults.defPlanetSizes[planetSize], BodyDefaults.defPlanetSizes[planetSize]);
        gameObject.GetComponent<SpriteRenderer>().color = BodyDefaults.defPlanetColors[planetTemperature, planetComposition];
    }

    public IEnumerator Rotate()
    {
        while (true)
        {
            yield return new WaitForFixedUpdate();

            while (BodyDefaults.rotate)
            {
                gameObject.transform.RotateAround(Vector3.zero, Vector3.back, 90 * Time.deltaTime);
                yield return new WaitForFixedUpdate();
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunBody : SpaceBody, IClickable
{
    //Sun Specific Parameters
    public int sunType; //O B A F G K M

    public void SetSunParameters(int sType)
    {
        sunType = sType;
    }

    public void Click()
    {
        throw new System.NotImplementedException();
    }

    public override void SetVisuals()
    {
        gameObject.transform.position = Vector3.zero;
        gameObject.transform.localScale = new Vector3(BodyDefaults.defSunSizes[sunType], BodyDefaults.defSunSizes[sunType], BodyDefaults.defSunSizes[sunType]);
        gameObject.GetComponent<SpriteRenderer>().color = BodyDefaults.defSunColors[sunType];
    }


    public IEnumerator Rotate()
    {
        while(true)
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

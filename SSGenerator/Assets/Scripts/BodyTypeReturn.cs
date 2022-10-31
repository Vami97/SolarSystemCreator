using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BodyTypeReturn
{
    //Planets list
    public static List<Body> planets = new List<Body>();

    //This is how the bodies should be named in the dropdown
    private static string[] bodyTypes = new string[] { "Custom Planet", "Sun", "Planet", "Asteroid Belt", "Moon/Satellite" };

    public static int BodyType(string str)
    {
        for(int i = 0; i < bodyTypes.Length; i++)
        {
            if (str == bodyTypes[i]) return i;
        }

        return 0; //Default if option picked isnt right
    }
}

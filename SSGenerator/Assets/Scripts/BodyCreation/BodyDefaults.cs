using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyDefaults
{
    //Changable
    public static bool rotate = false;

    //General Defaults
    public static string defName = "No Name";
    public static float defLocation = 0f;


    //Custom Specific Defaults


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


    //Moon Specific Defaults
    public static float[] defMoonSizes = new float[6] { 0.5f, 1f, 1.5f, 0.25f, 1f, 2f };
    public static Color32[] defMoonColors = new Color32[6] { Color.magenta, Color.magenta, Color.magenta, Color.gray, Color.gray, Color.white };


    //Asteroid Belt Specific Defaults

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkyColor", menuName = "Game/New Sky Color")]
public class SkyColor : ScriptableObject
{

    public Color sunny;
    public Color cloudy;
    public Color windy;

    public static SkyColor Instance
    {
        get
        {
            return Resources.Load<SkyColor>("Weather/SkyColor");
        }
    }
}

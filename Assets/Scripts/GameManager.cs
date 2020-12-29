using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton {
    public static string GEL_TAG = "Gel";
    public static string PLAYER_TAG = "Player";
    public static string BUTTON_COLLIDER = "Button Collider";
    public static string BUTTON_TRIGGER = "Button Trigger";
    public static string CRATE = "Crate";
    public static string GOAL = "Goal";
    public static string ETHEREAL_AREA = "Ethereal Gel Area";
    public static string TerrainLayer = "Terrain";
    public static string SwitchLayer = "Switch";
    public static string CratePlayerLayer = "Crate - Player";
    public static string HazardLayer = "Hazard";
    public enum GelType {
        Bounce,
        Ethereal
    }
}

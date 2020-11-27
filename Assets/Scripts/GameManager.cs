using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton {
    public static string GEL_TAG = "Gel";
    public static string PLAYER_TAG = "Player";
    public static string FLOOR_TAG = "Floor";
    public static string BUTTON_COLLIDER = "Button Collider";
    public static string BUTTON_TRIGGER = "Button Trigger";
    public static string CRATE = "Crate";
    public static string GOAL = "Goal";
    public static string GROUNDING = "Grounding";

    public enum GelType {
        Bounce,
        Ethereal
    }
}

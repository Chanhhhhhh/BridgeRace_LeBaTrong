using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Constants
{

    public const string ANIM_RUN = "run";
    public const string ANIM_IDLE = "idle";
    public const string ANIM_STAND = "stand";
    public const string ANIM_FALLING = "fall";
    public const string ANIM_WIN = "win";



    public const string TAG_BRICK = "Brick";
    public const string TAG_PLAYER = "Player";
    public const string TAG_BOT = "Bot";
    public const string TAG_FINISHBOX = "FinishBox";

    public static readonly string[] ANIM_SCENE =
    {
        "start1"
    };

    public static string GetRandomAnimScene()
    {
        return names[Random.Range(0, ANIM_SCENE.Length - 1)];
    }
    public static List<string> names = new List<string>
       {
            "John",
            "Emma",
            "Liam",
            "Olivia",
            "Noah",
            "Ava",
            "Lucas",
            "Mia",
            "Ben",
            "Sophia",
            "James",
            "Charlotte",
            "Elijah",
            "Emily",
            "Henry",
            "Grace",
            "David",
            "Lily",
            "Leo",
            "Anna",
        };
    public static string GetName()
    {
        return names[Random.Range(0,names.Count-1)];
    }
}


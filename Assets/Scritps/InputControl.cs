using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputControl : MonoBehaviour
{
    public static bool isBloked { get; private set; }

    public static void SetBoolTrue()
    {
        isBloked = true;
    }
    public static void SetBoolFalse()
    {
        isBloked = false;
    }
}

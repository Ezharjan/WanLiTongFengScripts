using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static int ProgressValue = 0;
    private void Awake()
    {

        ProgressValue = 0;
    }
    public static int People
    {
        get
        {
            return _people;
        }
        set
        {
            if (value > 100) _people = 100;
            else if (value < 0) _people = 0;
            else _people = value;
        }
    }
    public static int Policy
    {
        get
        {
            return _policy;
        }
        set
        {
            if (value > 100) _policy = 100;
            else if (value < 0) _policy = 0;
            else _policy = value;
        }
    }
    public static int Military
    {
        get
        {
            return _military;
        }
        set
        {
            if (value > 100) _military = 100;
            else if (value < 0) _military = 0;
            else _military = value;
        }
    }
    public static int Economic
    {
        get
        {
            return _economic;
        }
        set
        {
            if (value > 100) _economic = 100;
            else if (value < 0) _economic = 0;
            else _economic = value;
        }
    }

    static int _people;
    static int _policy;
    static int _military;
    static int _economic;

    public static void ChangeAllValue(int people, int policy, int military, int economic)
    {
        People += people;
        Policy += policy;
        Economic += economic;
        Military += military;
    }
    public static void SetAllValue(int people, int policy, int military, int economic)
    {
        People = people;
        Policy = policy;
        Economic = economic;
        Military = military;
    }
}
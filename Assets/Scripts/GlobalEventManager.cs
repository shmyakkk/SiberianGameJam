using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GlobalEventManager
{
    public static UnityEvent OnStartedDay = new UnityEvent();
    public static UnityEvent OnStartedNight = new UnityEvent();

    public static UnityEvent OnEnterQ = new UnityEvent();
    public static UnityEvent OnReloadQ = new UnityEvent();


    public static void SendStartedDay() => OnStartedDay.Invoke();
    public static void SendStartedNight() => OnStartedNight.Invoke();
    public static void SendEnterQ() => OnEnterQ.Invoke();
    public static void SendReloadQ() => OnReloadQ.Invoke();
}

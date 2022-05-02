using UnityEngine.Events;

public class GlobalEventManager
{
    public static UnityEvent OnStartedDay = new UnityEvent();
    public static UnityEvent OnStartedNight = new UnityEvent();

    public static void SendStartedDay() => OnStartedDay.Invoke();
    public static void SendStartedNight() => OnStartedNight.Invoke();
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class GlobalEvents
{
    public static UnityEvent<int> EventStartSceneLoading = new UnityEvent<int>();
    public static UnityEvent<int> EventEndSceneLoading = new UnityEvent<int>();
    public static UnityEvent EventIvanIsDead = new UnityEvent();
    public static UnityEvent<string> EventStartMinigameInput = new UnityEvent<string>();
    public static UnityEvent EventEndMinigameInput = new UnityEvent();
}

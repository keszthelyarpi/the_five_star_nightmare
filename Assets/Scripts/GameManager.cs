using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static Action OnStateChanged;
    public static HashSet<Milestones> Milestones = new();

    public static void AddMilestone(Milestones milestone)
    {
        Milestones.Add(milestone);
        OnStateChanged?.Invoke();
    }

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
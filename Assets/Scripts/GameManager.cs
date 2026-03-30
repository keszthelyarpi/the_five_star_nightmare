using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static Action OnStateChanged;
    public static HashSet<MilestoneSet> Milestones = new() { MilestoneSet.GameStarted };

    public static void AddMilestone(MilestoneSet milestone)
    {
        Milestones.Add(milestone);
        OnStateChanged?.Invoke();
    }

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
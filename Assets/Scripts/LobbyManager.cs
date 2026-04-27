using UnityEngine;

public class LobbyManager : MonoBehaviour
{
    [Header("NPC Referenciák")]
    public GameObject mrDidereg;
    public GameObject madamePompas;
    public GameObject general;

    void Start()
    {
        UpdateLobbyState();
    }

    public void UpdateLobbyState()
    {
        // Alapértelmezetten mindenki ki van kapcsolva
        mrDidereg.SetActive(false);
        madamePompas.SetActive(false);
        general.SetActive(false);

        // A specifikáció szerinti sorrend alapján aktiválunk 
        if (!GameManager.Milestones.Contains(MilestoneSet.Level1Completed))
        {
            mrDidereg.SetActive(true);
        }
        else if (!GameManager.Milestones.Contains(MilestoneSet.Level2Completed))
        {
            madamePompas.SetActive(true);
        }
        else if (!GameManager.Milestones.Contains(MilestoneSet.AllMissionsDone))
        {
            general.SetActive(true);
        }
    }
}
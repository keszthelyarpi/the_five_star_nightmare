using System.Collections;
using UnityEngine;

public class BoilerWheel : MonoBehaviour, IInteractable
{
    [Header("Forgatás beállításai")]
    public float rotationAngle = -180f;
    public float rotationSpeed = 1f;

    private bool isRotating = false;

    // Dinamikus szöveg a specifikáció szerint 
    public string GetInteractText()
    {
        if (GameManager.Milestones.Contains(MilestoneSet.BoilerStarted))
            return "A kazán már üzemel";

        if (!GameManager.Milestones.Contains(MilestoneSet.TalkedToMrDidereg))
            return "Be van rozsdásodva... (Beszélj a vendéggel)";

        return "Szelep elfordítása (E)";
    }

    public void Interact()
    {
        if (isRotating) return;

        // 1. Ellenőrizzük, hogy felvettük-e a küldetést
        if (!GameManager.Milestones.Contains(MilestoneSet.TalkedToMrDidereg))
        {
            // Itt küldhetsz egy hibaüzenetet a UI-ra a specifikáció szerint 
            Debug.Log("Még nem tudod, hogyan kell használni.");
            return;
        }
        else if (GameManager.Milestones.Contains(MilestoneSet.BoilerStarted))
        {
            return;
        }
        else
        {
            // 3. Küldetés teljesítése: Milestone hozzáadása és animáció
            GameManager.AddMilestone(MilestoneSet.BoilerStarted);
            StartCoroutine(RotateWheelSmoothly());
        }

        Debug.Log("A fűtés elindult, a küldetés célja teljesítve!");
    }

    private IEnumerator RotateWheelSmoothly()
    {
        isRotating = true;
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = startRotation * Quaternion.Euler(0, 0, rotationAngle);
        float timeElapsed = 0;

        while (timeElapsed < 1f)
        {
            timeElapsed += Time.deltaTime * rotationSpeed;
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, timeElapsed);
            yield return null;
        }

        transform.rotation = endRotation;
        isRotating = false;
    }
}
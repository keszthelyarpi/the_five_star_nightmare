using UnityEngine;

public class WaypointPatrol : MonoBehaviour
{
    [Header("Útvonal Pontok")]
    public Transform[] waypoints; // Húzd be ide a Point_A, Point_B stb. objektumokat
    public float moveSpeed = 2f;
    public float waitTime = 1f;   // Mennyit várjon a pontnál? (A specifikáció szerinti "Turn" állapot)

    private int currentPointIndex = 0;
    private bool isWaiting = false;

    void Update()
    {
        if (isWaiting || waypoints.Length == 0) return;

        // 1. Célpont meghatározása
        Transform target = waypoints[currentPointIndex];

        // 2. Mozgás a cél felé
        transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);

        // 3. Megérkezés ellenőrzése
        if (Vector2.Distance(transform.position, target.position) < 0.1f)
        {
            StartCoroutine(WaitAndTurn());
        }
    }

    private System.Collections.IEnumerator WaitAndTurn()
    {
        isWaiting = true;

        // Következő pont indexének kiszámítása (ciklikus)
        currentPointIndex = (currentPointIndex + 1) % waypoints.Length;

        // Várakozás (Turn állapot)
        yield return new WaitForSeconds(waitTime);

        // Megfordulás (Sprite tükrözése)
        Flip();

        isWaiting = false;
    }

    void Flip()
    {
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }
}
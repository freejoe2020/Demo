using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float lifeTime = 10f; // The lifetime of the bullet in seconds

    void Start()
    {
        // Randomly select one of the colors: red, green, or blue
        Color[] colors = new Color[] { Color.red, Color.green, Color.blue };
        int randomIndex = Random.Range(0, colors.Length);
        GetComponent<Renderer>().material.color = colors[randomIndex];

        // Automatically destroy the bullet after a set time (10 seconds)
        Destroy(gameObject, lifeTime);
    }
}

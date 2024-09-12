using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Speed of the projectile
    public float speed = 5f;

    // Direction of the projectile
    private Vector2 direction;

    // Start is called before the first frame update
    void Start()
    {
        // Set the direction of the projectile
        // Here we assume the projectile moves towards the right
        direction = Vector2.right;
    }

    // Update is called once per frame
    void Update()
    {
        // Move the projectile in the specified direction
        transform.Translate(direction * speed * Time.deltaTime);

        // Check if the projectile is out of the screen
        if (!IsVisible())
        {
            // Destroy the projectile if it's out of the screen
            Destroy(gameObject);
        }
    }

    // Check if the projectile is visible within the camera's viewport
    private bool IsVisible()
    {
        Vector3 viewportPosition = Camera.main.WorldToViewportPoint(transform.position);
        return viewportPosition.x > 0 && viewportPosition.x < 1 && viewportPosition.y > 0 && viewportPosition.y < 1;
    }
}
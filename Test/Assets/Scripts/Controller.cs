using UnityEngine;

public class Controller : MonoBehaviour
{
    public float moveSpeed = 5f; // 物体移动速度
    public float jumpForce = 10f; // 跳跃力量
    public KeyCode moveForwardKey = KeyCode.W; // 向前移动的键
    public KeyCode moveBackwardKey = KeyCode.S; // 向后移动的键
    public KeyCode moveLeftKey = KeyCode.A; // 向左移动的键
    public KeyCode moveRightKey = KeyCode.D; // 向右移动的键
    public KeyCode jumpKey = KeyCode.Space; // 跳跃的键
    public GameObject bulletPrefab; // Assign the bullet prefab in Unity Editor
    public float bulletSpeed = 10f; // Speed of the bullet
    public KeyCode fireKey = KeyCode.E; // Key to fire the bullet

    private Rigidbody rb;
    private bool isGrounded = true; // 是否在地面上

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody component not found on the object.");
        }
    }

    void Update()
    {
        Move();
        if (Input.GetKeyDown(jumpKey) && isGrounded)
        {
            Jump();
        }
        if (Input.GetKeyDown(fireKey))
        {
            FireBullet();
        }
    }
    void FireBullet()
    {
        if (bulletPrefab != null)
        {
            // Instantiate the bullet at the top of the object
            GameObject bullet = Instantiate(bulletPrefab, transform.position + Vector3.up, Quaternion.identity);
            Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();

            if (bulletRb != null)
            {
                // Calculate the direction: 45 degrees upward in the object's forward direction
                Vector3 fireDirection = (transform.forward + Vector3.up).normalized;

                // Apply force to the bullet in the calculated direction
                bulletRb.AddForce(fireDirection * bulletSpeed, ForceMode.Impulse);
            }
            else
            {
                Debug.LogError("Bullet prefab does not have a Rigidbody component.");
            }
        }
        else
        {
            Debug.LogError("Bullet prefab is not assigned.");
        }
    }


    void Move()
    {
        float moveX = 0f;
        float moveZ = 0f;

        // 检查WASD和箭头按键
        if (Input.GetKey(moveLeftKey) || Input.GetKey(KeyCode.LeftArrow))
        {
            moveX = -moveSpeed;
        }
        else if (Input.GetKey(moveRightKey) || Input.GetKey(KeyCode.RightArrow))
        {
            moveX = moveSpeed;
        }

        if (Input.GetKey(moveForwardKey) || Input.GetKey(KeyCode.UpArrow))
        {
            moveZ = moveSpeed;
        }
        else if (Input.GetKey(moveBackwardKey) || Input.GetKey(KeyCode.DownArrow))
        {
            moveZ = -moveSpeed;
        }

        Vector3 moveDirection = new Vector3(moveX, 0, moveZ).normalized;
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }

    void Jump()
    {
        rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
        isGrounded = false;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}

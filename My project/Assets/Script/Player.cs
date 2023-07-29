using UnityEngine;

 class Player : MonoBehaviour

 {
    [SerializeField] Collider2D[] results;
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] float jumpStrength = 1f;

    Vector2 direction;
    new Rigidbody2D rigidbody;
    new Collider2D collider;

    bool grounded;

    void Awake()

    {
        rigidbody = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
        results = new Collider2D[1];
    }

    void CheckCollison()
    {
        grounded = false;

        Vector2 size = collider.bounds.size;
        size.y += 0.1f;
        size.x /= 1f;

        int amount = Physics2D.OverlapBoxNonAlloc(transform.position, size, 0f, results);

        for (int i = 0; i < amount; i++)
        {
            GameObject hit = results[i].gameObject;

            if (hit.layer == LayerMask.NameToLayer("Ground"))
            {
                grounded = hit.transform.position.y < (transform.position.y - 0.5f);

                Physics2D.IgnoreCollision(collider, results[i], !grounded);
            }
        }
    }

    void Update()

    {
        CheckCollison();

        if (grounded && Input.GetButtonDown("Jump"))
        {
            direction = Vector2.up * jumpStrength;
        }
        else
        {
            direction += Physics2D.gravity * Time.deltaTime;
        }

        direction.x = Input.GetAxis("Horizontal") * moveSpeed;

        if (grounded)
        {
            direction.y = Mathf.Max(direction.y, -1f);
        }

        if (direction.x > 0f)
        {
            transform.eulerAngles = Vector3.zero;
        }

        else if (direction.x < 180f)
        {
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }

        rigidbody.MovePosition(rigidbody.position + direction * Time.deltaTime);

    }

    void FixedUpdate()
    {

        rigidbody.MovePosition(rigidbody.position + direction * Time.fixedDeltaTime);

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Objective"))
        {
            enabled = false;
            FindObjectOfType<GameManager>().LevelComplete();
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            enabled = false;
            FindObjectOfType<GameManager>().LevelFailed();
        }
    }

}


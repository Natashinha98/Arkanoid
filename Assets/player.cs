using UnityEngine;

public class player : MonoBehaviour
{
    public KeyCode moveLeft = KeyCode.A;
    public KeyCode moveRight = KeyCode.D;
    public float speed = 12.0f;

    // Mouse control is enabled by default. Keyboard controls remain available.
    public bool useMouseControl = true;

    private Rigidbody2D rb2d;
    private float limiteEsquerdo;
    private float limiteDireito;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();

        GameObject leftWall = GameObject.Find("LeftWall");
        GameObject rightWall = GameObject.Find("RightWall");

        if (leftWall != null && rightWall != null)
        {
            float wallLeftPos = leftWall.transform.position.x;
            float wallRightPos = rightWall.transform.position.x;
            float wallLeftWidth = leftWall.transform.localScale.x;
            float wallRightWidth = rightWall.transform.localScale.x;

            float wallLeftEdge = wallLeftPos + (wallLeftWidth / 2);
            float wallRightEdge = wallRightPos - (wallRightWidth / 2);

            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            float halfWidth = sr != null ? sr.bounds.extents.x : 0.52f;

            limiteEsquerdo = wallLeftEdge + halfWidth;
            limiteDireito = wallRightEdge - halfWidth;
        }
        else
        {
            limiteEsquerdo = -4.83f;
            limiteDireito = 4.83f;
        }

        if (rb2d != null)
            rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    void Update()
    {
        if (rb2d == null) return;

        // Keyboard: A/D and LEFT/RIGHT arrows.
        float keyboardDirection = 0f;

        if (Input.GetKey(moveLeft) || Input.GetKey(KeyCode.LeftArrow))
            keyboardDirection -= 1f;

        if (Input.GetKey(moveRight) || Input.GetKey(KeyCode.RightArrow))
            keyboardDirection += 1f;

        // Mouse: move the paddle horizontally with the mouse cursor.
        // Keyboard input takes priority when a key is being pressed.
        if (keyboardDirection != 0f)
        {
            Vector2 vel = rb2d.linearVelocity;
            vel.x = keyboardDirection * speed;
            rb2d.linearVelocity = vel;
        }
        else if (useMouseControl && Camera.main != null)
        {
            Vector3 mouseScreen = Input.mousePosition;
            Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(
                new Vector3(mouseScreen.x, mouseScreen.y, Mathf.Abs(Camera.main.transform.position.z))
            );

            float targetX = Mathf.Clamp(mouseWorld.x, limiteEsquerdo, limiteDireito);
            float distance = targetX - transform.position.x;

            // Smooth enough to feel responsive without changing the collision/gameplay logic.
            Vector2 vel = rb2d.linearVelocity;
            vel.x = Mathf.Clamp(distance * 20f, -speed * 1.5f, speed * 1.5f);
            rb2d.linearVelocity = vel;
        }
        else
        {
            Vector2 vel = rb2d.linearVelocity;
            vel.x = 0f;
            rb2d.linearVelocity = vel;
        }

        // Keep the paddle inside the walls.
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, limiteEsquerdo, limiteDireito);
        transform.position = pos;
    }
}

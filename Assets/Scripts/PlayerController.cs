using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    private float elapsedTime = 0f;
    private float score = 0f;

    public float scoreMultiplier = 10f;
    public float thrustForce = 1f;

    Rigidbody2D rb;

    public UIDocument uiDocument;
    private Label scoreText;
    public GameObject explosionEffect;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        scoreText = uiDocument.rootVisualElement.Q<Label>("ScoreLabel");
    }

    void Update()
    {
        void UpdateScore()
            {
            elapsedTime += Time.deltaTime;
            score = Mathf.FloorToInt(elapsedTime * scoreMultiplier);
            scoreText.text = "Score: " + score;

            }
        void MovePlayer()
            {
            if (Mouse.current.leftButton.isPressed)
            {

                // Calculate mouse direction
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.value);
                Vector2 direction = (mousePos - transform.position).normalized;

                // Move player in direction of mouse
                transform.up = direction;
                rb.AddForce(direction * thrustForce);
            }

            }
        UpdateScore();
        MovePlayer();
      



    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Instantiate(explosionEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }

}

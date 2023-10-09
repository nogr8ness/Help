using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed, jumpPower;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI deathsText;
    [SerializeField] private TextMeshProUGUI highScoreText;

    [Header("Multiplayer Params")]
    [SerializeField] private Player otherPlayer;
    [SerializeField] private TextMeshProUGUI redScoreText, purpleScoreText;

    public static int score;
    public int gameScore;

    private int deaths;
    private int highScore;
    private float horizontalInput;

    //References
    private Rigidbody2D body;
    private Tracker tracker;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        tracker = GetComponent<Tracker>();

        if (PlayerPrefs.GetInt("collisions", -1) == -1)
            PlayerPrefs.SetInt("collisions", 0);

        //If multiplayer is enabled
        if(PlayerPrefs.GetInt("multiplayer", 0) == 1)
        {
            if (PlayerPrefs.GetInt("collisions") == 1)
                Physics2D.IgnoreLayerCollision(8, 8, false);

            else
                Physics2D.IgnoreLayerCollision(8, 8, true);

            redScoreText.gameObject.SetActive(true);
            purpleScoreText.gameObject.SetActive(true);

            deathsText.enabled = false;

            otherPlayer.gameObject.SetActive(true);
        }
        
    }

    private void Update()
    {
        if (gameObject.name == "Player1")
        {
            if (Input.GetKey(KeyCode.RightArrow))
                horizontalInput = 1;

            else if (Input.GetKey(KeyCode.LeftArrow))
                horizontalInput = -1;

            else
                horizontalInput = 0;
        }
        
        if (gameObject.name == "Player2")
        {
            if (Input.GetKey(KeyCode.D))
                horizontalInput = 1;

            else if (Input.GetKey(KeyCode.A))
                horizontalInput = -1;

            else
                horizontalInput = 0;
        }

        body.velocity = new Vector2(horizontalInput * speed * Tracker.reversed, body.velocity.y);
        body.mass = Tracker.mass;

        scoreText.text = "Score: " + score;

        if (gameObject.name == "Player1" && Input.GetKeyDown(KeyCode.UpArrow))
        {
            body.AddForce(new Vector2(0, jumpPower), ForceMode2D.Impulse);
        }
        
        if (gameObject.name == "Player2" && Input.GetKeyDown(KeyCode.W))
        {
            body.AddForce(new Vector2(0, jumpPower), ForceMode2D.Impulse);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Spike") {
            transform.position = Vector3.zero;
            otherPlayer.transform.position = new Vector3(0, 1, 0);

            body.velocity = Vector2.zero;
            otherPlayer.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

            if(PlayerPrefs.GetInt("multiplayer", 0) == 0)
            {
                deaths++;
                deathsText.text = "Deaths: " + deaths;
            }
            else
            {
                otherPlayer.gameScore++;

                if(gameObject.name == "Player1")
                    purpleScoreText.text = "Purple: " + otherPlayer.gameScore;

                else
                    redScoreText.text = "Red: " + otherPlayer.gameScore;
            }
                
            
            if(score > highScore)
            {
                highScore = score;
                highScoreText.text = "High score: " + highScore;
            }

            score = 0;

            tracker.ResetAll();
        }
    }
}

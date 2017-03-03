using System;
using UnityEngine;
using UnityEngine.UI;

public class BallControl : MonoBehaviour
{
    public Text bounceText;
    public Text endText;
    public Text timeText;

    public float speed;

    private MeshRenderer ballRender;
    private Rigidbody2D ballBody;
    private Vector3 startMove;
    private float startHorizontal;
    private float startVertical;
    private float x;
    private float y;
    private float startTime;
    private float endTime;
    private int quadrant;
    private int bounceCount;    
    public static bool startGame;

    void Start()
    {
        ballRender = GetComponent<MeshRenderer>();
        ballBody = GetComponent<Rigidbody2D>();

        // Set x and y direction forces to be applied to the ball
        // Note that if Project Settings -> Physics 2D -> Max Translation Speed is increased too much...
        // ...the ball will go slower the closer either x or y is to 0 or 1, in other words...
        // ...if it's going directly (North/South/East/West), it will be slowest, and it will be fastest going directly (NW/SW/NE/SE)
        x = 1 - UnityEngine.Random.value;
        y = 1 - x;

        // Determine which direction the ball will go based on the randomly generated quadrant
        quadrant = UnityEngine.Random.Range(0, 3);
        switch (quadrant)
        {
            case 0:
                startHorizontal = x;
                startVertical = y;
                break;
            case 1:
                startHorizontal = x;
                startVertical = -y;
                break;
            case 2:
                startHorizontal = -x;
                startVertical = -y;
                break;
            case 3:
                startHorizontal = -x;
                startVertical = y;
                break;
        }

        startMove = new Vector2(startVertical, startHorizontal);
        bounceCount = 0;
        Time.timeScale = 0.0F;
        PauseMenuControl.isPaused = false;
        startGame = false;
        bounceText.text = "Click and drag to place a wall.\nUse the wall to keep the ball in the circle!";
        endText.text = "";
        timeText.text = "Press any key to start";
    }

    void Update()
    {
        // If the game hasn't started, wait for the user to press any key or click; once they do, start the game and give the ball its initial push
        if ((!startGame) && (Input.anyKey))
        {
            Time.timeScale = 1.0F;
            startGame = true;
            ballBody.AddForce(startMove * speed);
            startTime = Time.time;
        }

        // If the game has started, set the time display to the nearest integer representation of the time since starting
        if (startGame)
        {
            endTime = Time.time - startTime;
            if (endTime <= 0.98)
            {
                timeText.text = "Time: 0";
            }
            
            else
            {
                timeText.text = string.Format("Time: {0}", endTime.ToString("#"));
            }
        }
    }

    void OnCollisionEnter2D()
    {
        // If the ball bounces off of something (i.e. a wall), increment the bounce counter
        bounceCount++;
        SetBounceText();
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // If the ball exits the playfield, end the game
        if (other.gameObject.CompareTag("Playfield"))
        {
            endTime = Time.time - startTime;
            endText.text = string.Format("Game Over...\nYou made it to {0} bounces in {1} seconds!", bounceCount, endTime.ToString("#.00"));
            EndGame();
        }
    }

    void EndGame()
    {
        // TO DO: destroy the ball in a fancy way
        ballRender.enabled = false;
        Time.timeScale = 0.0F;
    }

    internal void SetBounceText()
    {
        bounceText.text = "Bounces: " + bounceCount.ToString();
    }
}

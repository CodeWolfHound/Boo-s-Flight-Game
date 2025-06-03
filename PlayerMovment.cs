using System.Diagnostics;
using System.Numerics;
using UnityEngine;

public class PlayerMovement: MonoBehaviour
{
    #region Public variables

    public RestartScene RestartSceneScript;
    public float clickCooldown = 1f; // How long to wait between presses
    public AudioSource clickEffect;
    public bool isTouching; // Feeds boolean to RestartScene
    public Rigidbody2D playerBody; // Refrences rigidbody object 
    public int Score;
    public float Timer;

    #endregion // Public variables

    #region Private variables

    private float jumpUpwardsForce; // Static Jump Force (Unused)
    private float timer = 0f; // Countdown clock, starts at zero and counts down to zero after pressing
    private Vector2 ghostDirection;

    #endregion // Private variables

    #region Public Methods

    /// <summary>
    /// Generates a random X axis number.
    /// </summary>
    /// <returns>A random integer between -5 and 5.</returns>
    public int RandomIntX()
    {
        return Random.Range(-5, 5);
    }

    /// <summary>
    /// Triggered when an object collides with this MonoBehaviour.
    /// </summary>
    /// <param name="collision">The object that collided with this MonoBehaviour.</param>
    public void OnTriggerEnter2D(Collider2D collision)
    {
        isTouching = true;
    }

    #endregion // Public Methods

    #region Private Methods

    /// <summary>
    /// Contains initalization logic for this MonoBehaviour.
    /// </summary>
    private void Awake()
    {
        Timer = Time.timeSinceLevelLoad;
    }

    /// <summary>
    /// Triggered every tick. Updates the current timer and score labels on the screen.
    /// Also decreases the current timer by the tick time.
    /// </summary>
    private void Update()
    {
        Timer = Time.timeSinceLevelLoad;
        TimerSet();
        RestartSceneScript.Score.text = Score.ToString();
        RestartSceneScript.Timer.text = Timer.ToString("F2");
    }

    /// <summary>
    /// Decrements the timer by the tick time. If the timer is already at or below zero, it does nothing.
    /// </summary>
    private void TimerSet()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime; // Subtract the time passed
        }
    }

    /// <summary>
    /// Bounces the ghost if the bottom of the ghost has collided with the mouse.
    /// </summary>
    private void GhostBounce()
    {
        // Grabs position of x and y of the gameobject
        float xPosition = transform.position.x;
        float yPosition = transform.position.y;

        playerBody.linearVelocity = new Vector2(RandomIntX(), yPosition);

        playerBody.linearVelocity += Vector2.up * RandomForce();
        ScoreTracker();
        //playerBody.linearVelocity += Vector2.up * jumpUpwardsForce;

        //Debug.Log(xPosition + " " + yPosition); // Logs positions of player

    }

    /// <summary>
    /// Generates a random jump force number.
    /// </summary>
    /// <returns>A random jump force number between 10 and 15.</returns>
    private int RandomForce()
    {
        return Random.Range(10, 15);
    }

    /// <summary>
    /// Triggered when the mouse hovers over this MonoBehaviour.
    /// </summary>
    private void OnMouseOver()
    {
        //Checks if timer is ready when hovered overed over object
        if (timer <= 0)
        {
            clickEffect.Play(); // Plays AudioSource
            GhostBounce(); //Makes player move
            timer = clickCooldown; // resets the timer  and counts again
            //Debug.Log(RandomForce());
        }
    }

    /// <summary>
    /// Increases the score by one. Also prints the current score (before incrementing) to the DEBUG console.
    /// </summary>
    private void ScoreTracker()
    {
        Debug.Log(Score);
        Score++;
    }

    #endregion // Private Methods
}

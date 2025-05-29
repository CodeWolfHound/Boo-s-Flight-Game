
using UnityEngine;


public class PlayerMovment : MonoBehaviour
{
    public RestartScene RestartSceneScript;
    private float jumpUpwardsForce; //Static Jump Force (Unused)
    public float clickCooldown = 1f; // How long to wait between presses
    private float timer = 0f; // Countdown clock , starts at zero  and counts down to zero after pressing
    private Vector2 ghostDirection;
    public AudioSource clickEffect;
    //Feeds boolean to RestartScene
    public bool isTouching;
    //Refrences rigidbody object 
    public Rigidbody2D playerBody;
    public int Score;
    public float Timer;

    private void Awake()
    {
        Timer = Time.timeSinceLevelLoad;
    }
    private void Update()
    {
        Timer = Time.timeSinceLevelLoad;
        TimerSet();
       RestartSceneScript.Score.text = Score.ToString();
       RestartSceneScript.Timer.text = Timer.ToString("F2");

    }
    private void TimerSet()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime; // Subtract the time passed
        }
    }
    private void GhostBounce()
    {
        //Grabs position of x and y of the gameobject
        float xPosition = transform.position.x;
        float yPosition = transform.position.y;
       
        playerBody.linearVelocity = new Vector2 (RandomIntX(), yPosition);

        playerBody.linearVelocity += Vector2.up * RandomForce();
        ScoreTracker();
        //playerBody.linearVelocity += Vector2.up * jumpUpwardsForce;

        //Debug.Log(xPosition + " " + yPosition); // Logs positions of player

    }
    public int RandomIntX()
    {
        //Generates a random X axis 
        return Random.Range(-5, 5);

    }
    private int RandomForce()
    {
        //Generates a random jump force number 
        return Random.Range(10, 15);
    }

    // Unity automatically calls this method every frame when the mouse is over any GameObject's collider.
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

    public void OnTriggerEnter2D(Collider2D collision)
    {

        isTouching = true;
    }

    private void ScoreTracker()
    {
        
        Debug.Log(Score);
        Score++;
    }
}   

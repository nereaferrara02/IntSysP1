using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System;

public class PlayerController : MonoBehaviour
{

    // Create public variables for player speed, and for the Text UI game objects
    public float speed;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    public GameObject loseTextObject;
    public AudioSource soundCoin;

    private float movementX;
    private float movementY;

    private Rigidbody rb;
    private int count;

    //New variables to display lose Text 
    private float startTime;
    private bool gameEnd;

    void Start()
    {
        // Assign the Rigidbody component to our private rb variable
        rb = GetComponent<Rigidbody>();


        // Set the count to zero 
        count = 0;

        SetCountText();

        // Set the text property of the Win Text UI to an empty string, making the 'You Win' (game over message) blank
        winTextObject.SetActive(false);
        loseTextObject.SetActive(false);
        startTime = Time.time;
        gameEnd = false; // Initialize gameEnd to false
        soundCoin.Stop();


    }

    void Update()
    {
        if (Time.time - startTime > 60 && !gameEnd) // Check if 60 seconds have passed
        {
            loseTextObject.SetActive(true); // Display the lose text
        }
    }


    void FixedUpdate()
    {
        // Create a Vector3 variable, and assign X and Z to feature the horizontal and vertical float variables above
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);

            if (!loseTextObject.activeSelf)
            {
                count += 1;
                soundCoin.PlayOneShot(soundCoin.clip);
            }


            SetCountText();
        }
    }

    void OnMove(InputValue value)
    {
        Vector2 v = value.Get<Vector2>();

        movementX = v.x;
        movementY = v.y;
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();

        if (count >= 14 && !loseTextObject.activeSelf)
        {
            // Set the text value of your 'winText'
            winTextObject.SetActive(true);
            gameEnd = true;
        }

     
    }
}

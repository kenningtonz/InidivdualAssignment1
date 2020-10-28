using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;

public class PlayerController : MonoBehaviour
{
    public Speed verticalSpeed;
    public float maxSpeed;
    public Boundary boundary;
    public GameController gameController;
    public Transform bulletSpawn;
    public GameObject bullet;

    // private instance variables
    private AudioSource _thunderSound;
    private AudioSource _yaySound;
    private AudioSource _bulletSound;
    private Rigidbody2D _rigidbody2D;

    private bool isFiring = false;

    //TODO: create a reference to the BulletPoolManager here
    public BulletPoolManager bulletpoolmanager;


    // Start is called before the first frame update
    void Start()
    {
        _thunderSound = gameController.audioSources[(int)SoundClip.THUNDER];
        _yaySound = gameController.audioSources[(int)SoundClip.YAY];
        _bulletSound = GetComponent<AudioSource>();
        _rigidbody2D = GetComponent<Rigidbody2D>();

        // Shoots bullet on a delay if button is pressed
        StartCoroutine(FireBullet());
    }

    // Update is called once per frame
    void Update()
    {
        // Move player
        Move();

        // Checks if shoot button is pressed
        ActionCheck();

        // Destroys bullet when it's off screen
        CheckBounds();
    }

    public void Move()
    {
        if(Input.GetAxis("Horizontal") > 0.1f)
        {
           _rigidbody2D.AddForce(new Vector2(verticalSpeed.max * Time.deltaTime, 0.0f));
        }

        if (Input.GetAxis("Horizontal") < -0.1f)
        {
            _rigidbody2D.AddForce(new Vector2(verticalSpeed.min * Time.deltaTime, 0.0f));
        }

        _rigidbody2D.velocity = Vector2.ClampMagnitude(_rigidbody2D.velocity, 5.0f);
        _rigidbody2D.velocity *= 0.95f;
    }

    private void CheckBounds()
    {
        // check right boundary
        if(transform.position.x > boundary.Right)
        {
            transform.position = new Vector2(boundary.Right, transform.position.y);
        }

        // check left boundary
        if (transform.position.x < boundary.Left)
        {
            transform.position = new Vector2(boundary.Left, transform.position.y);
        }
    }

    private void ActionCheck()
    {
        // see Edit -> Project Settings -> Input
        if (Input.GetAxis("Jump") > 0)
        {
            isFiring = true;
        }
        else
        {
            isFiring = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        switch(other.gameObject.tag)
        {
            case "Cloud":
                _thunderSound.Play();
                gameController.Lives -= 1;
                break;
            case "Island":
                _yaySound.Play();
                gameController.Score += 100;
                break;
        }
    }

    IEnumerator FireBullet()
    { 
        while (true)
        {
            // Check every 0.15 seconds if shoot button is pressed
            yield return new WaitForSeconds(0.15f);
            if (isFiring)
            {
                _bulletSound.Play();

                //TODO: this code needs to change to user the BulletPoolManager's
                //TODO: GetBullet function which will return a reference to a 
                //TODO: bullet object. 
                //TODO: Ensure you position the new bullet at the bulletSpawn position

                bulletpoolmanager.GetBullet(bulletSpawn.position);

            }

        }
    }

}

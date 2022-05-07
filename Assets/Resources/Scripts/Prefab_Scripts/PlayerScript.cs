using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    const int MENUINDEX = 0;

    public GameObject bullet;
    Rigidbody2D body;

    float screenHalfWorldUnits;
    float screenHalfWorldUnitsHeight;

    Vector2 move;
    private float acceleration = 1000;
    private float speed = 3;
    private float maxSpeed;
    private float timeStamp;

    private bool isPaused;

    GameObject pausescreen;


    void Start() {
 
        float playerHalfWidth = transform.localScale.x / 8f;
        float playerHalfHeight = transform.localScale.y;
        screenHalfWorldUnits = Camera.main.aspect * Camera.main.orthographicSize + playerHalfWidth;
        screenHalfWorldUnitsHeight = Camera.main.aspect * Camera.main.orthographicSize + playerHalfHeight;
        body = GetComponent<Rigidbody2D>();
        maxSpeed = speed * 2.5f;
        pausescreen = GameObject.Find("Pause Screen");
        pausescreen.SetActive(false);

        
    }

     void FixedUpdate() { 

        BoundaryCheck(screenHalfWorldUnits,screenHalfWorldUnitsHeight);
        movePlayer();
               
    }

    
    void Update()
    {
        move = new Vector2(Input.GetAxisRaw("Horizontal"),  Input.GetAxisRaw("Vertical"));
        if (Input.GetButtonDown("Pause")){
            isPaused = true;
            Time.timeScale = 0;
        }
        else if (Input.GetButtonDown("Resume")){
            isPaused = false;
            Time.timeScale = 1; 
        }
        pauseScreen();
        shootPin();

    }

    void pauseScreen(){
        if (isPaused){
            pausescreen.SetActive(true);
            if (Input.GetButtonDown("Quit")){
                Time.timeScale = 1;
                SceneManager.LoadSceneAsync(MENUINDEX, LoadSceneMode.Single);
            }
        }
        else {
            // canvas.SetActive(false);
            pausescreen.SetActive(false);
             
        }
    }

    void movePlayer(){
        if(body.velocity.magnitude > maxSpeed)
         {
                body.velocity = body.velocity.normalized * maxSpeed;
                // Debug.Log("Speed to Great: " + body.velocity);
         }

        body.AddForce(move * acceleration * Time.fixedDeltaTime);
    }

    void shootPin(){
        
        if (Input.GetButton("Fire1") && Time.time > timeStamp + .5){
            Instantiate(bullet, this.gameObject.transform.GetChild(0).position, Quaternion.identity);
            timeStamp = Time.time;
        }
    }


    void BoundaryCheck(float boundaryUnitx, float boundaryUnity){
        // print(boundaryUnit);
        if (transform.position.x < -boundaryUnitx)
        {
            transform.position = new Vector2(boundaryUnitx, transform.position.y);
        }
        else if (transform.position.x > boundaryUnitx)
        {
            transform.position = new Vector2(-boundaryUnitx, transform.position.y);
        }
        else if (transform.position.y > boundaryUnity){
            transform.position = new Vector2(transform.position.x, -boundaryUnity);
            
        }
        else if (transform.position.y < -boundaryUnity){
            transform.position = new Vector2(transform.position.x, boundaryUnity);
            
        }
    }
}

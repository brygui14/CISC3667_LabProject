using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class balloonController : MonoBehaviour
{

    const int MENUINDEX = 0;
    const int DIRECTIONSINDEX = 1;
    const int OPTIONSINDEX = 2;
    const int SCORESINDEX = 3;
    const int EASYINDEX = 4;
    const int MEDIUMINDEX = 5;
    const int HARDINDEX = 6;
    const int GAMEOVERINDEX = 7;
    Rigidbody2D body;
    public AudioClip audioSource;

    float screenHalfWorldUnits;
    float screenHalfWorldUnitsHeight;

    Vector2 move = new Vector2(1,0);
    private float acceleration = 1500;
    private float speed = 1;
    private float maxSpeed;
    private float scoreValue;
    private int levelIndex;

    GameObject player;
    private GameObject score;
    Rigidbody2D playerBody;

    public RectTransform fader;

    private Animator anim;

    


    void Start() {
 
        float playerHalfWidth = transform.localScale.x / 8f;
        float playerHalfHeight = transform.localScale.y;
        screenHalfWorldUnits = Camera.main.aspect * Camera.main.orthographicSize + playerHalfWidth;
        screenHalfWorldUnitsHeight = Camera.main.aspect * Camera.main.orthographicSize + playerHalfHeight;
        body = GetComponent<Rigidbody2D>();
        GetComponent<AudioSource> ().playOnAwake = false;
        GetComponent<AudioSource> ().clip = audioSource;
        maxSpeed = speed * 2.5f;
        player = GameObject.Find("Player");
        playerBody = player.GetComponent<Rigidbody2D>();

        anim = GetComponent<Animator>();

        

        fader.gameObject.SetActive(true);
        LeanTween.scale(fader, Vector3.one, 0f);
        LeanTween.scale(fader, Vector3.zero, 1f).setOnComplete(() => {
            fader.gameObject.SetActive(false);
        });

        score = GameObject.Find("Score Keeper");        

        getDifficultyLevel();


        
    }

     void FixedUpdate() { 

        BoundaryCheck(screenHalfWorldUnits,screenHalfWorldUnitsHeight, levelIndex);
        movePlayer(levelIndex);
               
    }

    private void getDifficultyLevel(){
        int index = SceneManager.GetActiveScene().buildIndex;
        if (index == 4){
            setDifficulty(0);
        }
        else if (index == 5){
            setDifficulty(1);
        }
        else{
            setDifficulty(2);
        }

    }

    private void setDifficulty(int difficulty){
        if (difficulty == 0){
            transform.localScale = new Vector3(0.3f, 0.3f, 1f);
            scoreValue = 100;
            levelIndex = 4;
        }
        else if (difficulty == 1){
            transform.localScale = new Vector3(0.2f, 0.2f, 1f);
            scoreValue = 250;
            levelIndex = 5;
        }
        else{
            transform.localScale = new Vector3(0.1f, 0.1f, 1f);
            scoreValue = 500;
            levelIndex = 6;
        }
    }

    void movePlayer(int levelIndex){

        if (levelIndex == HARDINDEX){

            Vector3 desiredVelocity = Vector3.Normalize(transform.position - player.transform.position) * maxSpeed;
            Vector3 steering = (Vector2)desiredVelocity - playerBody.velocity;

            if(body.velocity.magnitude > maxSpeed){
                body.velocity = body.velocity.normalized * maxSpeed;
                // Debug.Log("Speed to Great: " + body.velocity);
            }

            body.AddForce((Vector2)steering * acceleration * Time.fixedDeltaTime);



            // desired_velocity = normalize(target - position) * max_velocity
            // steering = desired_velocity - velocity

        }
        else{
            if(body.velocity.magnitude > maxSpeed){
                body.velocity = body.velocity.normalized * maxSpeed;
                // Debug.Log("Speed to Great: " + body.velocity);
            }
            body.AddForce(move * acceleration * Time.fixedDeltaTime);
        }


        
        
    }

    void BoundaryCheck(float boundaryUnitx, float boundaryUnity, int levelIndex){
        // print(boundaryUnit);
        if (levelIndex != HARDINDEX){
            if (transform.position.x < -boundaryUnitx){
                move.x = 1;
            }
            else if (transform.position.x > boundaryUnitx){
                move.x = -1;
            }
        }
        else{
            Debug.Log(boundaryUnity);
            if (transform.position.x < -boundaryUnitx){
                transform.position = new Vector2(boundaryUnitx, transform.position.y);
                }
            else if (transform.position.x > boundaryUnitx){
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

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Projectile"){
            GetComponent<AudioSource> ().Play(0);
            score.SendMessage("Won", scoreValue);
            move = new Vector2(0,0);
            body.velocity = move;

            anim.Play("Explosion");

        }
    }

    void destroy(){
        score.SendMessage("nextLevel", levelIndex);
        
        Destroy(gameObject);
    }
}

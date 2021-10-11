using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public GameObject mainCamera;
    private bool camReadyToMove;
    public Rigidbody2D rb;
    public Renderer rend;
    private float score = 0f;
    private float countdownToNextPoint = 6f;
    public GameObject scoreText;
    public GameObject deathScreeen;

    public Material white;
    public Material red;


    [SerializeField] bool dead;
    [SerializeField] bool isGoodToJump;
    [SerializeField] float beenOnGroundFor = 0f;
    [SerializeField] float yPosToDie = -5;



    private enum Trajectory
    {
        LeftUp,
        StraightUp,
        RightUp
    }
    Trajectory tiltTrajectory = Trajectory.StraightUp;

    private enum Lane
    {
        LeftLane,
        MiddleLane,
        RightLane
    }
    Lane inLane = Lane.MiddleLane;

    // Start is called before the first frame update
    void Start()
    {
        rend = GameObject.Find("Player_Temp_Display").GetComponent<Renderer>();
    }

    void Update()
    {
        if (!dead)
        {

            //move camera and death Y Position. if below death Y position you die
            if(rb.velocity.y > 0)
            {
                if(transform.position.y + 3 >= mainCamera.gameObject.transform.position.y)
                {
                    mainCamera.gameObject.transform.position = new Vector3(mainCamera.gameObject.transform.position.x, transform.position.y + 3, mainCamera.gameObject.transform.position.z);
                    yPosToDie = transform.position.y - 5;
                }
            } else
            {
                if(transform.position.y <= yPosToDie)
                {
                    dead = true;
                    deathScreeen.gameObject.SetActive(true);
                    Destroy(gameObject);
                }
            }













            //player input
            if (isGoodToJump)
            {
                //temp PC system
                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    tiltTrajectory = Trajectory.LeftUp;
                    //display trajectory left


                } else if (Input.GetKey(KeyCode.RightArrow))
                {
                    tiltTrajectory = Trajectory.RightUp;
                    //display trajectory right

                }
                else
                {
                    tiltTrajectory = Trajectory.StraightUp;
                    //display trajectory center

                }

                if (Input.GetKey(KeyCode.Space))
                {
                    if (tiltTrajectory.Equals(Trajectory.LeftUp))
                    {
                        //Launch up and left
                        jump(2f, 5f);

                    } else if (tiltTrajectory.Equals(Trajectory.RightUp))
                    {
                        //launch up and left
                        jump(-2f, 5f);

                    } else
                    {
                        //launch up
                        jump(0f, 5f);

                    }
                    //do something to move the camera in a certain way
                    


                }
                //temp PC input ENDS

                //touchscreen workable
                if (Input.acceleration.x > 0.2f && inLane != Lane.LeftLane)
                {
                    tiltTrajectory = Trajectory.LeftUp;
                    //display trajectory left

                }
                else if(Input.acceleration.x < -0.2f && inLane != Lane.RightLane)
                {
                    tiltTrajectory = Trajectory.RightUp;
                    //display trajectory right
                    
                }
                else
                {
                    tiltTrajectory = Trajectory.StraightUp;
                    //display trajectory center

                }


                if (Input.GetMouseButtonDown(0) && isGoodToJump)
                {
                    if (tiltTrajectory.Equals(Trajectory.LeftUp))
                    {
                        //Launch up and left
                        jump(2.9f, 24f);
                        if(inLane == Lane.RightLane)
                        {
                            inLane = Lane.MiddleLane;
                        } else
                        {
                            inLane = Lane.LeftLane;
                        }
                    }
                    else if (tiltTrajectory.Equals(Trajectory.RightUp))
                    {
                        //launch up and right
                        jump(-2.9f, 24f);
                        if(inLane == Lane.LeftLane)
                        {
                            inLane = Lane.MiddleLane;
                        } else
                        {
                            inLane = Lane.RightLane;
                        }
                    }
                    else
                    {
                        //launch up
                        jump(0f, 22f);

                    }
                }


            }






            //deal with time increase score
            countdownToNextPoint -= Time.deltaTime;
            if(countdownToNextPoint <= 0f)
            {
                countdownToNextPoint = 5f;
                updateScoreDisplay(1f);           //make sure to call this whenever you do anything else to the score too
            }
            




            
        }
    }

    public void updateScoreDisplay(float addNumber)
    {
        score += addNumber;
        Debug.Log("score is " + score);
        scoreText.GetComponent<TMP_Text>().text = score.ToString();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("platform"))
        {
            beenOnGroundFor += Time.deltaTime;
            if(beenOnGroundFor >= 0.1f)
            {
                isGoodToJump = true;
                rend.material = white;
                //show trajectory now
                if (collision.gameObject.GetComponent<platformController>().canCrumble && !collision.gameObject.GetComponent<platformController>().isCrumbling)
                {
                    collision.gameObject.GetComponent<platformController>().startCrumbling(5f);
                }
            }
        } else if (collision.gameObject.CompareTag("StarPoint"))
        {
            collision.gameObject.GetComponent<starPointSystem>().destroyMe();
        }
    }





    void jump(float modX, float modY)
    {
        isGoodToJump = false;
        rend.material = red;
        beenOnGroundFor = 0f;
        if (tiltTrajectory.Equals(Trajectory.StraightUp))
        {
            beenOnGroundFor = -0.2f;
        }
        //camReadyToMove = true;
        //temp disable Trajectory showing
        Vector2 launchVector = new Vector2(modX, modY);
        rb.AddForce(launchVector, ForceMode2D.Impulse); 
    }



}

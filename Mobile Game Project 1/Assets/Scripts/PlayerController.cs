using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject mainCamera;
    private bool camReadyToMove;
    public Rigidbody2D rb;
    private float score = 0f;
    private float countdownToNextPoint = 6f;

    [SerializeField] bool dead;
    [SerializeField] bool isGoodToJump;
    [SerializeField] float beenOnGroundFor = 0f;



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
        
    }

    void Update()
    {
        if (!dead)
        {














            //player input
            if (isGoodToJump)
            {
                //temp PC system
                /*
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
                */

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
                        jump(1.5f, 15f);
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
                        jump(-1.5f, 15f);
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
                        jump(0f, 10f);

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

    void updateScoreDisplay(float addNumber)
    {
        score += addNumber;
        Debug.Log("score is " + score);
        //display the new score
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("platform"))
        {
            beenOnGroundFor += Time.deltaTime;
            if(beenOnGroundFor >= 0.2f)
            {
                isGoodToJump = true;
                //show trajectory now
                if (camReadyToMove)
                {
                    //switch the camera from another way
                    camReadyToMove = false;
                    mainCamera.gameObject.transform.position = new Vector3(mainCamera.gameObject.transform.position.x, transform.position.y + 3, mainCamera.gameObject.transform.position.z);
                }
                if (collision.gameObject.GetComponent<platformController>().canCrumble && !collision.gameObject.GetComponent<platformController>().isCrumbling)
                {
                    collision.gameObject.GetComponent<platformController>().startCrumbling(5f);
                }
            }
        } else if (collision.gameObject.CompareTag("point"))
        {
            //access the point thing, get it to do an effect and get destroyed
            updateScoreDisplay(5f);
        }
    }





    void jump(float modX, float modY)
    {
        Debug.Log("JUMP!");
        isGoodToJump = false;
        beenOnGroundFor = 0f;
        camReadyToMove = true;
        //temp disable Trajectory showing
        Vector2 launchVector = new Vector2(modX, modY);
        //rb.AddForce(launchVector, ForceMode2D.Impulse);   //fix this when i can fix the gravity problem, in the meantime this next part

        if(modX > 0)
        {
            transform.position = new Vector3(transform.position.x + 2, transform.position.y + 5, transform.position.z);

        } else if(modX < 0)
        {
            transform.position = new Vector3(transform.position.x - 2, transform.position.y + 5, transform.position.z);

        }
        else
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + 5, transform.position.z);

        }

    }





}

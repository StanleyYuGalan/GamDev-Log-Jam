using UnityEngine;
using System.Collections;

public class PlayerMotor : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 moveVector;
    private float speed = 50.0f;
    private float verticalVelocity = 0.0f;
    private float gravity = 50.0f;

    public Animator animator;
    public GameObject youDied;
    public GameObject youWin;
    public float directionControl = 0.5f;
    private bool dead = false;
    private float initialWait = 2f;
    private float currentHoldTime = 0f;


    //Use this for installation
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    //Update is called once per frame
    void Update()
    {
        currentHoldTime += Time.deltaTime;
        if (!dead)
        {
            moveVector = Vector3.zero;

            // GRAVITY
            if (controller.isGrounded)
            {
                // JUMP
                verticalVelocity = -0.5f;
                transform.FindChild("WaterSplash").gameObject.SetActive(true);
                transform.FindChild("WaterWake").gameObject.SetActive(true);
                if (Input.GetAxisRaw("Jump") > 0.0f)
                {
                    verticalVelocity = 20.0f;
                    transform.FindChild("WaterSplash").gameObject.SetActive(false);
                    transform.FindChild("WaterWake").gameObject.SetActive(false);
                }
            }
            else
            {
                verticalVelocity -= gravity * Time.fixedDeltaTime;
            }

            moveVector.z = 0;
            moveVector.y = verticalVelocity;
            moveVector.x = 0;

            //Debug.Log("(" + moveVector.x + ", " + moveVector.y + ", " + moveVector.z + "), rotation = " + transform.rotation);
            
            // TURN  
            if (Input.GetKey(KeyCode.A)&& currentHoldTime > initialWait)
            {
                transform.position += transform.forward * 20 * Time.deltaTime;
                //Temp
                transform.Rotate(Vector3.down * 90 * Time.deltaTime);
                if (directionControl > 0)
                    directionControl -= .05f;
                //animator.SetFloat("Blend", 0);
            }
            else if (Input.GetKey(KeyCode.D) && currentHoldTime > initialWait)
            {
                transform.position += transform.forward * -20 * Time.deltaTime;
                //Temp
                transform.Rotate(Vector3.up * 90 * Time.deltaTime);
                if (directionControl < 1)
                    directionControl += .05f;
                // animator.SetFloat("Blend", 1);
            }
            else
            {
                if (directionControl < .5)
                    directionControl += .05f;
                else if (directionControl > .5)
                    directionControl -= .05f;
                //animator.SetFloat("Blend", 0.5f);
            }

            animator.SetFloat("Blend", directionControl);
            // ROTATE
            if (Input.GetKey(KeyCode.Q))
            {
                transform.Rotate(Vector3.down * 90 * Time.deltaTime);
            }
            else if (Input.GetKey(KeyCode.E))
            {
                transform.Rotate(Vector3.up * 90 * Time.deltaTime);
            }

            transform.position += transform.right * Time.deltaTime * speed;

            //moveVector = transform.rotation * moveVector;
            controller.Move(moveVector * Time.deltaTime);
        }

    }

    void OnCollisionEnter(Collision collision)
    {
        //if (hit.gameObject.tag.Equals("Obstacle"))
        //{
        //    killWinston();
        //}

        //else if (hit.gameObject.tag.Equals("Victory"))
        //{ winGame(); }

        /*if (hit.point.z > transform.position.z + controller.radius)
        {
            killWinston();
        }*/

    }

    void winGame()
    {
        youWin.SetActive(true);
        dead = true;
        Destroy(gameObject);
        Debug.Log("Winston Won");
    }

    void killWinston()
    {
        animator.SetBool("Die", true);
        //youDied.SetActive(true);
        dead = true;
        Debug.Log("Winston Died");
    }
}



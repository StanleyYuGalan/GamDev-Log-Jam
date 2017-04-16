using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerMotor : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 moveVector;
    private float speed = 20.0f;
    private float verticalVelocity = 0.0f;
    private float gravity =50.0f;



    public Animator animator;
    public GameObject youDied;
    public GameObject youWin;
    public float directionControl = 0.5f;
    private bool dead = false;
    private float initialWait = 2f;
    private float currentHoldTime = 0f;
    private float score = 0;
    public CapsuleCollider caps;
    public Image Lifebar;
    public AudioSource woodImpactSound;
    public AudioSource waterSound;
    public AudioSource windFlappingSound;
    public Text scoreBoard;
    //Use this for installation
    void Start()
    {
        controller = GetComponent<CharacterController>();
        windFlappingSound.enabled = false;
   
    }

    //Update is called once per frame
    void Update()
    {


        currentHoldTime += Time.deltaTime;
        if (!dead)
        {
            moveVector = Vector3.zero;
            score +=  Time.deltaTime * 137;
           
            scoreBoard.text = ((int)score).ToString();
            // GRAVITY
            if (controller.isGrounded)
            {
                // JUMP
                verticalVelocity = -0.5f;
                transform.FindChild("WaterSplash").gameObject.SetActive(true);
                transform.FindChild("WaterWake").gameObject.SetActive(true);
                //waterSound.loop = true;
                waterSound.enabled = true;
                windFlappingSound.enabled = false;
                if (Input.GetAxisRaw("Jump") > 0.0f)
                {
                    verticalVelocity = 15.0f;
                    transform.FindChild("WaterSplash").gameObject.SetActive(false);
                    transform.FindChild("WaterWake").gameObject.SetActive(false);
                    waterSound.enabled = false;
                    if (!windFlappingSound.isPlaying)
                        windFlappingSound.Play();
                    windFlappingSound.enabled = true;
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


       if (transform.position.y <= -9)
        {
            killBeaver();
        }
    
    }
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        
       if (hit.gameObject.tag.Equals("Terrain") || hit.gameObject.tag.Equals("Obstacle"))
        {
            woodImpactSound.Play();
            Lifebar.fillAmount -= .1f;
            
            if (Lifebar.fillAmount<=0)
                  killBeaver();
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag.Equals("Terrain") || col.gameObject.tag.Equals("Obstacle"))
        {
            if (col.gameObject.tag.Equals("Obstacle"))
            {
                Destroy(col.gameObject);

            }
            woodImpactSound.Play();
            Lifebar.fillAmount -= .1f;

            if (Lifebar.fillAmount <= 0)
                killBeaver();
        }
    }

    public void DecreaseLife()
    {
        woodImpactSound.Play();
        Lifebar.fillAmount -= .1f;

        if (Lifebar.fillAmount <= 0)
            killBeaver();
    }

    void killBeaver()
    {
        youDied.SetActive(true);
        dead = true;
        Debug.Log("Beaver Died");
    }
}



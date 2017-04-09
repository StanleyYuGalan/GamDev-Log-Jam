using UnityEngine;
using System.Collections;

public class PlayerMotor : MonoBehaviour
{
  private CharacterController controller;
  private Vector3 moveVector;
  private float speed = 15.0f;
  private float verticalVelocity = 0.0f;
  private float gravity = 12.0f;
    private float jumpLimiter = 0f;
    public Animator animator;
    public GameObject youDied;
    public GameObject youWin;
    private Transform baseRotation;
    private bool dead =false;
    float rotationLimiter = 0f;
    float minRot = 56;
    float maxRot = 100;

  //Use this for installation
  void Start ()
  {
      controller = GetComponent<CharacterController> ();
        baseRotation = transform;
          }
    //Update is called once per frame
  void Update ()
  {
        if (!dead) {
    moveVector = Vector3.zero;

    if (controller.isGrounded)
    {
      verticalVelocity = -0.5f;
    }
    else
    {
      verticalVelocity -= gravity * Time.deltaTime;
    }

    moveVector.z = Input.GetAxisRaw("Horizontal") * speed*(-1);
            moveVector.y = verticalVelocity;
            moveVector.x = speed;
           /*
            if (Input.GetKey(KeyCode.D))
            {

                transform.Rotate(transform.rotation * Vector3.right * 40 * Time.deltaTime);

            }
            else if (Input.GetKey(KeyCode.A))
            {
                transform.Rotate(transform.rotation * Vector3.left * 40 * Time.deltaTime);
            }
          */
            if (Input.GetAxisRaw("Jump") == 1 && jumpLimiter<1.2f)
            {
                moveVector = (jump(moveVector));
                jumpLimiter += Time.deltaTime;
            }
           else
            {
                animator.SetBool("Jump", false);
                if(controller.isGrounded)
                    jumpLimiter = 0;
            }
            //moveVector = transform.rotation * moveVector;
            controller.Move (moveVector * Time.deltaTime);
               }

    }
      void OnControllerColliderHit(ControllerColliderHit hit)
   {
        if (hit.gameObject.tag.Equals("Obstacle"))
        {
            killWinston();
        }
            
        else if (hit.gameObject.tag.Equals("Victory"))
        { winGame(); }
            
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
        youDied.SetActive(true);
        dead = true;
        Debug.Log("Winston Died");   
         }
    Vector3 jump(Vector3 moveVector)
    {
        animator.SetBool("Jump", true);
        moveVector.y += 10.6f;
        return moveVector;
    }
}



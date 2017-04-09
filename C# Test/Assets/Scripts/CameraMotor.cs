using UnityEngine;
using System.Collections;

public class CameraMotor : MonoBehaviour
{
  private Transform lookAt;
    
    private Vector3 startOffset;
    private float animationDuration =2f;
    private float transition = 0f;
    private Vector3 animationOffset = new Vector3(0,5,5);
  //Use this for initialzation
  void Start ()
  {
        lookAt = GameObject.FindGameObjectWithTag("Player").transform;
        startOffset = transform.position - lookAt.position;
  }

 
  //Update is called once per frame
  void Update ()
  {

    if ( lookAt!= null )
        {
            if(transition>1f)
            { 
            transform.position = Vector3.Lerp(gameObject.transform.position, lookAt.position + startOffset, 1f);
            }else
            {
            transform.position = Vector3.Lerp(gameObject.transform.position+animationOffset, lookAt.position + startOffset, transition);
                transition += Time.deltaTime * 1 / animationDuration;
            }
        }
    }

}

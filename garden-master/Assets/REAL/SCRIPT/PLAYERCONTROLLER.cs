using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLAYERCONTROLLER : MonoBehaviour
{
    public float runSpeed;
    private Rigidbody2D myRigidbody;
    private Animator myAnim;
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
        audioSource = this.transform.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Flip();
        Run();
    }

    void Flip()
    {
        bool plyerHasXAxisSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        if (plyerHasXAxisSpeed)
        {
            if (myRigidbody.velocity.x > 0.1f)
            {
                transform.localRotation = Quaternion.Euler(0, 180, 0);
            }

            if (myRigidbody.velocity.x < -0.1f)
            {
                transform.localRotation = Quaternion.Euler(0, 0, 0);
            }
        }
    }

    void Run()
    {
        float moveDir = Input.GetAxis("Horizontal");
        Vector2 playerVel = new Vector2(moveDir * runSpeed, myRigidbody.velocity.y);
        myRigidbody.velocity = playerVel;
        bool plyerHasXAxisSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        myAnim.SetBool("Walk", plyerHasXAxisSpeed);

        if (plyerHasXAxisSpeed && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
        else if(!plyerHasXAxisSpeed && audioSource.isPlaying)
        {
            audioSource.Stop();
        }

    }
}

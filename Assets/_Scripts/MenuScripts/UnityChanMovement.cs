using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityChanMovement : MonoBehaviour
{
    private Animator anim;
    public float speed = 5;
    private bool runForward=false;
    private bool goRotate = false;
    private float rotateTimer = 0;
    public Animator Button;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        StartCoroutine(MovementCor());
    }

    // Update is called once per frame
    void Update()
    {
        if (runForward)
        {
            rb.velocity = transform.forward  * speed;
        }
        if (goRotate)
        {
            transform.eulerAngles += Vector3.up * Time.deltaTime*180;
            rotateTimer += Time.deltaTime;
            if (rotateTimer >= 1f)
                goRotate = false;
        }
    }

    IEnumerator MovementCor()
    {
        yield return new WaitForSeconds(2.58f);
        transform.Rotate(new Vector3(0,180,0));
        anim.SetBool("Run",true);
        runForward = true;
        

    }
    private void OnTriggerEnter(Collider other)
    {
        Button.SetTrigger("Move");
        rb.velocity = Vector3.zero;
        runForward = false;
        goRotate = true;
        anim.SetBool("Run", false);
    }
}

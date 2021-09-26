using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 direction;
    public float forwardSpeed;
    public float maxSpeed;

    private int desiredLane = 1; // 0: sol, 1: orta, 2: right
    public float laneDistance = 4; // iki şerit arasındaki mesafe

    public float jumpForce;
    public float Gravity = -20;

    public bool isGrounded;
    public LayerMask groundLayer;
    public Transform groundCheck;

    public Animator animator;
    private bool isSliding = false;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

   
    void Update()
    {
        //baslat ekrani icin
        if (!PlayerManager.isGameStarted)
            return;

        //Increase Speed 
        if (forwardSpeed < maxSpeed)
            forwardSpeed += 0.1f * Time.deltaTime;


        animator.SetBool("isGameStarted", true);
        direction.z = forwardSpeed;

        isGrounded = Physics.CheckSphere(groundCheck.position, 0.10f, groundLayer);
        animator.SetBool("isGrounded", isGrounded);
        if (isGrounded)
        {
            if (SwipeManager.swipeUp)
                Jump();

            if (SwipeManager.swipeDown && !isSliding)
                StartCoroutine(Slide());
        }
        else
        {
            direction.y += Gravity * Time.deltaTime;
            if (SwipeManager.swipeDown && !isSliding)
            {
                StartCoroutine(Slide());
                direction.y = -8;
            }


        }

        //hangi şeritte olmamız gereken girdileri topluycaz.
        if (SwipeManager.swipeRight)
        {

            desiredLane++;
            if (desiredLane == 3)
                desiredLane = 2;

        }
        if (SwipeManager.swipeLeft)
        {

            desiredLane--;
            if (desiredLane == -1)
                desiredLane = 0;
        }
        // gelecek yerde nerede olabileceğimizi hesaplıyoruz.
        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;
        if (desiredLane == 0)
            targetPosition += Vector3.left * laneDistance;
        else if (desiredLane == 2)
            targetPosition += Vector3.right * laneDistance;



        if (transform.position == targetPosition)
            return;
        Vector3 diff = targetPosition - transform.position;
        Vector3 moveDir = diff.normalized * 25 * Time.deltaTime;
        if (moveDir.sqrMagnitude < diff.sqrMagnitude)
            controller.Move(moveDir);
        else
            controller.Move(diff);

    }
    private void FixedUpdate()
    {
        //Başlat ekranı için
        if (!PlayerManager.isGameStarted)
            return;



        controller.Move(direction * Time.fixedDeltaTime);

    }
    private void Jump()
    {

        direction.y = jumpForce;

    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {

        if (hit.transform.tag == "Obstacle")
        {

            PlayerManager.gameOver = true;
            //ses
            FindObjectOfType<AudioManager>().PlaySound("GameOver");

        }

    }
    private IEnumerator Slide()
    {
        isSliding = true;
        animator.SetBool("isSliding", true);
        controller.center = new Vector3(0, -0.5f, 0);
        controller.height = 1;
        yield return new WaitForSeconds(0.5f);

        controller.center = new Vector3(0, 0, 0);
        controller.height = 2;
        animator.SetBool("isSliding", false);
        isSliding = false;
    }
}

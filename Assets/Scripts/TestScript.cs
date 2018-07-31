using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour, IResetable
{
    private RaycastHit2D[] hits = new RaycastHit2D[1];
    private int groundedR, groundedL;
    private Transform gndChkR_s, gndChkL_s, gndChkR_e, gndChkL_e;
    public string typeOfCube = "Empty";
    private int gravity = 0;
    private bool isStuned = false;
    private bool isFacedRight = true;
    private float maxSpeed = 4f;
    public float jumpPower = 6.5f;
    public bool isGrounded = true;
    public Transform groundCheck;
    public float groundRadius = 0.2f;
    public LayerMask whatIsGround;
    public bool isConrtolBlocked;
    private Rigidbody2D body;
    private new Transform transform;
    private Vector2 startPosition;
    private GameObject levelManager;
    private Animator animator;
    private CameraShake cameraShake;
    public int isInsideOfSMT;
    public bool afterTeleporting = false;
    public bool isFalling;
    public delegate void MethodContainer(string message);
    public static event MethodContainer OnAction;

    private void Start()
    {
        cameraShake = FindObjectOfType<CameraShake>();
        isInsideOfSMT = 0;
        animator = GetComponent<Animator>();
        transform = GetComponent<Transform>();
        body = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
        levelManager = FindObjectOfType<LevelManager>().gameObject;
        animator.SetBool("Empty", true);
    }

    public void SetGravity(int gravityDirection, bool isCharacterDead)
    {
        if (gravityDirection == gravity)
            return;
        if(!isCharacterDead)
            StartCoroutine(cameraShake.Shake(.10f, .2f));
        transform.Rotate(0, 0, 90 * gravityDirection - 90 * gravity);
        transform.Find("GravityDetection").transform.Rotate(0, 0, 90 * -gravityDirection + 90 * gravity);
        gravity = gravityDirection;
    }

    public string GetTypeOfCube()
    {
        return typeOfCube;
    }

    public void SetStun(bool state)
    {
        body.gravityScale = state == false ? 1.0f : 0.0f;
        isStuned = state;
        animator.SetFloat("Speed", 0);
        body.velocity = new Vector3();
    }
    public void SetTypeOfCube(string newType)
    {
        typeOfCube = newType;
        animator.SetBool("Red", false);
        animator.SetBool("Green", false);
        animator.SetBool("Blue", false);
        animator.SetBool("Purple", false);
        animator.SetBool("Empty", false);
        animator.SetBool(newType, true);
    }

    public void Push(Vector2 direction)
    {
        OnAction("Jump");
        body.AddForce(direction, ForceMode2D.Impulse);
    }
    public int GetGravity()
    {
        return gravity;
    }
    public void Jump()
    {
        if (!isStuned)
        {

            OnAction("Jump");
            switch (gravity)
            {
                case 0:
                    body.AddForce(new Vector2(0, jumpPower), ForceMode2D.Impulse);
                    break;
                case 1:
                    body.AddForce(new Vector2(-jumpPower, 0), ForceMode2D.Impulse);
                    break;
                case 2:
                    body.AddForce(new Vector2(0, -jumpPower), ForceMode2D.Impulse);
                    break;
                case 3:
                    body.AddForce(new Vector2(jumpPower, 0), ForceMode2D.Impulse);
                    break;
            }
        }
    }
    void Awake()
    {

        gndChkR_s = gameObject.transform.Find("NewGroundCheck/gndChkR_s");
        gndChkR_e = gameObject.transform.Find("NewGroundCheck/gndChkR_e");
        gndChkL_s = gameObject.transform.Find("NewGroundCheck/gndChkL_s");
        gndChkL_e = gameObject.transform.Find("NewGroundCheck/gndChkL_e");
    }
    public void Move(float move)
    {
        if (!isStuned)
        {
            animator.SetFloat("Speed", Mathf.Abs(move));
            switch (gravity)
            {
                case 0:
                    gameObject.GetComponent<SpriteRenderer>().flipX = isFacedRight;
                    body.velocity = new Vector3(maxSpeed * move, body.velocity.y);
                    break;
                case 1:
                    gameObject.GetComponent<SpriteRenderer>().flipX = isFacedRight;
                    body.velocity = new Vector3(body.velocity.x, maxSpeed * move);
                    break;
                case 2:
                    gameObject.GetComponent<SpriteRenderer>().flipX = isFacedRight;
                    body.velocity = new Vector3(-maxSpeed * move, body.velocity.y);
                    break;
                case 3:
                    gameObject.GetComponent<SpriteRenderer>().flipX = isFacedRight;
                    body.velocity = new Vector3(body.velocity.x, -maxSpeed * move);
                    break;
            }
        }
    }

    public bool checkGround()
    {
        groundedR = Physics2D.LinecastNonAlloc(gndChkR_s.position, gndChkR_e.position, hits, (1 << LayerMask.NameToLayer("Ground")) | (1 << LayerMask.NameToLayer("PassthroughPlatform")));
        groundedL = Physics2D.LinecastNonAlloc(gndChkL_s.position, gndChkL_e.position, hits, (1 << LayerMask.NameToLayer("Ground")) | (1 << LayerMask.NameToLayer("PassthroughPlatform")));

        if ((groundedL > 0) || (groundedR > 0))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool checkFalling()
    {
        switch (gravity)
        {
            case 0:
                if (body.velocity.y > 0)
                    return false;
                return true;
            case 1:
                if (body.velocity.x < 0)
                    return false;
                return true;
            case 2:
                if (body.velocity.y < 0)
                    return false;
                return true;
            case 3:
                if (body.velocity.x > 0)
                    return false;
                return true;
        }
        return false;
    }
    void Update()
    {
        //isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        isFalling = checkFalling();
        isGrounded = checkGround();
        animator.SetBool("isGrounded", isGrounded);
        if ((Input.GetButtonDown("Jump") || Input.GetButtonDown("Vertical") )&& isGrounded)
        {
            Jump();
        }
        float move = Input.GetAxis("Horizontal");
        if (move < 0)
        {
            isFacedRight = true;
        }
        else if (move > 0)
        {
            isFacedRight = false;
        }
        Move(move);
    }


    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Danger")
        {
            OnAction("Death");
            SetStun(true);
            animator.SetBool("isDying", true);
        }
        else if (coll.gameObject.tag == "Saw")
        {
            OnAction("Death");
            SetStun(true);
            animator.SetBool("isDying", true);
        }
    }

    public void KillCharacter()
    {
       
        levelManager.GetComponent<LevelManager>().ResetLevel();
    }
    public void Teleport(bool isTelporting)
    {
        if(isTelporting)
            OnAction("Teleport");
        animator.SetBool("isTeleporting", isTelporting);
    }

    void OnDestroy()
    {
        Physics2D.gravity = new Vector3(0, -9.82f, 0);
        SetGravity(0, true);
    }

    IEnumerator ResetInsideCounter()
    {
        yield return new WaitForSeconds(0.1f);
        isInsideOfSMT = 0;
    }
    public void Reset()
    {
        StartCoroutine(ResetInsideCounter());
        animator.SetBool("isDying", false);
        SetStun(false);
        Teleport(false);
        SetTypeOfCube("Empty");
        Physics2D.gravity = new Vector3(0, -9.82f, 0);
        SetGravity(0, true);
        body.velocity = new Vector2(0, 0);
        gameObject.GetComponent<SpriteRenderer>().sprite = levelManager.GetComponent<LevelManager>().GetSprite(typeOfCube + "Char");
        transform.position = startPosition;
    }

}

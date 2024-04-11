using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public Animator animator;

    public AudioClip audioClip;
    public AudioSource audioSource;

    private new Rigidbody2D rigidbody;
    private new Collider2D collider;

    private Collider2D[] results;
    private Vector2 diraction;

    public float moveSpeed = 1f;
    public float jumpStrength = 1f;

    private bool grounded;
    private bool climbing;
    private bool stay;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
        results = new Collider2D[4];
        audioSource.volume = 0.1f;
    }

    private void CheckCollision()
    {
        grounded = false;
        climbing = false;

        Vector2 size = collider.bounds.size;
        size.y += 0.1f;
        size.x /= 2f;

        int amount = Physics2D.OverlapBoxNonAlloc(transform.position, size, 0f, results);

        for(int i = 0; i < amount; i++)
        {
            GameObject hit = results[i].gameObject;

            if(hit.layer == LayerMask.NameToLayer("Ground"))
            {
                grounded = hit.transform.position.y < (transform.position.y - 0.5f);

                Physics2D.IgnoreCollision(collider, results[i], !grounded);
            }else if(hit.layer == LayerMask.NameToLayer("Ladder"))
            {
                climbing = true;
            }
        }

        animator.SetBool("climbing", climbing);

    }

    private void Update()
    {
        CheckCollision();

        if(!climbing && !grounded)
        {
            diraction += Physics2D.gravity * Time.deltaTime;
        }

        if (grounded)
        {
            diraction.y = Mathf.Max(diraction.y, -1f);
        }

        if (diraction.x >0f)
        {
            transform.eulerAngles = Vector3.zero;
        }
        else if (diraction.x < 0f)
        {
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }

        if(diraction.x == 0 && grounded && !climbing)
        {
            animator.SetBool("stay", true);
            stay = true;
        }
        else
        {
            animator.SetBool("stay", false);
            stay = false;
        }
    }

    private void FixedUpdate()
    {
        rigidbody.MovePosition(rigidbody.position + diraction * Time.fixedDeltaTime);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Objective"))
        {
            enabled = false;
            FindAnyObjectByType<GameManager>().LevelComplete();
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            enabled = false;
            FindAnyObjectByType<GameManager>().LevelFailed();
        }
    }


    public void Jump(InputAction.CallbackContext context)
    {
        if (context.started && grounded && !climbing)
        {
            diraction.y = jumpStrength;
        }
    }


    public void WalkLeft(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            diraction.x = -moveSpeed;
        }
        else if (context.canceled && diraction.x < 0f)
        {
            diraction.x = 0f;
        }

    }

    public void WalkRight(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            diraction.x = moveSpeed;
        }
        else if (context.canceled && diraction.x > 0f)
        {
            diraction.x = 0f;
        }
    }

    public void ClimbingUp(InputAction.CallbackContext context)
    {
        if (context.started && climbing)
        {
            diraction.y = moveSpeed;
        }
        else if (context.canceled && diraction.y > 0f)
        {
            diraction.y = 0f;
        }
    }

    public void ClimbingDown(InputAction.CallbackContext context)
    {
        if (context.started && climbing)
        {
            diraction.y = -moveSpeed;
        }
        else if (context.canceled && diraction.y < 0f)
        {
            diraction.y = 0f;
        }
    }


    public void soundSmoking()
    {

        audioSource.clip = audioClip;
        audioSource.Play();
    }

}

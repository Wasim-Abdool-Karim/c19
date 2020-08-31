
using UnityEngine;
using System.Collections;
using Pathfinding;

[RequireComponent (typeof (Rigidbody2D))]
[RequireComponent (typeof (Seeker))]
public class Enemy_1 : MonoBehaviour
{
    private Animator avatarAnimation;
    private float enemy_scale;
    private bool isOnGround=false;
    public Transform target;
    public float updateRate = 2f;
    private Seeker seeker;
    private Rigidbody2D rb;

    //calculated Path
    public Path path;
    //ai speed per second (non framerate dependant)
    public float speed = 300f;
    public ForceMode2D fmode;

    [HideInInspector] //keep inspector clutter free whilst retaining accessibility
    public bool pathIsEnded = false;
    //max distance at which enemy will continue along it's path
    public float nextPointDist = 3;

    private int currentWaypoint = 0;

    //variables for jumping
    public Transform groundCheck;
    public float groundRadius;
    public LayerMask groundLayer;

    private void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        avatarAnimation = GetComponent<Animator>();
        enemy_scale = transform.localScale.x;

        if (target == null)
        {
            Debug.LogError("object not found");
            return;
        }

        seeker.StartPath ( transform.position, target.position, OnPathComplete);
        StartCoroutine(UpdatePath());
    }

    IEnumerator UpdatePath()
    {
        if (target == null)
        {
            //searching for new target could go here but this AI will only really go to one or two spots
            yield return false;
        }

        // form new path to target, return result to the OnPathComplete method
        seeker.StartPath(transform.position, target.position, OnPathComplete);

        yield return new WaitForSeconds(1f / updateRate);
        StartCoroutine(UpdatePath());
    }

    public void OnPathComplete(Path p)
    {
        Debug.Log("Error in path " + p.error);
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    void FixedUpdate()
    {
        if (target == null)
        {
            //searching for new target could go here but this AI will only really go to one spot for now
            avatarAnimation.SetFloat("Speed", Mathf.Abs(0));
            return;
        }

        //TODO: player flip based on speed


        if (path == null)
            return;

        if (currentWaypoint >= path.vectorPath.Count) //checks for end of path
        {
            if (pathIsEnded)
                return;

            Debug.Log("End of path reached.");
            pathIsEnded = true;
            return;
        }
        pathIsEnded = false;

        //Direction to the next waypoint
        Vector3 dir = (path.vectorPath[currentWaypoint] - transform.position).normalized;
        dir *= speed * Time.fixedDeltaTime;

        //animation switching
        avatarAnimation.SetFloat("Speed", Mathf.Abs(dir.x));
        isOnGround = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer);
        avatarAnimation.SetBool("onGround", isOnGround);
        //Move the AI
        
        
        // sprite orientation
        /*if (dir.x < 0f)
        {
            transform.localScale = new Vector2(enemy_scale, transform.localScale.y);
        }
        if (dir.x > 0f)
        {
            transform.localScale = new Vector2(-enemy_scale, transform.localScale.y);
        }*/
        // moving right
        if (dir.x > 0f)
        {
            rb.velocity = new Vector2(dir.x , 0);
            transform.localScale = new Vector2(-enemy_scale, transform.localScale.y);
        }
        // moving left
        if (dir.x < 0f)
        {
            rb.velocity = new Vector2(dir.x , 0);
            transform.localScale = new Vector2(enemy_scale, transform.localScale.y);
        }

        // Checking if player is trying to jump while on the ground
        if (dir.y>0 && isOnGround)
        {
            rb.velocity = new Vector2(0, 8f);
        }
        /*
        //rb.velocity = dir;
        Vector2 X=new Vector2(dir.x, 0);

        Vector2 Y=new Vector2(0, dir.y);
        rb.AddForce(X, fmode);

        if (isOnGround && dir.y > 0)
        {
            rb.AddForce(Y, fmode);
        }*/
        float dist = Vector3.Distance(transform.position, path.vectorPath[currentWaypoint]);
        if (dist < nextPointDist)
        {
            currentWaypoint++;
            return;
        }
    }
    }

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D avatar;
    private Animator avatarAnimation;
    private float horizontal_movement = 0f;
    private float jump_speed = 8f;
    public float move_speed = 5f;
    private float avatar_scale;
    // for detecting if avatar is on the onGround
    public Transform groundCheck;
    public float groundRadius;
    public LayerMask groundLayer;
    private bool isOnGround;

    // Start is called before the first frame update
    void Start()
    {
        // acquiring the avatar's rigidbody and animation components
        avatar = GetComponent<Rigidbody2D> ();
        avatarAnimation = GetComponent<Animator> ();

        // avatar scale for flipping on the x axis
        avatar_scale = transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        // Checking if player is on the ground
        isOnGround = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer);

        // updating avatar's position based on movement input detected
        horizontal_movement = Input.GetAxis("Horizontal");

        // moving right
        if(horizontal_movement > 0f){
          avatar.velocity = new Vector2(horizontal_movement * move_speed, avatar.velocity.y);
          transform.localScale = new Vector2(avatar_scale, transform.localScale.y);
        }
        // moving left
        if(horizontal_movement < 0f){
          avatar.velocity = new Vector2(horizontal_movement * move_speed, avatar.velocity.y);
          transform.localScale = new Vector2(-avatar_scale, transform.localScale.y);
        }

        // Checking if player is trying to jump while on the ground
        if(Input.GetButtonDown("Jump") && isOnGround){
          avatar.velocity = new Vector2(avatar.velocity.x, jump_speed);
        }

        // updating animation
        avatarAnimation.SetFloat("Speed", Mathf.Abs(avatar.velocity.x));
        avatarAnimation.SetBool("onGround", isOnGround);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_movement : MonoBehaviour {
    private BoxCollider2D boxCollider;
    public Vector2 moveDelta;
    private Animator animator;
    private bool isEngaged = false; //on default, not engaged cuz u dont spawn engaged with red
    
    public dialogue dialogue;

    private void Start() {
        //GetComponent gets info on the component that player_movement is attached to (so in this case, white)
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
    }

    //FixedUpdate is called at fixed time intervals, so useful with physics calcs/rigidbody
    //Update is diff cuz it is called every frame, but diff players have diff framerates based on their hardware capability, so it might give people with better hardware a better advantage
    //this means FixedUpdate is usually slower and people dont want a slower game, so update is usually used for non-physics and fixedupdate is used for physics
    private void FixedUpdate()
    {
        if (isEngaged) {
            //disable movement and animation
            moveDelta = Vector2.zero; //.zero is a zero vector, so (0, 0) since its in 2d
            animator.SetFloat("speed", 0);
            return;
            //if theres a return in fixedupdate, nothing happens until the next time interval, meaning the next time interval isnt going to immediately start
            //same goes for update, nothing will happen till the next frame
        }

        if (!dialogue.isActive()) {
            float x = Input.GetAxisRaw("Horizontal");
            moveDelta = new Vector2(x, 0);
            if (moveDelta.x > 0)
                transform.localScale = Vector3.one;
            else if (moveDelta.x < 0)
                transform.localScale = new Vector3(-1, 1, 1);

            //make BoxCast to check for collisions
            RaycastHit2D hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(moveDelta.x, 0), Mathf.Abs(moveDelta.x * Time.deltaTime), LayerMask.GetMask("Nothingness", "NPC"));

            //disable movement
            if (hit.collider != null) {
                moveDelta = Vector2.zero;
                animator.SetFloat("speed", 0);
            }
            animator.SetFloat("speed", Mathf.Abs(moveDelta.x));
        }
        else {
            moveDelta = Vector2.zero;
            animator.SetFloat("speed", 0);
        }
        
        //move the player
        Vector2 targetPosition = (Vector2)transform.position + moveDelta * 3 * Time.deltaTime;
        transform.Translate(moveDelta * 3 * Time.deltaTime);
        animator.SetFloat("speed", Mathf.Abs(moveDelta.x));
    }

    public void setEngagement(bool engaged) {
        isEngaged = engaged;
        //when engaged, can't move
        enabled = !engaged;
        //reset player movement input
        moveDelta = Vector2.zero;
        animator.SetFloat("speed", 0);
    }
}

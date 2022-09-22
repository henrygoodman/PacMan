using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacmanMovement : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector2 targetPosition;
    private double initialX, initialY;
    private float speed = 3;
    int i = 0;
    private Vector2[] positions = new Vector2[4];

    private Animator anim;
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        initialX = transform.position.x ;
        initialY = transform.position.y ;
        positions[0] = new Vector2((float)initialX + 5, (float)initialY + 0);
        positions[1] = new Vector2((float)initialX + 5, (float)initialY - 4);
        positions[2] = new Vector2((float)initialX + 0, (float)initialY - 4);
        positions[3] = new Vector2((float)initialX + 0, (float)initialY + 0);
        targetPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        var moving = (Vector2)transform.position != targetPosition;

        if (moving)
        {
            Tween();
        }
        else
        {
            anim.SetInteger("Direction", i % 4);
            targetPosition = positions[i++ % 4];
        }

    }

    private void Tween()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }
}

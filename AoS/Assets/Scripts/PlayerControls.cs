using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerPhysics))]
public class PlayerControls : MonoBehaviour
{

    /// <summary>
    /// Player Handling
    /// </summary>

    public float gravity = 20;

    public float speed = 10;
    public float acceleration = 40;
    public float jumpHeight = 12;

    public float currentSpeed;
    public float targetSpeed;


    private Vector2 amountToMove;

    private PlayerPhysics playerPhysics;

    // Use this for initialization
    void Start()
    {
        playerPhysics = GetComponent<PlayerPhysics>();
    }

    // Update is called once per frame
    void Update()
    {
        targetSpeed = Input.GetAxisRaw("Horizontal") * speed;
        currentSpeed = Towards(currentSpeed, targetSpeed, acceleration);

        //// If player is on the ground
        if (playerPhysics.grounded)
        {
            amountToMove.y = 0;
            if (Input.GetButtonDown("Jump"))
            {
                amountToMove.y = jumpHeight;
            }
        }

        amountToMove.x = currentSpeed;
        amountToMove.y -= gravity * Time.deltaTime;
        playerPhysics.Move(amountToMove * Time.deltaTime);
    }

    /// <summary>
    /// Increase n towards target by speed.
    /// </summary>
    /// <param name="n">Current speed.</param>
    /// <param name="target">The target.</param>
    /// <param name="howFastIncrementing">The speed.</param>
    /// <returns></returns>
    private float Towards(float n, float target, float howFastIncrementing)
    {
        if (n == target)
        {
            return n;
        }
        else
        {
            var dir = Mathf.Sign(target - n);
            n += howFastIncrementing * Time.deltaTime * dir;
            return (dir == Mathf.Sign(target - n)) ? n : target;
        }

    }
}

using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider))]
public class PlayerPhysics : MonoBehaviour
{

    public LayerMask collisionMask;

    private BoxCollider collider;

    private Vector3 size;
    private Vector3 centre;

    private float skin = .005f;

    Ray ray;
    RaycastHit hit;

    [HideInInspector]
    public bool grounded;

    void Start()
    {
        collider = GetComponent<BoxCollider>();
        size = collider.size;
        centre = collider.center;
    }

    /// <summary>
    /// A method to move the player by a certain amount.
    /// </summary>
    /// <param name="moveAmount">The amount to move.</param>
    public void Move(Vector2 moveAmount)
    {
        var deltaY = moveAmount.y;
        var deltaX = moveAmount.x;

        Vector2 playerPos = transform.position;

        grounded = false;
        for (var i = 0; i < 3; i++)
        {
            var dir = Mathf.Sign(deltaY);
            var x = (playerPos.x + centre.x - size.x / 2) + size.x / 2 * i;
            var y = playerPos.y + centre.y + size.y / 2 * dir;

            ray = new Ray(new Vector2(x, y), new Vector2(0, dir));

            Debug.DrawRay(ray.origin, ray.direction);

            if (Physics.Raycast(ray, out hit, Mathf.Abs(deltaY), collisionMask))
            {
                //// Distance between the player and the ground.
                var dst = Vector3.Distance(ray.origin, hit.point);

                //// Stop downward movement after meeting the collider.
                if (dst > skin)
                {
                    ////deltaY = dst * dir + skin;
                    deltaY = -dst + skin;
                }
                else
                {
                    deltaY = 0;
                }

                grounded = true;
                break;
            }
        }

        var newPostion = new Vector2(deltaX, deltaY);
        transform.Translate(newPostion);
    }
}

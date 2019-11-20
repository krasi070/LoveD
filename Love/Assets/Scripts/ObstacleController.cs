using UnityEngine;

public class ObstacleController : RaycastController
{
    public LayerMask playerMask;

    public override void Awake()
    {
        base.Awake();
    }

    public void Move(Vector3 velocity)
    {
        UpdateRaycastOrigins();

        PushPlayer(velocity);
        transform.Translate(velocity);
    }

    public void PushPlayer(Vector3 velocity)
    {
        float directionX = Mathf.Sign(velocity.x);

        if (velocity.x != 0)
        {
            float rayLength = Mathf.Abs(velocity.x) + SkinWidth;

            for (int i = 0; i < horizontalRayCount; i++)
            {
                Vector2 rayOrigin = directionX == -1 ? raycastOrigins.bottomLeft : raycastOrigins.bottomRight;
                rayOrigin += Vector2.up * horizontalRaySpacing * i;
                RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength, playerMask);

                Debug.DrawRay(rayOrigin, Vector2.right * directionX * rayLength, Color.blue);

                if (hit)
                {
                    float pushX = velocity.x - (hit.distance - SkinWidth) * directionX;

                    hit.transform.Translate(new Vector2(pushX, 0));

                    break;
                }
            }
        }
    }
}
using UnityEngine;

public class Controller2D : RaycastController
{
    public LayerMask collisionMask;
    public LayerMask powerUpsMask;

    public CollisionInfo collisions;

    private bool _powerUpActive;
    private float _powerUpTimer;
    private float _timer;

    public override void Awake()
    {
        base.Awake();
    }

    private void Update()
    {
        if (_powerUpActive)
        {
            _timer += Time.deltaTime;
            if (_timer >= _powerUpTimer)
            {
                _powerUpActive = false;
            }
            else
            {
                Move(Vector3.zero);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == powerUpsMask.value)
        {
            Destroy(collision.gameObject);
        }
    }

    public void Move(Vector3 velocity)
    {
        UpdateRaycastOrigins();
        collisions.Reset();

        if (_powerUpActive)
        {
            velocity = new Vector3(5f * Time.deltaTime, 0f, 0f);
        }

        if (velocity.x != 0)
        {
            HorizontalCollisions(ref velocity);
        }

        if (velocity.y != 0)
        {
            VerticalCollisions(ref velocity);
        }

        transform.Translate(velocity);
    }

    private void HorizontalCollisions(ref Vector3 velocity)
    {
        float directionX = Mathf.Sign(velocity.x);
        float rayLength = Mathf.Abs(velocity.x) + SkinWidth;

        for (int i = 0; i < horizontalRayCount; i++)
        {
            Vector2 rayOrigin = directionX == -1 ? raycastOrigins.bottomLeft : raycastOrigins.bottomRight;
            rayOrigin += Vector2.up * horizontalRaySpacing * i;

            HorizontalPowerUpCollision(rayOrigin, directionX, rayLength);

            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength, collisionMask);
            
            Debug.DrawRay(rayOrigin, Vector2.right * directionX * rayLength, Color.red);

            if (hit)
            {
                if (_powerUpActive)
                {
                    Debug.Log(hit.collider.gameObject.name);
                }

                velocity.x = (hit.distance - SkinWidth) * directionX;
                rayLength = hit.distance;

                collisions.right = directionX == 1;
                collisions.left = directionX == -1;
            }
        }
    }

    private void VerticalCollisions(ref Vector3 velocity)
    {
        float directionY = Mathf.Sign(velocity.y);
        float rayLength = Mathf.Abs(velocity.y) + SkinWidth;

        for (int i = 0; i < verticalRayCount; i++)
        {
            Vector2 rayOrigin = directionY == -1 ? raycastOrigins.bottomLeft : raycastOrigins.topLeft;
            rayOrigin += Vector2.right * (verticalRaySpacing * i + velocity.x);

            VerticalPowerUpCollision(rayOrigin, directionY, rayLength);

            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLength, collisionMask);
            
            Debug.DrawRay(rayOrigin, Vector2.up * directionY * rayLength, Color.red);

            if (hit)
            {
                velocity.y = (hit.distance - SkinWidth) * directionY;
                rayLength = hit.distance;

                collisions.above = directionY == 1;
                collisions.below = directionY == -1;
            }
        }
    }

    private void HorizontalPowerUpCollision(Vector2 rayOrigin, float directionX, float rayLength)
    {
        RaycastHit2D powerUpHit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength, powerUpsMask);

        if (powerUpHit)
        {
            if (powerUpHit.distance < SkinWidth)
            {
                ForwardDash(powerUpHit.collider.gameObject);
            }
        }
    }

    private void VerticalPowerUpCollision(Vector2 rayOrigin, float directionY, float rayLength)
    {
        RaycastHit2D powerUpHit = Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLength, powerUpsMask);

        if (powerUpHit)
        {
            if (powerUpHit.distance < SkinWidth)
            {
                ForwardDash(powerUpHit.collider.gameObject);
            }
        }
    }

    private void ForwardDash(GameObject powerUp)
    {
        GameObject.Find("GameMaster").GetComponent<GameMaster>().DashEffect();
        Destroy(powerUp);
        _powerUpActive = true;
        _powerUpTimer = 0.3f;
        _timer = 0f;
    }

    public struct CollisionInfo
    {
        public bool above;
        public bool right;
        public bool below;
        public bool left;

        public void Reset()
        {
            above = false;
            right = false;
            below = false;
            left = false;
        }
    }
}
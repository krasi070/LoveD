using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ObstacleQueue))]
public class Ground : MonoBehaviour
{
    public float speed;

    private const float LeftBound = -15f;
    private const float RightBound = 15f;
    private const float UpperBound = -5f;
    private const float LowerBound = -6f;

    private float _speedIncreaseTimer = 0f;
    private float _increaseSpeedEvery = 7.5f;
    private float _speedIncreaseAmount = 1.1f;
    private float _currSpeed;
    private bool _increaseSpeed;

    private ObstacleQueue _obstacleQueue;

    private void Start()
    {
        _obstacleQueue = GetComponent<ObstacleQueue>();
        InitGround();
        _currSpeed = speed;
    }

    private void Update()
    {
        if (_increaseSpeed)
        {
            IncreaseSpeed();
        }

        MoveTiles();
    }

    public void PlaceObstacles(bool placeObstacles)
    {
        if (placeObstacles)
        {
            _increaseSpeed = true;
            _obstacleQueue.PlaceObstacles();
        }
        else
        {
            foreach (Transform tile in transform)
            {
                if (tile.position.y > UpperBound)
                {
                    tile.GetComponent<Tile>().SetToDestroy();
                }

                if (tile.position.y <= UpperBound && tile.GetComponent<Tile>().IsHole)
                {
                    _obstacleQueue.CreateObstacle(tile.position);
                    Destroy(tile.gameObject);
                }
            }

            _increaseSpeed = false;
            _obstacleQueue.ResetQueue();
        }
    }

    public void ResetSpeed()
    {
        _currSpeed = speed;
    }

    public void ChangeObstacleColor(Color[] colors)
    {
        _obstacleQueue.ObstacleColor = colors[1];
        _obstacleQueue.PowerUpColor = colors[0];
        ChangeColorOfExistingObstacles(colors);
    }

    private void ChangeColorOfExistingObstacles(Color[] colors)
    {
        foreach (Transform tile in transform)
        {
            SpriteRenderer sprite = tile.GetComponent<SpriteRenderer>();
            if (sprite != null)
            {
                if (tile.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
                {
                    sprite.color = colors[1];
                }
                else if (tile.gameObject.layer == LayerMask.NameToLayer("Power Up"))
                {
                    sprite.color = colors[0];
                    tile.GetChild(0).GetChild(0).GetComponent<Text>().color = colors[0];
                }
            }
        }
    }

    private void MoveTiles()
    {
        bool queuedNewObstacleLine = false;
        Vector2 spawnPosition = Vector2.zero;

        foreach (Transform tile in transform)
        {
            Vector3 velocity = new Vector3(-_currSpeed * Time.deltaTime, 0f, 0f);

            ObstacleController controller = tile.GetComponent<ObstacleController>();
            if (controller != null)
            {
                tile.GetComponent<ObstacleController>().Move(velocity);
            }
            else
            {
                tile.Translate(velocity);
            }

            if (tile.position.x < LeftBound)
            {
                if (!queuedNewObstacleLine)
                {
                    spawnPosition = new Vector2(tile.position.x + 30, LowerBound);

                    queuedNewObstacleLine = true;
                }

                Destroy(tile.gameObject);
            }
        }

        if (queuedNewObstacleLine)
        {
            _obstacleQueue.InstantiateNextObstacleLine(spawnPosition);
        }
    }

    private void IncreaseSpeed()
    {
        _speedIncreaseTimer += Time.deltaTime;
        if (_speedIncreaseTimer > _increaseSpeedEvery && _currSpeed < 15f)
        {
            _currSpeed *= _speedIncreaseAmount;
            _speedIncreaseTimer = 0f;
        }
    }

    private void InitGround()
    {
        for (int i = (int)LeftBound; i < (int)RightBound; i++)
        {
            Vector2 position = new Vector2(i, LowerBound);
            _obstacleQueue.InstantiateNextObstacleLine(position);
        }
    }
}
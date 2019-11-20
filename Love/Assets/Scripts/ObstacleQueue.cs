using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObstacleQueue : MonoBehaviour
{
    public Transform hole;
    public Transform normalTile;
    public Transform forwardDashPowerUp;

    private bool _placeObstacles;

    public Color ObstacleColor { get; set; } = new Color32(255, 255, 255, 255);

    public Color PowerUpColor { get; set; } = new Color32(255, 143, 158, 255);

    // 0 is empty space
    // 1 is normal tile
    // 3 is forward dash power up
    // null byte[] is a hole
    private Queue<byte[]> _queuedObstacles;

    private void Awake()
    {
        ResetQueue();
    }

    public void PlaceObstacles()
    {
        _placeObstacles = true;
    }

    public void InstantiateNextObstacleLine(Vector2 startPosition)
    {
        byte[] obstacleLine = _queuedObstacles.Dequeue();

        if (obstacleLine == null)
        {
            Instantiate(hole, startPosition, Quaternion.identity, transform);
            Instantiate(hole, startPosition + Vector2.up, Quaternion.identity, transform);
        }
        else
        {
            for (int i = 0; i < obstacleLine.Length; i++)
            {
                if (i == 0 && obstacleLine[0] == 0)
                {
                    Instantiate(hole, startPosition, Quaternion.identity, transform);
                }

                if (i == 1 && obstacleLine[1] == 0)
                {
                    Instantiate(hole, startPosition + Vector2.up * i, Quaternion.identity, transform);
                }

                if (obstacleLine[i] == 1)
                {
                    Transform instance = Instantiate(normalTile, startPosition + Vector2.up * i, Quaternion.identity, transform);
                    instance.GetComponent<SpriteRenderer>().color = ObstacleColor;
                }
                else if (obstacleLine[i] == 3)
                {
                    Transform instance = Instantiate(forwardDashPowerUp, startPosition + Vector2.up * i, Quaternion.identity, transform);
                    instance.GetComponent<SpriteRenderer>().color = PowerUpColor;
                    instance.GetChild(0).GetChild(0).GetComponent<Text>().color = PowerUpColor;
                }
            }
        }

        AddNewObstacleLine();
    }

    public void CreateObstacle(Vector3 position)
    {
        Transform instance = Instantiate(normalTile, position, Quaternion.identity, transform);
        instance.GetComponent<SpriteRenderer>().color = ObstacleColor;
        instance.localScale = Vector3.zero;
        instance.GetComponent<Tile>().SetToCreate();
    }

    public void ResetQueue()
    {
        _placeObstacles = false;
        _queuedObstacles = new Queue<byte[]>();

        for (int i = 0; i < 5; i++)
        {
            _queuedObstacles.Enqueue(new byte[2] { 1, 1 });
        }
    }

    private void AddNewObstacleLine()
    {
        if (_placeObstacles && Random.Range(1, 100) >= 75)
        {
            if (Random.Range(1, 100) >= 50)
            {
                if (Random.Range(1, 100) >= 50)
                {
                    if (Random.Range(1, 100) >= 75)
                    {
                        if (Random.Range(1, 100) >= 50)
                        {
                            if (Random.Range(1, 100) >= 75)
                            {
                                _queuedObstacles.Enqueue(new byte[] { 1, 1 });
                                _queuedObstacles.Enqueue(new byte[] { 1, 1 });
                                _queuedObstacles.Enqueue(new byte[] { 1, 1 });
                                _queuedObstacles.Enqueue(new byte[] { 1, 1, 3, 1 });
                                _queuedObstacles.Enqueue(new byte[] { 1, 1 });
                                _queuedObstacles.Enqueue(new byte[] { 1, 1 });
                                _queuedObstacles.Enqueue(new byte[] { 1, 1 });
                                _queuedObstacles.Enqueue(new byte[] { 1, 1 });
                                _queuedObstacles.Enqueue(new byte[] { 1, 1 });
                            }
                            else
                            {
                                _queuedObstacles.Enqueue(new byte[] { 1, 1 });
                                _queuedObstacles.Enqueue(new byte[] { 1, 1 });
                                _queuedObstacles.Enqueue(new byte[] { 1, 1 });

                                _queuedObstacles.Enqueue(new byte[] { 1, 1, 0, 1, 1, 1, 1 });
                                _queuedObstacles.Enqueue(new byte[] { 1, 1, 0, 1, 1, 1, 1 });

                                _queuedObstacles.Enqueue(new byte[] { 1, 1, 0, 0, 0, 1, 1 });

                                _queuedObstacles.Enqueue(new byte[] { 1, 1, 1, 1, 0, 1, 1 });
                                _queuedObstacles.Enqueue(new byte[] { 1, 1, 1, 1, 3, 1, 1 });

                                _queuedObstacles.Enqueue(new byte[] { 1, 1 });
                                _queuedObstacles.Enqueue(new byte[] { 1, 1 });
                                _queuedObstacles.Enqueue(new byte[] { 1, 1 });
                            }
                        }
                        else
                        {
                            if (Random.Range(1, 100) >= 50)
                            {
                                for (int i = 0; i < 7; i++)
                                {
                                    _queuedObstacles.Enqueue(null);
                                    _queuedObstacles.Enqueue(new byte[] { 0, 0, 0, 1 });
                                }

                                _queuedObstacles.Enqueue(null);
                            }
                            else
                            {
                                if (Random.Range(1, 100) >= 75)
                                {
                                    if (Random.Range(1, 100) >= 50)
                                    {
                                        _queuedObstacles.Enqueue(new byte[] { 1, 1 });
                                        _queuedObstacles.Enqueue(new byte[] { 1, 1 });
                                        _queuedObstacles.Enqueue(new byte[] { 1, 1 });
                                        _queuedObstacles.Enqueue(new byte[] { 1, 1, 0, 1 });
                                        _queuedObstacles.Enqueue(new byte[] { 1, 1, 0, 1 });
                                        _queuedObstacles.Enqueue(new byte[] { 1, 1, 0, 1 });
                                        _queuedObstacles.Enqueue(new byte[] { 1, 1, 1, 1 });
                                    }
                                    else
                                    {
                                        _queuedObstacles.Enqueue(new byte[] { 1, 1 });
                                        _queuedObstacles.Enqueue(new byte[] { 1, 1 });
                                        _queuedObstacles.Enqueue(new byte[] { 1, 1 });
                                        _queuedObstacles.Enqueue(new byte[] { 1, 1, 0, 1 });
                                        _queuedObstacles.Enqueue(new byte[] { 1, 1, 0, 1 });
                                        _queuedObstacles.Enqueue(new byte[] { 1, 1, 0, 1 });
                                        _queuedObstacles.Enqueue(new byte[] { 1, 1, 0, 1 });
                                        _queuedObstacles.Enqueue(new byte[] { 1, 1, 1, 1, 0, 1 });
                                        _queuedObstacles.Enqueue(new byte[] { 1, 1, 0, 0, 0, 1 });
                                        _queuedObstacles.Enqueue(new byte[] { 1, 1, 0, 0, 0, 1 });
                                        _queuedObstacles.Enqueue(new byte[] { 1, 1, 1, 1, 1, 1 });
                                    }
                                }
                                else
                                {
                                    _queuedObstacles.Enqueue(new byte[] { 1, 1 });
                                    _queuedObstacles.Enqueue(new byte[] { 1, 1 });
                                    _queuedObstacles.Enqueue(new byte[] { 1, 1 });
                                    _queuedObstacles.Enqueue(new byte[] { 1, 1, 0, 1 });
                                    _queuedObstacles.Enqueue(new byte[] { 1, 1, 0, 1 });
                                    _queuedObstacles.Enqueue(new byte[] { 1, 1, 0, 1 });
                                    _queuedObstacles.Enqueue(new byte[] { 1, 1, 0, 1 });
                                    _queuedObstacles.Enqueue(new byte[] { 1, 1, 0, 1, 0, 1 });
                                    _queuedObstacles.Enqueue(new byte[] { 1, 1, 0, 1, 0, 1 });
                                    _queuedObstacles.Enqueue(new byte[] { 1, 1, 0, 1, 0, 1 });
                                    _queuedObstacles.Enqueue(new byte[] { 1, 1, 1, 1, 0, 1 });
                                    _queuedObstacles.Enqueue(new byte[] { 1, 1, 0, 0, 0, 1, 0, 1 });
                                    _queuedObstacles.Enqueue(new byte[] { 1, 1, 1, 1, 1, 1, 0, 1 });
                                    _queuedObstacles.Enqueue(new byte[] { 1, 1, 0, 0, 0, 0, 0, 1 });
                                    _queuedObstacles.Enqueue(new byte[] { 1, 1, 1, 1, 1, 1, 1, 1 });
                                }
                            }
                        }
                    }
                    else
                    {
                        _queuedObstacles.Enqueue(new byte[] { 1, 1, 1, 1, 1 });
                    }
                }
                else
                {
                    _queuedObstacles.Enqueue(new byte[] { 1, 1, 1, 1 });
                }
            }
            else
            {
                _queuedObstacles.Enqueue(new byte[] { 1, 1, 1 });
            }
        }
        else
        {
            _queuedObstacles.Enqueue(new byte[] { 1, 1 });
        }
    }
}
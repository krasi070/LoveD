using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour
{
    public Text timerText;
    public Text titleText;
    public Text instructionsText;
    public Text respawnText;

    // This is used when the player is pushed out of the screen from the left
    public Text foundLoveText;

    // This is used when the player falls down a hole
    public Text foundDeathText;

    public CameraZoom mainCamera;

    public Transform loveBox;
    public Transform ground;

    private float _timer;
    private bool _inGame;
    private bool _zoomingIn;
    private bool _zoomingOut;
    private int _colorIndex;

    private List<Color[]> _colors;

    private Transform _loveBoxInstance;
    private Transform _groundInstance;

    private void Start()
    {
        InitColors();
        InstantiateGround();
        SpawnPlayer(new Vector2(-1f, -4f));
    }

    private void Update()
    {
        if (_loveBoxInstance != null)
        {
            if (_inGame)
            {
                _timer += Time.deltaTime;
                timerText.text = _timer.ToString("0.00").Replace(',', '.');

                if (_loveBoxInstance.position.x < -12f || _loveBoxInstance.position.y < -8f)
                {
                    if (_loveBoxInstance.position.x < -12f)
                    {
                        foundLoveText.gameObject.SetActive(true);
                    }
                    else
                    {
                        foundDeathText.gameObject.SetActive(true);
                    }

                    DestroyPlayer();
                    _groundInstance.GetComponent<Ground>().PlaceObstacles(false);
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
                {
                    _inGame = true;
                    titleText.gameObject.SetActive(false);
                    instructionsText.gameObject.SetActive(false);
                    timerText.gameObject.SetActive(true);
                    _groundInstance.GetComponent<Ground>().PlaceObstacles(true);
                }
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                SpawnPlayer(new Vector2(-1f, 8f));
                _groundInstance.GetComponent<Ground>().PlaceObstacles(true);
            }
        }

        if (_zoomingIn)
        {
            _zoomingIn = mainCamera.ZoomIn();
            if (!_zoomingIn)
            {
                NextColor();
                _zoomingOut = true;
            }
        }

        if (_zoomingOut)
        {
            _zoomingOut = mainCamera.ZoomOut();
        }
    }

    public void DashEffect()
    {
        _zoomingIn = mainCamera.ZoomIn();
    }

    public void InstantiateGround()
    {
        _groundInstance = Instantiate(ground, new Vector3(0f, -3.5f, 0f), Quaternion.identity);
    }

    public void NextColor()
    {
        _colorIndex = (_colorIndex + 1) % _colors.Count;

        _loveBoxInstance.GetComponent<SpriteRenderer>().color = _colors[_colorIndex][0];
        titleText.color = _colors[_colorIndex][0];
        foundLoveText.color = _colors[_colorIndex][0];
        foundDeathText.color = _colors[_colorIndex][0];

        timerText.color = _colors[_colorIndex][1];
        instructionsText.color = _colors[_colorIndex][1];
        respawnText.color = _colors[_colorIndex][1];
        _groundInstance.GetComponent<Ground>().ChangeObstacleColor(_colors[_colorIndex]);

        mainCamera.gameObject.GetComponent<Camera>().backgroundColor = _colors[_colorIndex][2];
    }

    private void SpawnPlayer(Vector2 position)
    {
        _timer = 0f;
        foundLoveText.gameObject.SetActive(false);
        foundDeathText.gameObject.SetActive(false);
        respawnText.gameObject.SetActive(false);
        _groundInstance.GetComponent<Ground>().ResetSpeed();
        _loveBoxInstance = Instantiate(loveBox, position, Quaternion.identity);
        _loveBoxInstance.GetComponent<SpriteRenderer>().color = _colors[_colorIndex][0];
    }

    private void DestroyPlayer()
    {
        Destroy(_loveBoxInstance.gameObject);
        respawnText.gameObject.SetActive(true);
    }

    private void InitColors()
    {
        _colors = new List<Color[]>();

        _colors.Add(new Color[]
        {
            new Color32(255, 143, 158, 255),
            new Color32(255, 255, 255, 255),
            new Color32(229, 195, 224, 255)
        });

        _colors.Add(new Color[]
        {
            new Color32(241,229,35, 255),
            new Color32(170,38,218, 255),
            new Color32(195,85,245, 255)
        });

        _colors.Add(new Color[]
        {
            new Color32(24,139,192, 255),
            new Color32(203,94,60, 255),
            new Color32(228,202,113, 255)
        });

        _colors.Add(new Color[]
        {
            new Color32(255,215,193, 255),
            new Color32(120,43,0, 255),
            new Color32(255,123,50, 255)
        });

        _colors.Add(new Color[]
        {
            new Color32(233,234,227, 255),
            new Color32(125,138,117, 255),
            new Color32(209,205,180, 255)
        });

        _colors.Add(new Color[]
        {
            new Color32(255,219,68, 255),
            new Color32(170,227,171, 255),
            new Color32(74,135,72, 255)
        });

        _colors.Add(new Color[]
        {
            new Color32(226, 124, 0, 255),
            new Color32(0, 129, 199, 255),
            new Color32(0, 55, 99, 255)
        });

        _colors.Add(new Color[]
        {
            new Color32(120, 0, 0, 255),
            new Color32(41, 41, 41, 255),
            new Color32(231, 232, 234, 255)
        });
    }
}
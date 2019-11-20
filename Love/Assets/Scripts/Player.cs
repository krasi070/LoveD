using UnityEngine;

[RequireComponent(typeof(Controller2D))]
public class Player : MonoBehaviour
{
    public float maxJumpHeight = 4f;
    public float minJumpHeight = 1f;
    public float timeToJumpApex = 0.4f;

    private float _gravity;
    private float _maxJumpVelocity;
    private float _minJumpVelocity;
    private Vector3 _velocity;

    private Controller2D _controller;

    private void Start()
    {
        _controller = GetComponent<Controller2D>();

        _gravity = -(maxJumpHeight * 2 / Mathf.Pow(timeToJumpApex, 2));
        _maxJumpVelocity = Mathf.Abs(_gravity) * timeToJumpApex;
        _minJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(_gravity) * minJumpHeight);
    }

    private void Update()
    {
        if (_controller.collisions.above || _controller.collisions.below)
        {
            _velocity.y = 0;
        }

        CheckForJumpInput();
        CheckForDownwardStompInput();

        _controller.Move(_velocity * Time.deltaTime);
    }

    private void CheckForJumpInput()
    {
        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && _controller.collisions.below)
        {
            _velocity.y = _maxJumpVelocity;
        }

        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow))
        {
            if (_velocity.y > _minJumpVelocity)
            {
                _velocity.y = _minJumpVelocity;
            }
        }
    }

    private void CheckForDownwardStompInput()
    {
        if (_velocity.y < 0 && !_controller.collisions.below)
        {
            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                _velocity.y += 10f * _gravity * Time.deltaTime;
            }
            else
            {
                _velocity.y += 1.8f * _gravity * Time.deltaTime;
            }
        }
        else
        {
            if (!_controller.collisions.below && (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)))
            {
                _velocity.y += 10f * _gravity * Time.deltaTime;
            }
            else
            {
                _velocity.y += _gravity * Time.deltaTime;
            }
        }
    }
}
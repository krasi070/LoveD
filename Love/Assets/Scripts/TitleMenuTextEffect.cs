using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class TitleMenuTextEffect : MonoBehaviour
{
    public float speed = 2f;
    public float time = 1f;

    private int _directionY = 1;
    private float _timer;

    private RectTransform _rectTransform;

    private void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (_timer < time)
        {
            Vector2 translateAmount = new Vector2(0f, speed * Time.deltaTime * _directionY);
            _rectTransform.anchoredPosition += translateAmount;

            _timer += Time.deltaTime;
        }
        else
        {
            _directionY = -_directionY;
            _timer = 0f;
        }
    }
}
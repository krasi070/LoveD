using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class TextOpacityEffect : MonoBehaviour
{
    public float timeToReachMaxOpacity = 2f;

    private float _timer = 0f;
    private float _sign = 1f;
    private float _opacity = 0.2f;
    private float _opacityChangeAmount;

    private Text _textField;

    private void Start()
    {
        _textField = GetComponent<Text>();

        _opacityChangeAmount = 0.8f / timeToReachMaxOpacity;
    }

    private void Update()
    {
        if (_timer < timeToReachMaxOpacity)
        {
            _timer += Time.deltaTime;
        }
        else
        {
            _sign = -_sign;
            _timer = 0f;
        }

        _opacity = Mathf.Clamp(_opacity + _sign * _opacityChangeAmount * Time.deltaTime, 0.2f, 1f);
        _textField.color = new Color(_textField.color.r, _textField.color.g, _textField.color.b, _opacity);
    }
}
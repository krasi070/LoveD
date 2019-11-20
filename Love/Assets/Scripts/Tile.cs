using UnityEngine;

public class Tile : MonoBehaviour
{
    private bool _creating;
    private bool _destroying;
    private float _amount = 2f;

    public bool IsHole { get { return transform.GetComponent<BoxCollider2D>() == null; } }

    private void Update()
    {
        if (_creating)
        {
            if (transform.localScale.Equals(Vector3.one))
            {
                _creating = false;
            }
            else
            {
                float increaseBy = Mathf.Clamp(_amount * Time.deltaTime, 0f, 1f - transform.localScale.x);
                transform.localScale += new Vector3(increaseBy, increaseBy, increaseBy);
            }
        }

        if (_destroying)
        {
            if (transform.localScale.x <= 0f)
            {
                Destroy(gameObject);
            }

            float reduceBy = _amount * Time.deltaTime;
            transform.localScale -= new Vector3(reduceBy, reduceBy, reduceBy);
        }
    }

    public void SetToCreate()
    {
        _creating = true;
    }

    public void SetToDestroy()
    {
        _destroying = true;
    }
}
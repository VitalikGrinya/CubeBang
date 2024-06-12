using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Renderer))]
public class Cube : MonoBehaviour
{
    [SerializeField] private int _chanceSeparate = 100;

    private Rigidbody _rigidbody;
    private Renderer _renderer;

    public event Action<Cube> Touched;

    public int ChanceSeparate => _chanceSeparate;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _renderer = GetComponent<Renderer>();
    }

    public void Destroy()
    {
        Touched?.Invoke(this);
        Destroy(gameObject);
    }

    public void Initialize(int scale, int chance)
    {
        transform.localScale /= scale;
        _chanceSeparate /= chance;
        _renderer.material.color = UnityEngine.Random.ColorHSV();
    }

    public void AddForce(float force, float distance = 1)
    {
        float minDistance = 0.1f;
        float minValue = 2f;
        float maxValue = 4f;

        if (distance < minDistance)
            distance = minDistance;

        force /= distance;

        Vector3 direction = new Vector3(
                force * RandomizeValue(minValue, maxValue),
                force * RandomizeValue(minValue, maxValue),
                force * RandomizeValue(minValue, maxValue));

        _rigidbody.AddForce(direction);
    }

    private float RandomizeValue(float minValue, float maxValue) => UnityEngine.Random.Range(minValue, maxValue);
}

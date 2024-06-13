using System.Collections.Generic;
using UnityEngine;

public class Bang : MonoBehaviour
{
    [SerializeField] private float _baseRadiusOverlap = 1f;
    [SerializeField] private float _power = 5f;
    [SerializeField]private Generator _generation;

    private void OnEnable()
    {
        _generation.Separated += Explode;
        _generation.Destroyed += Explode;
    }

    private void OnDisable()
    {
        _generation.Separated -= Explode;
        _generation.Destroyed -= Explode;
    }

    private void Explode(List<Cube> cubes)
    {
        foreach (Cube cube in cubes)
        {
            cube.AddForce(_power);
        }
    }

    private void Explode(Cube currentCube)
    {
        float scale = currentCube.transform.position.x;
        float power = _power / scale;
        float radius = _baseRadiusOverlap / scale;

        Collider[] colliders = Physics.OverlapSphere(currentCube.transform.position, radius);

        foreach (var collider in colliders)
        {
            if (collider.gameObject.TryGetComponent(out Cube cube))
            {
                float distance = Vector3.Distance(currentCube.transform.position, cube.transform.position);
                cube.AddForce(power, distance);
            }
        }
    }
}

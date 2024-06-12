using Palmmedia.ReportGenerator.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bang : MonoBehaviour
{
    
    [SerializeField] private float _baseRadiusOverlap = 1f;
    [SerializeField] private float _power = 5f;
    [SerializeField] private Generate _generate;

    private void OnEnable()
    {
        _generate.Separated += Explode;
    }

    private void OnDisable()
    {
        _generate.Separated -= Explode;
    }

    private void Explode(List<Cube> cubes)
    {
        foreach (Cube cube in cubes)
        {
            cube.AddForce(_power);
        }
    }
}

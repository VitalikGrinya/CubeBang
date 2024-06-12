using System;
using System.Collections.Generic;
using UnityEngine;

public class Generate : MonoBehaviour
{
    [SerializeField] private int _minCreateValue = 2;
    [SerializeField] private int _maxCreateValue = 6;
    [SerializeField] private int _minChance = 0;
    [SerializeField] private int _maxChance = 100;
    [SerializeField] private List<Cube> _cubes;

    private int _dividerScale = 2;
    private int _dividerChance = 2;

    public event Action<List<Cube>> Separated;
    public event Action<Cube> Destroyed;

    private void OnEnable()
    {
        foreach (var cube in _cubes)
            cube.Touched += Spawn;
    }

    private void OnDisable()
    {
        foreach (var cube in _cubes)
            cube.Touched -= Spawn;
    }

    private void Spawn(Cube cube)
    {
        List<Cube> createdCubes = new List<Cube>();

        if (cube.ChanceSeparate >= UnityEngine.Random.Range(_minChance, _maxChance))
        {
            int countSpawn = RandomizeCount();

            for (int i = _minCreateValue; i <= countSpawn; i++)
            {
                createdCubes.Add(InstantiateCubes(cube));
            }

            _cubes.Remove(cube);
            cube.Touched -= Spawn;
            Separated?.Invoke(createdCubes);
        }
        else
            Destroyed?.Invoke(cube);
    }

    private Cube InstantiateCubes(Cube cube)
    {
        Cube newCub = Instantiate(cube);
        newCub.Initialize(_dividerScale, _dividerChance);
        newCub.Touched += Spawn;

        return newCub;
    }

    private int RandomizeCount() => UnityEngine.Random.Range(_minCreateValue, _maxCreateValue);
}
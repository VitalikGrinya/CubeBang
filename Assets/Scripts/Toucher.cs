using UnityEngine;

public class Toucher : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Ray _ray;

    private void Update()
    {
        if(Input.GetMouseButtonUp(0))
            Touch();
    }

    private void Touch()
    {
        _ray = _camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(_ray, out hit, Mathf.Infinity))
        {
            if (hit.collider.TryGetComponent(out Cube cube))
                cube.Destroy();
        }
    }
}

using UnityEngine;

public class Planet : MonoBehaviour
{
    [SerializeField] private float _gravity;

    private Transform _planetTransform;
    private float _speed = 25;
    
    private Cylinder _cylinder;
    private void Awake()
    {
        _planetTransform = GetComponent<Transform>();
    }
    private void Update()
    {
        _planetTransform.Rotate(0, 1 * _speed * Time.deltaTime, 0);
    }
    private void OnTriggerEnter(Collider other)
    {
        _cylinder = other.gameObject.GetComponent<Cylinder>();
        _cylinder.SetGravity(true, _gravity);
        _cylinder.SetVisitedPlanet(_planetTransform);
    }
    private void OnTriggerExit(Collider other)
    {
        _cylinder.SetGravity(false, 0);
        _cylinder.SetVisitedPlanet(null);
    }
}

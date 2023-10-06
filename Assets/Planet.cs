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
        _cylinder.SetVisitedPlanet(_planetTransform, _gravity);
    }
    //private void OnTriggerExit(Collider other)
    //{
        //_cylinder.SetVisitedPlanet(null, 0);
    //}
}

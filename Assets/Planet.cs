using UnityEngine;

public class Planet : MonoBehaviour
{
    [SerializeField] private float _gravity;

    private Transform _planetTransform;
    
    private Cylinder _cylinder;
    private void Awake()
    {
        _planetTransform = GetComponent<Transform>();
    }
    private void OnTriggerEnter(Collider other)
    {
        _cylinder = other.gameObject.GetComponent<Cylinder>();
        _cylinder.SetPlanetGravity(_gravity);
        _cylinder.SetVisitedPlanet(_planetTransform);
       
    }
    private void OnTriggerStay(Collider other)
    {
        Gravity(_cylinder);
    }
    private void OnTriggerExit(Collider other)
    {
        _cylinder.SetVisitedPlanet(null);
        _cylinder.SetPlanetGravity(0);
    }
    private void Gravity(Cylinder character)
    {
        if (character != null)
        {
            var heading = transform.position - character.transform.position;
            var distance = heading.magnitude;
            var direction = heading / distance;

            character.SetGravity(direction);
        }
        else character.SetGravity(new Vector3(0,0,0));
    }
}

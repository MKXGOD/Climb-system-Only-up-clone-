using UnityEngine;      
using static UnityEngine.GraphicsBuffer;


public class Cylinder : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private Transform _visitedPlanet;

    private Quaternion rotationRef;
    private Vector3 input;

    [SerializeField] private float _speed;

    private float _gravity;
    private float _jumpHeight = 2f;

    private bool _inPlanet;
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        if (_inPlanet)
            InputCharacter();
    }
    private void FixedUpdate()
    {
        Angel();
        Move();   
    }
    private void InputCharacter()
    {
        float _xInputDirection = Input.GetAxis("Horizontal");
        float _zInputDirection = Input.GetAxis("Vertical");

        input = new Vector3(_xInputDirection, 0, _zInputDirection);

        if (Input.GetKeyDown(KeyCode.Space))
            Jump();
    }

    private void Move()
    {
        _rigidbody.MovePosition(transform.position + (transform.forward * input.z + transform.right * input.x) * Time.fixedDeltaTime * _speed);
    }
    public void SetGravity(Vector3 direction)
    {
        Physics.gravity = new Vector3(direction.x, direction.y, direction.z);
    }
    private void Jump()
    {
        float jumpForce = Mathf.Sqrt(_jumpHeight * -2 * _gravity);
        _rigidbody.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
    private void Angel()
    {
        RaycastHit hit;
        rotationRef = Quaternion.Euler(0,0,0);

        if (Physics.Raycast(transform.position, -transform.up, out hit))
        {
            rotationRef = Quaternion.Lerp(transform.rotation, Quaternion.FromToRotation(Vector3.up, hit.normal), 20 * Time.fixedDeltaTime);
            transform.rotation = Quaternion.Euler(rotationRef.eulerAngles.x, rotationRef.eulerAngles.y, rotationRef.eulerAngles.z);
        }
    }
    
    public void SetVisitedPlanet(Transform planet)
    { 
        _visitedPlanet = planet;
        gameObject.transform.SetParent(planet, true);

        
    }
    public void SetPlanetGravity(float gravityValue)
    {
        _gravity = gravityValue;

        if (_gravity != 0)
        {
            _inPlanet = true;
            _rigidbody.freezeRotation = true;
            _rigidbody.useGravity = true;
            transform.rotation = Quaternion.LookRotation(_visitedPlanet.transform.position);

        }
        else
        {
            _rigidbody.useGravity = false;
            _inPlanet = false;
            _rigidbody.freezeRotation = false;
        }

    }
    
}

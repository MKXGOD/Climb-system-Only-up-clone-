using UnityEngine;


public class Cylinder : MonoBehaviour
{
    public Vector3 input;
    private Rigidbody _rigidbody;

    public Transform _visitedPlanet;

    Quaternion rotationRef;

    [SerializeField]private float _speed;
    [SerializeField]private float _gravity;
    private float _jumpHeight = 2f;
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            Jump();
    }
    void FixedUpdate()
    {
        Angel();
        Gravity();
        float _xInputDirection = Input.GetAxis("Horizontal");
        float _zInputDirection = Input.GetAxis("Vertical");

        input = new Vector3(_xInputDirection, 0, _zInputDirection);

        _rigidbody.MovePosition(transform.position + (transform.forward * input.z + transform.right * input.x) * Time.fixedDeltaTime * _speed);
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
    private void Jump()
    {
        float jumpForce = Mathf.Sqrt(_jumpHeight * -2 * _gravity);
        _rigidbody.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
    public void SetVisitedPlanet(Transform planet, float gravityValue)
    { 
        _visitedPlanet = planet;
        gameObject.transform.SetParent(_visitedPlanet, true);

        _gravity = gravityValue;
    }
    private void Gravity()
    {
        Physics.gravity = -transform.up;
    }
}

using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField]private Animator _animator;
    [SerializeField]private CharacterController _characterController;

    #region Movement
    [SerializeField] private float _speed = 5f;
    private Vector3 _moveCharacter;
    #endregion
    #region Gravity
    [SerializeField] private float _jumpHeight = 3f;
    private float _velocity;
    private float _gravity = -9.8f;
    #endregion
    #region Ground Checker
    private bool _isGrounded;
    #endregion
    #region Climb Checker
    [SerializeField] private Transform _climbCheckPivotHead;
    [SerializeField] private Transform _climbCheckPivotChest;
    [SerializeField] private Transform _climbCheckPivotLegs;
    [SerializeField] private LayerMask _objectLayer;
    #endregion
    private bool _isClimbing;
    private bool _isGravity = true;

    private void Update()
    {
        _moveCharacter = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        _moveCharacter = transform.TransformDirection(_moveCharacter);


        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
            Jump();
        if (Input.GetKeyDown(KeyCode.G))
        {
            _animator.SetBool("_isClimb", true);
        }
        else if (Input.GetKeyDown(KeyCode.V))
        {
            _animator.SetBool("_isClimb", false);
        }

        _animator.SetFloat("VectorX", Input.GetAxis("Horizontal"), 0.1f, Time.deltaTime);
        _animator.SetFloat("VectorZ", Input.GetAxis("Vertical"), 0.1f, Time.deltaTime);
    }
    private void FixedUpdate()
    {
        _isGrounded = IsOnTheGround();

        if (_isGrounded && _velocity < 0.1)
        {
            _velocity = -3;
            _animator.SetBool("_isFall", false);
        }
        else _animator.SetBool("_isFall", true);

        if(!_isClimbing)
        Move(_moveCharacter);
        TakeHighestPoint();
        Climbing();
        DoGravity();
    }
    private void Move(Vector3 direction)
    {
        _characterController.Move(direction * _speed * Time.deltaTime);
    }
    private void Jump()
    {
       _velocity = Mathf.Sqrt(_jumpHeight * -2 * _gravity);
       _animator.SetTrigger("_isJump");
    }
    private void TakeHighestPoint()
    {
        bool headResult = Physics.Raycast(_climbCheckPivotHead.position, transform.TransformDirection(Vector3.forward), .6f, _objectLayer);
        bool chestResult = Physics.Raycast(_climbCheckPivotChest.position, transform.TransformDirection(Vector3.forward), .6f, _objectLayer);
        bool legsResult = Physics.Raycast(_climbCheckPivotLegs.position, transform.TransformDirection(Vector3.forward), .6f, _objectLayer);

        if (!headResult && chestResult)
        {
            _isClimbing = true;
            
        }
        else if (!legsResult)
        {
            _isClimbing = false;
            
        }
    }
    private void DoGravity()
    {
        if (_isGravity)
        {
            _velocity += _gravity * Time.fixedDeltaTime;
            _characterController.Move(Vector3.up * _velocity * Time.fixedDeltaTime);
        }
    }
    private void Climbing()
    {
        if (_isClimbing)
        {
            _animator.SetBool("_isClimb", true);
            _velocity = 0f;
            _velocity = 2f;
        }
        else if (!_isClimbing)
        {
            _animator.SetBool("_isClimb", false);
        }

    }
  
    private bool IsOnTheGround()
    {
        float groundStayDistance = .1f;
        bool result = Physics.Raycast(transform.position, Vector3.down, out RaycastHit floorRatycastHit, groundStayDistance);
        return result;
    }
}

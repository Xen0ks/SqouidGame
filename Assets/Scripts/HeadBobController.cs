using UnityEngine;

public class HeadBobController : MonoBehaviour
{
    [SerializeField] private bool _enable = true;

    [Range(0, 0.5f)] public float _amplitude = 0.015f;
    [Range(0, 30)] public float _frequency = 10.0f;

    public Transform _camera = null;
    public Transform _cameraHolder = null;

    float _toggleSpeed = 0.00001f;
    Vector3 _startPos;
    CharacterController _controller;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _startPos = _camera.localPosition;
    }

    private void Update()
    {
        if (!_enable || !GetComponent<MovementBehaviour>().enabled) return;

        CheckMotion();
        ResetPosition();
        _camera.LookAt(FocusTarget());
    }

    void CheckMotion()
    {
        ResetPosition();
        float speed = new Vector3(_controller.velocity.x, 0, _controller.velocity.y).magnitude;

        if (speed < _toggleSpeed) return;
        if (!_controller.isGrounded) return;

        PlayMotion(FootStepMotion());
    }

    Vector3 FootStepMotion()
    {
        Vector3 pos = Vector3.zero;
        pos.y += Mathf.Sin(Time.time * _frequency) * _amplitude;
        pos.x += Mathf.Cos(Time.time * _frequency / 2) * _amplitude * 2;
        return pos;
    }

    void ResetPosition()
    {
        if (_camera.localPosition == _startPos) return;
        _camera.localPosition = Vector3.Lerp(_camera.localPosition, _startPos, 1 * Time.deltaTime);
    }

    Vector3 FocusTarget()
    {
        Vector3 pos = new Vector3(transform.position.x, transform.position.y + _cameraHolder.localPosition.y, transform.position.z);
        pos += _cameraHolder.forward * 15.0f;
        return pos;
    }



    void PlayMotion(Vector3 motion)
    {
        _camera.localPosition += motion;
    }




}
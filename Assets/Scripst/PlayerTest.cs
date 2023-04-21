using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerTest : MonoBehaviour
{
    [SerializeField] private float walkSpeed;
    [SerializeField] private float lookSensitivity;
    [SerializeField] private float cameraRotationLimit;
    private float currentCameraRotationX = 0;
    [SerializeField] private Camera theCamera;
    private Rigidbody MyRigid;
    // Start is called before the first frame update


    void Start()
    {
        theCamera = FindObjectOfType<Camera>();
        MyRigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        CharacterRotation();
        CameraRotation();
    }

    public void Move()
    {
        float _moveDirx = Input.GetAxisRaw("Horizontal");
        float _moveDirz = Input.GetAxisRaw("Vertical");

        Vector3 _moveHorizontal = transform.right * _moveDirx;
        Vector3 _moveVertical = transform.forward * _moveDirz;

        Vector3 _velocity = (_moveHorizontal + _moveVertical).normalized * walkSpeed;

        MyRigid.MovePosition(transform.position + _velocity * Time.deltaTime);


    }
    public void CharacterRotation()
    {
        float _yRotation = Input.GetAxisRaw("Mouse X");
        Vector3 _characterRotationY = new Vector3(0f, _yRotation, 0f) * lookSensitivity;
        MyRigid.MoveRotation(MyRigid.rotation * Quaternion.Euler(_characterRotationY));
        Debug.Log(MyRigid.rotation);
        Debug.Log(MyRigid.rotation.eulerAngles);
    }

    public void CameraRotation()
    {
        float _xRotation = Input.GetAxisRaw("Mouse Y");
        float _cameraRotationX = _xRotation * lookSensitivity;
        currentCameraRotationX -= _cameraRotationX;
        currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, _cameraRotationX, cameraRotationLimit);

        theCamera.transform.localEulerAngles = new Vector3(currentCameraRotationX, 0f, 0f);
    }
}

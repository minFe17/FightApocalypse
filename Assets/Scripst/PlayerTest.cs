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
       
       MyRigid= GetComponent<Rigidbody>();  
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        CameraRotation();
    }
    private void Move()
    {
        float _moveDirX = Input.GetAxisRaw("Horizontal");
        float _moveDirZ = Input.GetAxisRaw("Vertical");
        Vector3 _moveHorizontal = transform.right* _moveDirX;   
        Vector3 _moveVertical = transform.forward * _moveDirZ;

        Vector3 _velocity = (_moveHorizontal + _moveVertical).normalized *walkSpeed;

        MyRigid.MovePosition(transform.position+_velocity*Time.deltaTime);
    }
    private void CameraRotation()
    {
        Debug.Log("Ä«¸Þ¶ó");
        float _xRotation = Input.GetAxisRaw("Mouse Y");
        float _cameraRotarionX = _xRotation * lookSensitivity;
       currentCameraRotationX += _cameraRotarionX;
        currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -cameraRotationLimit, cameraRotationLimit);

        theCamera.transform.localEulerAngles = new Vector3(currentCameraRotationX,0f,0f);   
    }
}


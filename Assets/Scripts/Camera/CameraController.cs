using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float cameraMoveSpeed = 5f;
    [SerializeField] private float cameraRotationQESpeed = 4f;
    [SerializeField] private float cameraRotationRightMouseSpeed = 200f;
    [SerializeField] private float zoomSpeed = 5f;
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    [SerializeField] private const float MIN_Y_OFFSET = 2f;
    [SerializeField] private const float MAX_Y_OFFSET = 12f;

    private float zoomAmount = 1f;
    private CinemachineTransposer cinemachineTransposer;
    private Vector3 targetFollowOffset;
    private void Awake()
    {
        cinemachineTransposer = virtualCamera.GetCinemachineComponent<CinemachineTransposer>();
    }

    private void Start()
    {
        targetFollowOffset = cinemachineTransposer.m_FollowOffset;
    }

  
    void Update()
    {
        HandleCameraMovement();

        HandleCameraRotation();

        HandleCameraZoom();
    }

    private void HandleCameraZoom()
    {
        if (Input.mouseScrollDelta.y > 0)
        {
            targetFollowOffset.y -= zoomAmount;

        }
        if (Input.mouseScrollDelta.y < 0)
        {
            targetFollowOffset.y += zoomAmount;
        }
        targetFollowOffset.y = Mathf.Clamp(targetFollowOffset.y, MIN_Y_OFFSET, MAX_Y_OFFSET);
        cinemachineTransposer.m_FollowOffset = Vector3.Lerp(cinemachineTransposer.m_FollowOffset, targetFollowOffset, zoomSpeed * Time.deltaTime);
    }

    private void HandleCameraRotation()
    {
        Vector3 rotationVector = new Vector3(0, 0, 0);
        if (Input.GetKey(KeyCode.Q))
        {
            rotationVector.y = -1;

        }
        if (Input.GetKey(KeyCode.E))
        {
            rotationVector.y = 1;
        }
        transform.eulerAngles += rotationVector * cameraRotationQESpeed * Time.deltaTime;

        if (Input.GetMouseButton(1))
        {
            float mouseX = Input.GetAxis("Mouse X") * cameraRotationRightMouseSpeed * Time.deltaTime;
            transform.Rotate(Vector3.up, mouseX, Space.World);
        }
    }

    private void HandleCameraMovement()
    {
        Vector3 inputMoveDir = new Vector3(0, 0, 0);
        if (Input.GetKey(KeyCode.W))
        {
            inputMoveDir.z += 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            inputMoveDir.z -= 1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            inputMoveDir.x -= 1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            inputMoveDir.x += 1;
        }

        Vector3 moveVector = transform.forward * inputMoveDir.z + transform.right * inputMoveDir.x;
        transform.position += moveVector * cameraMoveSpeed * Time.deltaTime;
    }
}

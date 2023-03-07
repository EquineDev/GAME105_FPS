using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{
    [SerializeField]private Transform m_playerCamera;
    [SerializeField] private PlayerInput m_playerInput;
    [SerializeField] private float m_jumpHeight = 5f;
    [SerializeField] private float m_walkSpeed = 5f;
    [SerializeField] private float m_gravity = 5f;
    [SerializeField] private float m_maxPitch = 90f;
    [SerializeField] private float m_mouseSensitivity = 5f;
    
    private InputAction m_move;
    private InputAction m_lookX;
    private InputAction m_lookY;
    private InputAction m_fire;
    private Vector3 m_SpawnPoint;
    private CharacterController m_controller;
    
    private Vector3 m_velocity;

    private float m_pitch;
    // Start is called before the first frame update
    void Awake()
    {
        m_playerInput = new PlayerInput();
        m_controller = GetComponent<CharacterController>();
        m_SpawnPoint = transform.position;
    }

    private void OnEnable()
    {
        m_move = m_playerInput.Player.Move;
        m_move.Enable();

        m_lookX = m_playerInput.Player.MouseX;
        m_lookX.Enable();
        m_lookY = m_playerInput.Player.MouseY;
        m_lookY.Enable();
        
        m_fire = m_playerInput.Player.Fire;
        m_fire.Enable();
        m_fire.performed += Jump;
    }

    private void OnDisable()
    {
        m_move.Disable();
        m_fire.Disable();
        m_lookY.Disable();
        m_lookX.Disable();
    }

    private void Update()
    {

        Movement();
        Look();
        Gravity();


    }

    public void GoTojail()
    {
     
  
        transform.position = m_SpawnPoint;

    }
    

    private void Jump(InputAction.CallbackContext context)
    {
        
        if (m_controller.isGrounded)
        {
            m_velocity += Vector3.up * Mathf.Sqrt(m_jumpHeight * -2f * m_gravity);
          
        }
       Debug.Log(m_controller.isGrounded);
    }

    private void Movement()
    {
        Vector2 inputRead = m_move.ReadValue<Vector2>();
        Vector3 moveDirection = ((transform.right * inputRead.x) + (transform.forward * inputRead.y)).normalized;
        moveDirection *= m_walkSpeed * Time.deltaTime;
        m_controller.Move(moveDirection);
    }

    private void Look()
    {
       
        m_pitch -= m_lookY.ReadValue<float>() * m_mouseSensitivity * Time.deltaTime;
        m_pitch = Mathf.Clamp(m_pitch, -m_maxPitch, m_maxPitch);
        m_playerCamera.localRotation = Quaternion.Euler(m_pitch, 0, 0);
        transform.Rotate(Vector3.up *  m_lookX.ReadValue<float>()  * m_mouseSensitivity * Time.deltaTime);
    }

    private void Gravity()
    {
        m_velocity.y -= m_gravity * Time.deltaTime;

        m_controller.Move(m_velocity * Time.deltaTime);
    }

    

    
}

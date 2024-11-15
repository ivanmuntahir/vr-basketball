using System.Collections;
using System.Collections.Generic;
using BNG;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(FollowCamera))]
public class CallCanvas : MonoBehaviour
{
    [SerializeField] private InputActionAsset inputAction;
    [SerializeField] private string command;
    
    private FollowCamera _follower;
    private InputBridge _inputBridge;
    
    // Start is called before the first frame update
    void Start()
    {
        _follower = GetComponent<FollowCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_follower == null)
            _follower = FindObjectOfType<FollowCamera>();
        
        if(!inputAction.FindAction(command).enabled)
            inputAction.Enable();

        if (inputAction.FindAction(command).WasPressedThisFrame())
            _follower.FollowPlayer();
    }
}

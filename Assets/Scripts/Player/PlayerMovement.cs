using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomEventBus;
using CustomEventBus.Signals;

public class PlayerMovement : MonoBehaviour, IService
{
    [SerializeField] private float moveSpeed = 5f;

    private EventBus _eventBus;

    private Camera mainCamera;

    private Vector3 _targetPosition;
    private bool _isMoving = false;

    private bool _isLevelRunning = false;

    public void Init()
    {
        _eventBus = ServiceLocator.Current.Get<EventBus>();

        mainCamera = Camera.main;

        _eventBus.Subscribe<StartGameSingal>(OnStartGame);
        _eventBus.Subscribe<StopGameSingal>(OnStopGame);
    }

    private void Update()
    {
        if (!_isLevelRunning) return;

        if (Input.GetMouseButtonDown(0))
        {
            _targetPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            _targetPosition.z = 0;
            _isMoving = true;
        }
    }

    private void FixedUpdate()
    {
        if (!_isLevelRunning) return;


        if (_isMoving)
        {
            MoveToTargetPosition();
        }
    }


    private void MoveToTargetPosition()
    {
        transform.position = Vector3.MoveTowards(transform.position, _targetPosition, moveSpeed * Time.fixedDeltaTime);
        if (Vector3.Distance(transform.position, _targetPosition) < 0.01f)
        {
            _isMoving = false;
        }
    }


    private void OnStartGame(StartGameSingal singal)
    {
        _isLevelRunning = true;
    }

    private void OnStopGame(StopGameSingal signal)
    {
        _isLevelRunning = false;
    }

    private void OnDestroy()
    {
        _eventBus.Unsubscribe<StartGameSingal>(OnStartGame);
        _eventBus.Unsubscribe<StopGameSingal>(OnStopGame);
    }
}

using GameInput.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using GameUI.Interfaces;
using UnityEngine;

namespace GameCore.Player
{
    /// <summary>
    /// Player movement component it ensures the player moves towards the input position.
    /// </summary>
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed;
        [SerializeField] private Rigidbody2D _rigidBody2d;
        private Vector2 _targetPosition;
        private bool _isMoving;
        private IInputManager _inputManager;
        private IUIManager _uiManager;

        protected virtual void Start()
        {
            if (!_rigidBody2d)
                _rigidBody2d = GetComponent<Rigidbody2D>();
            _inputManager = DiContainer.Instance.ServiceProvider.GetRequiredService<IInputManager>();
            _uiManager = DiContainer.Instance.ServiceProvider.GetRequiredService<IUIManager>();
        }

        protected virtual void Update()
        {
            if (_inputManager.IsInputActive())
            {
                _targetPosition = _inputManager.GetInputPosition();
                _isMoving = true;
            }
            else
            {
                if (_targetPosition == (Vector2)transform.position)
                    _isMoving = false;
            }

            if (_inputManager.GetMenuKey())
                _uiManager.PauseGame();

        }

        private void Awake() => _moveSpeed = DiContainer.Instance.GameConfig.PlayerMoveSpeed;

        private void FixedUpdate()
        {
            if (!_isMoving) return;

            var currentPosition = _rigidBody2d.position;
            var targetPosition = new Vector2(_targetPosition.x, _targetPosition.y);
            var newPosition = Vector2.MoveTowards(currentPosition, targetPosition, _moveSpeed * Time.fixedDeltaTime);
            _rigidBody2d.MovePosition(newPosition);
        }
    }
}
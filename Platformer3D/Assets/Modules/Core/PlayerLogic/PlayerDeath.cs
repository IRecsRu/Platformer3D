using UnityEngine;

namespace Modules.Core.PlayerLogic
{
  [RequireComponent(typeof(PlayerHealth), typeof(PlayerAnimator))]
  public class PlayerDeath : MonoBehaviour
  {
    private PlayerHealth _health;
    private PlayerMove _move;
    private PlayerAnimator _animator;
    
    private bool _isDead;

    private void Awake()
    {
      _health = GetComponent<PlayerHealth>();
      _move = GetComponent<PlayerMove>();
      _animator = GetComponent<PlayerAnimator>();
    }

    private void Start() =>
      _health.HealthChanged += HealthChanged;

    private void OnDestroy() =>
      _health.HealthChanged -= HealthChanged;

    private void HealthChanged()
    {
      if (!_isDead && _health.Current <= 0) 
        Die();
    }

    private void Die()
    {
      _isDead = true;
      
      if(_move != null)
        _move.enabled = false;
      
      //_animator.PlayDeath();
    }
  }
}
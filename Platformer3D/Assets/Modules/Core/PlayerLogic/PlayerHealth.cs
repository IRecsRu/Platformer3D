using System;
using Modules.Core.Data;
using Modules.Core.Infrastructure.Services.PersistentProgress;
using Modules.Core.Logic;
using UnityEngine;

namespace Modules.Core.PlayerLogic
{
  [RequireComponent(typeof(PlayerAnimator))]
  public class PlayerHealth : MonoBehaviour, IHealth, ISavedProgress
  {
    private PlayerAnimator _animator;
    private HealthState _healthState;

    public event Action HealthChanged;

    public float Current
    {
      get => _healthState.CurrentHP;
      private set
      {
        if(value == _healthState.CurrentHP)
          return;
        
        _healthState.CurrentHP = value;
        HealthChanged?.Invoke();
      }
    }

    public float Max
    {
      get => _healthState.MaxHP;
      set => _healthState.MaxHP = value;
    }

    private void Awake() =>
      _animator = GetComponent<PlayerAnimator>();

    public void LoadProgress(PlayerProgress progress)
    {
      _healthState = progress.HealthState;
      HealthChanged?.Invoke();
    }

    public void UpdateProgress(PlayerProgress progress) =>
      progress.HealthState = _healthState;

    public void TakeDamage(float damage)
    {
      if(Current <= 0)
        return;
      
      Current -= damage;
      _animator.PlayHit();
    }
    
    public void TakeLethalDamage() =>
      TakeDamage(Current);
  }
}
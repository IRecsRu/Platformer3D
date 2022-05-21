using System;
using Modules.Core.Logic;
using Modules.Core.Services.Input;
using UnityEngine;
using Zenject;

namespace Modules.Core.PlayerLogic
{
  public class PlayerAnimator : MonoBehaviour, IAnimationStateReader
  {
    private const int SensitivityModifier = 4;
    private Animator _animator;

    private static readonly int MoveHash = Animator.StringToHash("Walking");
    private static readonly int HitHash = Animator.StringToHash("Hit");
    private static readonly int DieHash = Animator.StringToHash("Die");

    private readonly int _idleStateHash = Animator.StringToHash("Idle");
    private readonly int _idleStateFullHash = Animator.StringToHash("Base Layer.Idle");
    private readonly int _walkingStateHash = Animator.StringToHash("Move");
    private readonly int _deathStateHash = Animator.StringToHash("Die");
    
    private IInputService _inputService;

    public event Action<AnimatorState> StateEntered;
    public event Action<AnimatorState> StateExited;

    public AnimatorState State { get; private set; }
    
    public void Constructor(IInputService inputService)
    {
      _inputService = inputService;
      _animator = GetComponent<Animator>();
    }
    
    private void Update()
    {
      if(_inputService == null)
        return;
      
      float value = CharacterDisplacementValue();
      _animator.SetFloat(MoveHash, value, 0.1f, Time.deltaTime);
    }
    
    private float CharacterDisplacementValue() =>
      (Math.Abs(_inputService.Axis.x) + Math.Abs(_inputService.Axis.y)) * SensitivityModifier;

    public void PlayHit() =>
      _animator.SetTrigger(HitHash);

    public void PlayDeath() =>
      _animator.SetTrigger(DieHash);

    public void ResetToIdle() =>
      _animator.Play(_idleStateHash, -1);

    public void EnteredState(int stateHash)
    {
      State = StateFor(stateHash);
      StateEntered?.Invoke(State);
    }

    public void ExitedState(int stateHash) =>
      StateExited?.Invoke(StateFor(stateHash));

    private AnimatorState StateFor(int stateHash)
    {
      AnimatorState state;
      if (stateHash == _idleStateHash)
      {
        state = AnimatorState.Idle;
      }
      else if (stateHash == _walkingStateHash)
      {
        state = AnimatorState.Walking;
      }
      else if (stateHash == _deathStateHash)
      {
        state = AnimatorState.Died;
      }
      else
      {
        state = AnimatorState.Unknown;
      }

      return state;
    }
  }
}
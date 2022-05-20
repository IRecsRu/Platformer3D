using UnityEngine;

namespace Modules.Core.Services.Input
{
  public abstract class InputService : IInputService
  {
    protected const string Horizontal = "Horizontal";
    protected const string Vertical = "Vertical";
    private const string Button = "Jump";

    public abstract Vector2 Axis { get; }

    public bool IsJumpButtonUp() =>
      SimpleInput.GetButtonUp(Button);

    protected static Vector2 SimpleInputAxis() =>
      new Vector2(SimpleInput.GetAxis(Horizontal), SimpleInput.GetAxis(Vertical));
  }
}
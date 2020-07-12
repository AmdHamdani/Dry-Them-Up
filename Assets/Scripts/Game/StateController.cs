public class StateController<T> where T : System.Enum
{

    public T CurrentState { get; private set; }

    public void ChangeState(T state)
    {
        CurrentState = state;
    }

}
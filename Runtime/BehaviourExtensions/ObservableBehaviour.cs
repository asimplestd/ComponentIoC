using System.Threading.Tasks;
using System.Threading;
using UnityEngine;

public abstract class ObservableBehaviour : ObservableBehaviour<bool>
{
    protected void Complete()
    {
        Complete(true);
    }
}

public abstract class ObservableBehaviour<TResult> : MonoBehaviour
{
    private TaskCompletionSource<TResult> _observationCs;
    private CancellationTokenSource _completeTs = new CancellationTokenSource();

    public bool Completed { get; private set; }
    protected CancellationToken CompleteToken => _completeTs.Token;

    private TResult _result;

    public Task<TResult> Observe()
    {
        if (Completed)
        {
            return Task.FromResult(_result);
        }
        if (_observationCs == null)
        {
            _observationCs = new TaskCompletionSource<TResult>();
        }

        return _observationCs.Task;
    }

    public void WithCancellation(CancellationToken cancellationToken)
    {
        cancellationToken.Register(OnCancellationRequested);
    }

    private void Start()
    {
        OnStart();
    }

    protected virtual void OnStart()
    { }

    protected virtual void OnDestroy()
    {
        if (!Completed)
        {
            Complete(default);
        }
    }

    private void OnCancellationRequested()
    {
        if (!Completed)
        {
            Complete(default);
        }
    }

    protected void Complete(TResult result)
    {
        if (Completed)
        {
            return;
        }

        Completed = true;
        _completeTs.Cancel();

        Destroy(gameObject);

        if (_observationCs != null)
        {
            _observationCs.TrySetResult(result);
        }

        _completeTs.Dispose();
    }
}

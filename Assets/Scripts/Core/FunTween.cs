using System.Collections.Generic;

public class FunTween : SingletonBehaviour<FunTween>
{

    public int counter;
    public Dictionary<int, Tween> tweenQueue = new Dictionary<int, Tween>();

    private void Update()
    {
        if(tweenQueue.Count > 0)
        {
            for(int i = 0; i < tweenQueue.Count; i++)
            {
            //foreach (var tween in tweenQueue)
            //{
                //if (!tweenQueue[i].IsFinish())
                //{
                //    tweenQueue[i].OnTween?.Invoke();
                //}
                //else
                //{
                //    tweenQueue[i].OnCompleted?.Invoke();
                //    tweenQueue.Remove()
                //}
            //}
            }
        }
    }

    public void AddTween(System.Func<bool> IsTweenFinish, System.Action OnTweenProcess, System.Action OnProcessCompleted = null)
    {
        tweenQueue.Add(counter++, new Tween
        {
            IsFinish = IsTweenFinish,
            OnTween = OnTweenProcess,
            OnCompleted = OnProcessCompleted
        });
    }

}

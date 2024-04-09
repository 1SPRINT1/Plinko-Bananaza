using System;
using System.Threading;
using System.Threading.Tasks;
using DG.Tweening;

public static class StatesExtensions
{
    public static void SafeExit(this IScreenState state, Action complete)
    {
        if (state != null)
        {
            state.Exit(complete);
            return;
        }

        complete?.Invoke();
    }

    public static void SafeEnter(this IScreenState state, Action complete)
    {
        if (state != null)
        {
            state.Enter(complete);
            return;
        }

        complete?.Invoke();
    }
    
    public static void SafeSuspend(this IScreenState state, Action complete)
    {
        if (state != null)
        {
            state.Sleep(false, complete);
            return;
        }

        complete?.Invoke();
    }
    
    public static void SafeResume(this IScreenState state, Action complete)
    {
        if (state != null)
        {
            state.Sleep(true, complete);
            return;
        }

        complete?.Invoke();
    }
}

public static class Delay
{
    public static Sequence Execute(float time, Action onComplete) {
        return DOTween
            .Sequence()
            .AppendInterval(time)
            .OnComplete(() => onComplete?.Invoke())
            .Play();
    }
    
    public static Sequence Each(int time, Action<int> onTime) {
        var sequence = DOTween.Sequence();

        var counter = time;

        while (counter >= 0) {
            counter -= 1;
            var value = counter;
            sequence
                .Append(DOVirtual.DelayedCall(1, () => onTime?.Invoke(value)));
        }
        
        return sequence.Play();
    }
}
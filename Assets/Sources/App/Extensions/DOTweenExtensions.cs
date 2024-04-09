using System.Globalization;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using TMPro;

public static class DOTweenExtensions
{

    public static TweenerCore<int, int, NoOptions> DOCounter(
        this TMP_Text target, int fromValue, int endValue, float duration, bool addThousandsSeparator = true, CultureInfo culture = null
    ){
        int v = fromValue;
        CultureInfo cInfo = !addThousandsSeparator ? null : culture ?? CultureInfo.InvariantCulture;
        TweenerCore<int, int, NoOptions> t = DOTween.To(() => v, x => {
            v = x;
            target.text = addThousandsSeparator
                ? v.ToString("N0", cInfo)
                : v.ToString();
        }, endValue, duration);
        t.SetTarget(target);
        return t;
    }
}

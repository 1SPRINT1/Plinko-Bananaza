using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScreenLayout : MonoBehaviour
{
    private readonly List<ILayoutElement> _elements = new();
    private readonly List<PresenterView> _presenterViews = new();

    public void Construct()
    {
        GetComponentsInChildren<ILayoutElement>(true).Each(InitializeElement);

        GetComponentsInChildren<PresenterView>(true).Each(ConstructPresenter);
    }

    private void ConstructPresenter(PresenterView view) {
        view.Construct(this);
        _presenterViews.Add(view);
    }

    public bool TryGetElement<TElement>(out TElement element) where TElement : ILayoutElement
    {
        element = _elements.OfType<TElement>().FirstOrDefault();
        return element != null;
    }

    public bool TryGetElements<TElement>(out TElement[] elements) where TElement : ILayoutElement
    {
        elements = _elements.OfType<TElement>().ToArray();
        return elements.Length != 0;
    }
    
    protected void SetSelectedFromGroup<TGroup>(byte index) where TGroup : ButtonGroupElement {
        if (TryGetElement<TGroup>(out TGroup group))
            group.SetElementSelected(index);
    }

    protected void InitializeElement(ILayoutElement element)
    {
        _elements.Add(element);
    }

    public virtual void OnShowLayout()
    {
        _elements.Each(e => e.OnShowElement());
        _presenterViews.Each(p => p.Show());
    }

    public virtual void OnHideLayout()
    {
        _elements.Each(e => e.OnHideElement());
        _presenterViews.Each(p => p.Hide());
    }
    
    
}
using System;
using System.Linq;
using UnityEngine;
using Zenject;

public abstract class EntryUIKitInstaller : MonoInstaller {

    private Type[] _domainTypes;
    private IPresenterView[] _presenterViews;

    public override void InstallBindings() {
        _domainTypes = AppDomain
            .CurrentDomain
            .GetAssembliesTypes();
        
        _presenterViews = FindObjectsOfType<MonoBehaviour>(true)
            .OfType<IPresenterView>()
            .ToArray();

        BindSources();

        BindStates();

        BindInstances();
    }

    private void BindSources() {
        
        BindScreens();

        BindServices();
    }

    protected abstract void BindServices();

    private void BindStates() {
        BindPresenters();

        BindAsSingle<IUIKitModel>();
        
        BindAsSingle<IScreenState>();
        
        Container.BindAsSingle<AppClient>();
    }

    private void BindPresenters() {
        _presenterViews.Each(Container.BindAsSingleFromInstanceType);

        OnType<IPresenter>().Each(t => {
            if (t.TryGetAttribute<PresenterOfAttribute>(out var attribute))
                attribute.Types.Each(typeInAttribute => {
                    Container
                        .BindInterfacesAndSelfTo(t)
                        .WhenInjectedInto(typeInAttribute);
                });
        });
    }

    private void BindAsSingle<TInterface>() {
        OnType<TInterface>().Each(type => {
            Container
                .BindInterfacesAndSelfTo(type)
                .AsSingle();
        });
    }

    private void BindScreens() {
        FindObjectsOfType<MenuScreen>(true).Each(screen => {
            Container
                .BindInterfacesAndSelfTo(screen.GetType())
                .FromInstance(screen)
                .AsSingle();
        });
    }

    private void BindInstances() {
        OnBindTargetInstances();
        Container.BindAsSingleFromInstanceMono<AppEntry>(); //bootstrap
    }

    protected abstract void OnBindTargetInstances();

    private Type[] OnType<TType>() {
        var interfaceType = typeof(TType);

        return _domainTypes
                .Where(type => !type.IsAbstract && type.IsClass && interfaceType.IsAssignableFrom(type))
                .ToArray();
    }

}

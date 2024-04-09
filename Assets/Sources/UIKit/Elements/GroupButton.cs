using System;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.UI.UIKit.Elements
{
    [RequireComponent(typeof(Button))]
    public class GroupButton : MonoBehaviour, ISelectButton
    {
        [SerializeField] protected int _index;
        [SerializeField] protected GameObject[] _unselected;
        [SerializeField] protected GameObject[] _selection;
        [SerializeField] private GameObject[] _checked;

        [Space] 
        [SerializeField, HideInInspector] protected Button _button;
        public event Action<ISelectButton> Clicked;
        public int Index => _index;
        public bool State => gameObject.activeInHierarchy;
    
        private void OnEnable()
        {
            _button.onClick.AddListener(() => Clicked?.Invoke(this));
        }

        private void OnDisable()
        {
            _button.onClick.RemoveAllListeners();
        }

        private void OnValidate()
        {
            SetValidatedName();
            _button = GetComponent<Button>();
        }

        public virtual void SetSelectedState(bool state)
        {
            _selection.Each(s => s.SetActive(state));
            
            if(_unselected != null)
                _unselected.Each(s => s.SetActive(!state));
        }

        public virtual void SetCheckedState(bool state)
        {
            if(_checked != null)
                _checked.Each(s => s.SetActive(state));
        }

        private void SetValidatedName()
        {
#if UNITY_EDITOR
            if (!UnityEditor.SceneManagement.PrefabStageUtility.GetCurrentPrefabStage() != null) //TODO: warning fix
                name = $"[{GetType().Name}:{_index}]";
#endif
        }
    }
}
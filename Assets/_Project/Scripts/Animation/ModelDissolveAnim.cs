using UnityEngine;
using DG.Tweening;

namespace Reactor.Animation
{
    public class ModelDissolveAnim : MonoBehaviour
    {
        public float duration = 1.5f;
        public string propertyName = "_DissolveAmount";
        
        private Renderer[] _renderers;
        private MaterialPropertyBlock _propBlock;

        private void Awake()
        {
            _renderers = GetComponentsInChildren<Renderer>();
            _propBlock = new MaterialPropertyBlock();
        }

        public void Play(bool appear)
        {
            float start = appear ? 1f : 0f;
            float end = appear ? 0f : 1f;

            DOTween.To(() => start, x => {
                foreach (var r in _renderers)
                {
                    r.GetPropertyBlock(_propBlock);
                    _propBlock.SetFloat(propertyName, x);
                    r.SetPropertyBlock(_propBlock);
                }
            }, end, duration);
        }
    }
}

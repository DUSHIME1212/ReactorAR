using UnityEngine;
using DG.Tweening;

namespace Reactor.Training
{
    public class ModelSwapManager : MonoBehaviour
{
    public Transform anchor;
    public float dissolveTime = 0.4f;

    GameObject _current;

    public void SwapTo(GameObject prefab)
    {
        if (prefab == null) return;

        if (_current != null)
        {
            // Shrink + fade old model out
            var old = _current;
            var renderers = old.GetComponentsInChildren<Renderer>();
            DOTween.Sequence()
                .Append(old.transform.DOScale(Vector3.zero, dissolveTime).SetEase(Ease.InBack))
                .OnComplete(() => Destroy(old));
        }

        // Spawn new model, scale up from zero
        _current = Instantiate(prefab, anchor.position, anchor.rotation);
        _current.transform.localScale = Vector3.zero;
        _current.transform
            .DOScale(Vector3.one, dissolveTime + 0.1f)
            .SetEase(Ease.OutBack)
            .SetDelay(dissolveTime * 0.5f);
    }
}}

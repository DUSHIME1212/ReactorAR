using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Reactor.UI
{
    public class NavTabController : MonoBehaviour
    {
        public Button[] tabs;
        public GameObject[] panels;
        public RectTransform underline;
        public float animDuration = 0.3f;

        private void Start()
        {
            for (int i = 0; i < tabs.Length; i++)
            {
                int index = i;
                tabs[i].onClick.AddListener(() => SwitchTab(index));
            }
            SwitchTab(0); // Default
        }

        public void SwitchTab(int index)
        {
            for (int i = 0; i < panels.Length; i++)
            {
                bool active = (i == index);
                panels[i].SetActive(active);
                
                if (active && underline != null)
                {
                    underline.DOMoveX(tabs[i].transform.position.x, animDuration).SetEase(Ease.OutBack);
                }
            }
        }
    }
}

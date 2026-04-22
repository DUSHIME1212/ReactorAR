using UnityEngine;
using TMPro;
using DG.Tweening;

namespace Reactor.UI
{
    public class StepInstructionUI : MonoBehaviour
    {
        public TextMeshProUGUI instructionLabel;
        public float fadeDuration = 0.25f;

        public void UpdateInstruction(string text)
        {
            DOTween.To(() => instructionLabel.color, x => instructionLabel.color = x, new Color(instructionLabel.color.r, instructionLabel.color.g, instructionLabel.color.b, 0f), fadeDuration).OnComplete(() => {
                instructionLabel.text = text;
                DOTween.To(() => instructionLabel.color, x => instructionLabel.color = x, new Color(instructionLabel.color.r, instructionLabel.color.g, instructionLabel.color.b, 1f), fadeDuration);
            });
        }
    }
}

using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SF.UI.View
{
    public class CharacterStatBarView : BaseView
    {
        [SerializeField] private TextMeshProUGUI _statText;
        [SerializeField] private Image _statFillImage;

        public void ChangeStatText(int amount)
        {
            _statText.text = amount.ToString();
        }

        public void ChangeStatFill(int amount, int max)
        {
            _statFillImage.fillAmount = amount / (max * 1f);
        }
    }
}
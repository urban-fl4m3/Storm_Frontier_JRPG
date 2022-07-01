using TMPro;
using UnityEngine;

namespace SF.UI.View
{
    public class TextButtonView : ButtonView
    {
        [SerializeField] private TextMeshProUGUI _text;
        
        public void SetText(string textContent)
        {
            _text.text = textContent;
        }
    }
}
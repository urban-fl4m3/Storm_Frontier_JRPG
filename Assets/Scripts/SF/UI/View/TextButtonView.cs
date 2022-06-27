using UnityEngine;
using UnityEngine.UI;

namespace SF.UI.View
{
    public class TextButtonView : ButtonView
    {
        [SerializeField] private Text _text;
        
        public void SetText(string textContent)
        {
            _text.text = textContent;
        }
    }
}
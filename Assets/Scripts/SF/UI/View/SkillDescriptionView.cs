using TMPro;
using UnityEngine;

namespace SF.UI.View
{
    public class SkillDescriptionView : BaseView
    {
        [SerializeField] private TMP_Text _descriptionText;

        public void SetDescription(string description)
        {
            _descriptionText.text = description;
        }
    }
}
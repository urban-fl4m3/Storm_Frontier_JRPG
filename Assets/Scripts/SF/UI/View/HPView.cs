using UnityEngine;
using UnityEngine.UI;

namespace SF.UI.View
{
    public class HPView : MonoBehaviour
    {
        [SerializeField] private Text _hpText;
        [SerializeField] private Image _fillImage;
        [SerializeField] private int _maxHP;

        private int _currentHp;

        public void SetHP(int hp)
        {
            Debug.Log("Change");
            
            _hpText.text = hp.ToString();
            
            _maxHP = hp;
            _currentHp = _maxHP;
            
            _fillImage.fillAmount = 1;
        }

        public void ChangeHP(int damage)
        {            
            _currentHp -= damage;
            
            _hpText.text = _currentHp.ToString();
            
            _fillImage.fillAmount = _currentHp / _maxHP * 1f;
        }
    }
}
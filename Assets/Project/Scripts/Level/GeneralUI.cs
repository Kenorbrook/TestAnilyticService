using UnityEngine;
using UnityEngine.UI;

namespace Level
{
    public class GeneralUI : MonoBehaviour
    {
        [SerializeField]
        private Button _menu;

        [SerializeField]
        private Button _addMoney1000;

        [SerializeField]
        private Button _addMoney500;

        [SerializeField]
        private Button _addMoney100;

        [SerializeField]
        private Button[] _achievementButton;

        private void Awake()
        {
            _menu.onClick.AddListener(SceneLoader.MenuScene);
            _menu.onClick.AddListener(PlayerDataChanger.CompleteLevel);
            InitAddMoney();
            InitAchieveButton();
        }

        private void InitAddMoney()
        {
            _addMoney100.onClick.AddListener(() =>
                PlayerDataChanger.AddMoney(100)
            );
            _addMoney500.onClick.AddListener(() =>
                PlayerDataChanger.AddMoney(500)
            );
            _addMoney1000.onClick.AddListener(() =>
                PlayerDataChanger.AddMoney(1000)
            );
        }

        private void InitAchieveButton()
        {
            foreach (var _button in _achievementButton)
            {
                _button.onClick.AddListener(() =>
                {
                    PlayerDataChanger.CompleteAchievement(_button.name);
                });
            }
        }
    }
}
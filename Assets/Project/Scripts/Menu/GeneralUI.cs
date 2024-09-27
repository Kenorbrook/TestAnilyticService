using Analytics;
using UnityEngine;
using UnityEngine.UI;

namespace MainMenu
{
    public class GeneralUI : MonoBehaviour
    {
        [SerializeField]
        private Button _play;

        private void Awake()
        {
            InitMenu();
        }

        private void InitMenu()
        {
            _play.onClick.AddListener(() =>
            {
                Analytic.SendEvent("StartLevel",$"level:{PlayerDataChanger.data.level}");
                SceneLoader.PlayScene();
            });
        }
    }
}
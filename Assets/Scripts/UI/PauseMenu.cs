using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Turnpturn.UI
{
    public class PauseMenu : MonoBehaviour
    {
        [SerializeField]
        private GameObject _completePauseMenuPanel;
        [SerializeField]
        private GameObject _mainMenuPanel;
        [SerializeField]
        private GameObject _optionsMenuPanel;
        [SerializeField]
        private GameObject _pauseButton;
        // Start is called before the first frame update
        void Start()
        {
            _completePauseMenuPanel.SetActive(false);
            _mainMenuPanel.SetActive(false);
            _optionsMenuPanel.SetActive(false);
            _pauseButton.SetActive(true);
        }

        // Update is called once per frame
        void Update()
        {

        }
        public void Pause()
        {
            Time.timeScale = 0f;
            _pauseButton.SetActive(false);
            _completePauseMenuPanel.SetActive(true);
            _mainMenuPanel.SetActive(true);
            _optionsMenuPanel.SetActive(false);
        }
        public void Unpause()
        {
            _pauseButton.SetActive(true);
            _completePauseMenuPanel.SetActive(false);
            _mainMenuPanel.SetActive(false);
            _optionsMenuPanel.SetActive(false);
            Time.timeScale = 1f;
        }

        public void HideOptionsMenu()
        {
            _optionsMenuPanel.SetActive(false);
        }
        public void ShowOptionsMenu()
        {
            _optionsMenuPanel.SetActive(true);
        }
        public void HideMainOptionsMenu()
        {
            _mainMenuPanel.SetActive(false);
        }
        public void ShowMainOptionsMenu()
        {
            _mainMenuPanel.SetActive(true);
        }

    }
}
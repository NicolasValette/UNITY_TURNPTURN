using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Turnpturn.Game.System
{
    public class LevelLoader : MonoBehaviour
    {
        [SerializeField]
        private Animator _transition;
        [SerializeField]
        private float _transitionTime = 1;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void LoadNextLevel()
        {
            StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
        }
        public void LoadSpecificLevel(int buildIndex)
        {
            StartCoroutine(LoadLevel(buildIndex));
        }
        public void LoadSpecificLevel(string sceneName)
        {
            StartCoroutine(LoadLevel(sceneName));
        }


        private IEnumerator LoadLevel(int levelIndex)
        {
            Debug.Log("Load Scene");
            _transition.SetTrigger("StartTransition");

            yield return new WaitForSeconds(_transitionTime);

            SceneManager.LoadScene(levelIndex);
        }
        private IEnumerator LoadLevel(string sceneName)
        {
            Debug.Log("Load Scene");
            _transition.SetTrigger("StartTransition");

            yield return new WaitForSeconds(_transitionTime);

            SceneManager.LoadScene(sceneName);
        }



    }


}

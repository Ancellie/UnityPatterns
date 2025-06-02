using UnityEngine;
using UnityEngine.UI; // Додати для нового UI

namespace UnityStandardAssets.Utility
{
    public class SimpleActivatorMenu : MonoBehaviour
    {
        // Використовуємо UI.Text замість застарілого GUIText
        public Text camSwitchButton;  // <-- Потрібно призначити в інспекторі
        public GameObject[] objects;

        private int m_CurrentActiveObject;

        private void OnEnable()
        {
            m_CurrentActiveObject = 0;
            camSwitchButton.text = objects[m_CurrentActiveObject].name;
        }

        public void NextCamera()
        {
            int nextActiveObject = (m_CurrentActiveObject + 1) % objects.Length;

            for (int i = 0; i < objects.Length; i++)
            {
                objects[i].SetActive(i == nextActiveObject);
            }

            m_CurrentActiveObject = nextActiveObject;
            camSwitchButton.text = objects[m_CurrentActiveObject].name;
        }
    }
}
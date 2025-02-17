using UnityEngine;
using System.Collections.Generic;
using CubeHopper.Game;
using UnityEngine.Events;

namespace CubeHopper.UI
{
    public class DragToStart : MonoBehaviour
    {
        [SerializeField] private Player _playerScript;
        [SerializeField] private List<CanvasGroup> _UI_list;
        [SerializeField] private UnityEvent onGameStart;
        public void StartGame()
        {
            //_playerScript.enabled = true;
            foreach(CanvasGroup item in _UI_list) 
            {
                item.LeanAlpha(0, 0.3f).setEaseOutQuad().setOnComplete(() =>  
                item.gameObject.SetActive(false));
            }
            onGameStart.Invoke();
            enabled = false;
        }
        private void OnEnable()
        {
            Player.OnRelease += StartGame;
        }
        private void OnDisable()
        {
            Player.OnRelease -= StartGame;
        }
        //i guess you always get what you deserve of your actions

    }
}

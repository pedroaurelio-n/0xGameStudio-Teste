using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
 
namespace PedroAurelio
{
    [RequireComponent(typeof(Canvas))]
    public class HealthBarManager : MonoBehaviour
    {
        private static Transform _canvasTransform;
        private static List<HealthBar> _barList;

        private void Awake()
        {
            _barList = new List<HealthBar>();
            _canvasTransform = transform;
        }

        private void Update()
        {
            for (int i = 0; i < _barList.Count; i++)
            {
                _barList[i].UpdatePosition();
            }
        }

        public static void AttachBarToCanvas(HealthBar bar)
        {
            _barList.Add(bar);
            bar.transform.SetParent(_canvasTransform);
        }

        private void OnDisable() => _barList.Clear();
    }
}
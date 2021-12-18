using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ComponentToggle.Components
{
    class Pens
    {
        private static GameObject[] _Pens;

        public static void OnLevelLoad() {
            _Pens = (from x in UnityEngine.Object.FindObjectsOfType<GameObject>()
                where x.name.ToLower().Contains("pen") | x.name.ToLower().Contains("marker") | x.name.ToLower().Contains("grip")
                select x).ToArray<GameObject>();
        }

        public static void Toggle(bool state) {
            if (_Pens == null) OnLevelLoad();
            foreach (var p in _Pens) p.gameObject.SetActive(!state);
        }
    }
}
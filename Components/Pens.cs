using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ComponentToggle.Components
{
    class Pens
    {
        private static VRC.SDK.Internal.Whiteboard.Marker[] Markers;
        private static List<GameObject> penArray;
        private static GameObject QVPens;
        private static GameObject QVEraser;
        private static GameObject GenericPens_1;
        private static GameObject GenericPens_2;
        private static GameObject GenericPens_3;
        private static GameObject GenericPens_4;
        //private static GameObject GenericPens_5;
        //private static GameObject GenericPens_6;
        //private static GameObject GenericPens_7;
        //private static GameObject GenericPens_8;
        //private static GameObject GenericPens_9;
        private static GameObject GenericEraser_1;
        private static GameObject GenericEraser_2;
        private static GameObject GenericEraser_3;
        //private static GameObject GenericEraser_4;
        //private static GameObject GenericEraser_5;
        //private static GameObject GenericEraser_6;
        //private static GameObject GenericEraser_7;
        //private static GameObject GenericEraser_8;
        //private static GameObject GenericEraser_9;

        public static void OnLevelLoad()
        {
            Markers = UnityEngine.Object.FindObjectsOfType<VRC.SDK.Internal.Whiteboard.Marker>();

            penArray = new List<GameObject>();
            QVPens = GameObject.Find("QvPen");
            QVEraser = GameObject.Find("QvEraser");
            GenericPens_1 = GameObject.Find("Pen");
            GenericPens_2 = GameObject.Find("Pens");
            GenericPens_3 = GameObject.Find("PenSet");
            GenericPens_4 = GameObject.Find("Markers");
            //GenericPens_5 = GameObject.Find("");
            //GenericPens_6 = GameObject.Find("");
            //GenericPens_7 = GameObject.Find("");
            //GenericPens_8 = GameObject.Find("");
            //GenericPens_9 = GameObject.Find("");
            GenericEraser_1 = GameObject.Find("Eraser");
            GenericEraser_2 = GameObject.Find("Erasers");
            GenericEraser_3 = GameObject.Find("Deleter");
            //GenericEraser_4 = GameObject.Find("");
            //GenericEraser_5 = GameObject.Find("");
            //GenericEraser_6 = GameObject.Find("");
            //GenericEraser_7 = GameObject.Find("");
            //GenericEraser_8 = GameObject.Find("");
            //GenericEraser_9 = GameObject.Find("");

            penArray.Add(QVPens);
            penArray.Add(QVEraser);
            penArray.Add(GenericPens_1);
            penArray.Add(GenericPens_2);
            penArray.Add(GenericPens_3);
            penArray.Add(GenericPens_4);
            //penArray.Add(GenericPens_5);
            //penArray.Add(GenericPens_6);
            //penArray.Add(GenericPens_7);
            //penArray.Add(GenericPens_8);
            //penArray.Add(GenericPens_9);
            penArray.Add(GenericEraser_1);
            penArray.Add(GenericEraser_2);
            penArray.Add(GenericEraser_3);
            //penArray.Add(GenericEraser_4);
            //penArray.Add(GenericEraser_5);
            //penArray.Add(GenericEraser_6);
            //penArray.Add(GenericEraser_7);
            //penArray.Add(GenericEraser_8);
            //penArray.Add(GenericEraser_9);
        }

        public static void Toggle()
        {
            if (penArray == null) OnLevelLoad();

            foreach (var gameObject in penArray)
            {
                gameObject.gameObject.SetActive(Main.Pens.Value);
            }

            foreach (var gameObject in Markers)
            {
                gameObject.GetComponent<VRC.SDK.Internal.Whiteboard.Marker>().enabled = Main.Pens.Value;
                gameObject.gameObject.SetActive(Main.VRC_Pickup_Objects.Value);
            }
        }
    }
}

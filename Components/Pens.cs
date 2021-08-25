using System.Collections.Generic;
using UnityEngine;
using Markers = VRC.SDK.Internal.Whiteboard.Marker;

namespace ComponentToggle.Components
{
    class Pens
    {
        private static Markers[] Markers;
        private static List<GameObject> penArray;
        private static GameObject QVPens;
        private static GameObject QVEraser;
        private static GameObject MidnightRooftop_TopPens;
        private static GameObject MidnightRooftop_BottomPens;
        private static GameObject GenericPens_1, GenericPens_2, GenericPens_3, GenericPens_4;
        private static GameObject GenericEraser_1, GenericEraser_2, GenericEraser_3;
        private static GameObject VRCHome_PenSet;

        public static void OnLevelLoad()
        {
            Markers = Object.FindObjectsOfType<Markers>();

            penArray = new List<GameObject>();
            QVPens = GameObject.Find("QvPen");
            QVEraser = GameObject.Find("QvEraser");
            MidnightRooftop_TopPens = GameObject.Find("QvPenTopFloor");
            MidnightRooftop_BottomPens = GameObject.Find("QvPenBottomFloor");
            GenericPens_1 = GameObject.Find("Pen");
            GenericPens_2 = GameObject.Find("Pens");
            GenericPens_3 = GameObject.Find("PenSet");
            GenericPens_4 = GameObject.Find("Markers");
            VRCHome_PenSet = GameObject.Find("_Pickups/PenSet");

            GenericEraser_1 = GameObject.Find("Eraser");
            GenericEraser_2 = GameObject.Find("Erasers");
            GenericEraser_3 = GameObject.Find("Deleter");

            penArray.Add(QVPens);
            penArray.Add(QVEraser);
            penArray.Add(MidnightRooftop_TopPens);
            penArray.Add(MidnightRooftop_BottomPens);
            penArray.Add(GenericPens_1);
            penArray.Add(GenericPens_2);
            penArray.Add(GenericPens_3);
            penArray.Add(GenericPens_4);
            penArray.Add(VRCHome_PenSet);
            penArray.Add(GenericEraser_1);
            penArray.Add(GenericEraser_2);
            penArray.Add(GenericEraser_3);
        }

        public static void Toggle(bool tempOn = false)
        {
            if (penArray == null || Markers == null) OnLevelLoad();

            foreach (var g in penArray) {
                if (tempOn) g.gameObject.SetActive(true);
                else g.gameObject.SetActive(Main.Pens.Value);
            }

            foreach (var gameObject in Markers) {
                if (tempOn) {
                    gameObject.GetComponent<Markers>().enabled = true;
                    gameObject.gameObject.SetActive(true);
                } else {
                    gameObject.GetComponent<Markers>().enabled = Main.Pens.Value;
                    gameObject.gameObject.SetActive(Main.Pens.Value);
                }
            }
        }
    }
}
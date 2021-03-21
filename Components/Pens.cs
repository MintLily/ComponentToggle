using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using ComponentToggle.Utilities.Config;
using System.Collections;
using System.Net;
using MelonLoader;
using System.ComponentModel;
using ComponentToggle.Utilities;
using Harmony;

namespace ComponentToggle.Components
{
    class Pens
    {
        private static VRC.SDK.Internal.Whiteboard.Marker[] Markers;
        private static List<GameObject> penArray;
        private static GameObject QVPens;
        private static GameObject QVEraser;
        private static GameObject MidnightRooftop_TopPens;
        private static GameObject MidnightRooftop_BottomPens;
        private static GameObject GenericPens_1;
        private static GameObject GenericPens_2;
        private static GameObject GenericPens_3;
        private static GameObject GenericPens_4;

        private static GameObject GenericEraser_1;
        private static GameObject GenericEraser_2;
        private static GameObject GenericEraser_3;

        private static string url = "https://raw.githubusercontent.com/KortyBoi/ComponentToggle/master/Utilities/WebAdded/GameObjects.txt";
        private static string listOfExtraGameObjects = string.Empty;
        public static string[] splitListOfObjects;
        
        public static void Init()
        {
            MelonLogger.Msg("Downloading Extra GameObject list...");
            WebClient WebAdded = new WebClient();
            listOfExtraGameObjects = WebAdded.DownloadString(url);
            MelonLogger.Msg("Download Complete");

            char[] delims = new[] { '\r', '\n' };
            splitListOfObjects = listOfExtraGameObjects.Split(delims, StringSplitOptions.RemoveEmptyEntries);

            MelonLogger.Msg("Found " + splitListOfObjects.Length.ToString() + " GameObjects in list");
        }

        public static void OnLevelLoad()
        {
            Markers = UnityEngine.Object.FindObjectsOfType<VRC.SDK.Internal.Whiteboard.Marker>();

            penArray = new List<GameObject>();
            QVPens = GameObject.Find("QvPen");
            QVEraser = GameObject.Find("QvEraser");
            MidnightRooftop_TopPens = GameObject.Find("QvPenTopFloor");
            MidnightRooftop_BottomPens = GameObject.Find("QvPenBottomFloor");
            GenericPens_1 = GameObject.Find("Pen");
            GenericPens_2 = GameObject.Find("Pens");
            GenericPens_3 = GameObject.Find("PenSet");
            GenericPens_4 = GameObject.Find("Markers");

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
            penArray.Add(GenericEraser_1);
            penArray.Add(GenericEraser_2);
            penArray.Add(GenericEraser_3);

            try
            {
                for (int i = 0; i < splitListOfObjects.Length; i++)
                {
                    penArray.Add(GameObject.Find(splitListOfObjects[i]));
                }
            }
            catch { MelonLogger.Error("Could not add spilt list to penArray"); }
        }

        public static void Toggle(bool tempOn = false)
        {
            if (penArray == null) OnLevelLoad();

            foreach (var gameObject in penArray)
            {
                if (tempOn)
                    gameObject.gameObject.SetActive(true);
                else
                    gameObject.gameObject.SetActive(CustomConfig.Get().Pens);
            }

            foreach (var gameObject in Markers)
            {
                if (tempOn)
                {
                    gameObject.GetComponent<VRC.SDK.Internal.Whiteboard.Marker>().enabled = true;
                    gameObject.gameObject.SetActive(true);
                }
                else
                {
                    gameObject.GetComponent<VRC.SDK.Internal.Whiteboard.Marker>().enabled = CustomConfig.Get().Pens;
                    gameObject.gameObject.SetActive(CustomConfig.Get().Pens);
                }
            }
        }

        // Unneeded - Credits go to Loukylor for this function
        /*
        public static List<GameObject> GetGameObjectsWithNameInChildren(GameObject gameObject, string name, List<GameObject> gameObjects = null)
        {
            if (gameObjects == null) gameObjects = new List<GameObject>();
            if (gameObject.name == name) gameObjects.Add(gameObject);
            foreach (var child in gameObject.transform)
                return GetGameObjectsWithNameInChildren(child.Cast<Transform>().gameObject, name, gameObjects);
            return gameObjects;
        }
        */
    }
}
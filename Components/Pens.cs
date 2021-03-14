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

        private static GameObject WebAdded_1;
        private static GameObject WebAdded_2;
        private static GameObject WebAdded_3;
        private static GameObject WebAdded_4;
        private static GameObject WebAdded_5;
        private static GameObject WebAdded_6;
        private static GameObject WebAdded_7;
        private static GameObject WebAdded_8;
        private static GameObject WebAdded_9;

        private static Queue<string> WebAddedItems = new Queue<string>();
        private static List<string> WebAddedResults = new List<string>();
        public static int WebAddedResultCount;

        private static string WebBaseURL(string number)
        {
            return $"https://raw.githubusercontent.com/KortyBoi/ComponentToggle/master/Utilities/WebAdded/GameObjectName_{number}.txt";
        }

        private static string[] WebAdded = { WebBaseURL("1"), WebBaseURL("2"), WebBaseURL("3"), WebBaseURL("4"), WebBaseURL("5"), WebBaseURL("6"), WebBaseURL("7"), WebBaseURL("8"), WebBaseURL("9") };

        public static void Init()
        {
            downloadFiles(WebAdded);
        }

        private static void downloadFiles(IEnumerable<string> urls)
        {
            foreach (var url in urls)
            {
                WebAddedItems.Enqueue(url);
            }

            // Starts the download
            MelonLogger.Msg("Downloading Extra GameObject list...");

            DownloadFile();
        }

        private static void DownloadFile()
        {
            try
            {
                if (WebAddedItems.Any())
                {
                    WebClient client = new WebClient();
                    client.DownloadStringCompleted += client_DownloadFileCompleted;
                    //client.DownloadProgressChanged += client_DownloadProgressChanged;

                    var nextItem = WebAddedItems.Dequeue();
                    client.DownloadStringAsync(new Uri(nextItem));
                    return;
                }
            }
            catch (Exception e)
            {
                MelonLogger.Error("Error in downloading strings of GameObjects: File may not exist\n" + e.ToString());
            }

            // End of the download
            MelonLogger.Msg("Download Complete");
            WebAddedResultCount = WebAddedResults.Count;
        }

        private static void client_DownloadFileCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            if (e != null)
                WebAddedResults.Add(e.Result);
            else if (e.Error != null)
                MelonLogger.Error("object is null\n" + e.ToString());
            if (e.Cancelled)
                MelonLogger.Warning("Operation was cancelled.");
            DownloadFile();
        }

        /*
        private static void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            double bytesIn = double.Parse(e.BytesReceived.ToString());
            double totalBytes = double.Parse(e.TotalBytesToReceive.ToString());
            double percentage = bytesIn / totalBytes * 100;
            progressBar1.Value = int.Parse(Math.Truncate(percentage).ToString());
        }
        */

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

            try
            {
                WebAdded_1 = GameObject.Find(WebAddedResults[0]);
                WebAdded_2 = GameObject.Find(WebAddedResults[1]);
                WebAdded_3 = GameObject.Find(WebAddedResults[2]);
                WebAdded_4 = GameObject.Find(WebAddedResults[3]);
                WebAdded_5 = GameObject.Find(WebAddedResults[4]);
                WebAdded_6 = GameObject.Find(WebAddedResults[5]);
                WebAdded_7 = GameObject.Find(WebAddedResults[6]);
                WebAdded_8 = GameObject.Find(WebAddedResults[7]);
                WebAdded_9 = GameObject.Find(WebAddedResults[8]);
            }
            catch { MelonLogger.Error("Could not assign objects from downloaded list"); }

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
                penArray.Add(WebAdded_1);
                penArray.Add(WebAdded_2);
                penArray.Add(WebAdded_3);
                penArray.Add(WebAdded_4);
                penArray.Add(WebAdded_5);
                penArray.Add(WebAdded_6);
                penArray.Add(WebAdded_7);
                penArray.Add(WebAdded_8);
                penArray.Add(WebAdded_9);
            }
            catch { MelonLogger.Error("Could not add objects to array"); }
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
    }
}
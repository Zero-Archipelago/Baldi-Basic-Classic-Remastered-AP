using UnityEngine;


namespace BaldiAP
{
    public class Menu
    {
        private bool ShowMenu = false;
        public string Server;
        public string Slot;
        public string Password;

        private Rect WindowRect => new Rect(20, 20, 250, 150);

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.F1))
            {
                ShowMenu = !ShowMenu;
            }
        }

        public void OnGUI()
        {
            if (!ShowMenu) return;
            GUI.Window(0, WindowRect, DrawMenu, "AP Connection");
        }

        private void DrawMenu(int id)
        {
            GUILayout.BeginVertical(GUILayout.Width(300));

            GUILayout.Label("Archipelago Connection");

            GUILayout.Space(8);

            GUILayout.Label("Server:");
            Server = GUILayout.TextField("Server:Port");

            GUILayout.Label("Slot:");
            Slot = GUILayout.TextField("Slot Name");

            GUILayout.Label("Password:");
            Password = GUILayout.PasswordField("Room Password", '*');

            GUILayout.Space(10);

            if (GUILayout.Button("Connect", GUILayout.Height(30)))
            {
                ConnectToArchipelago(Server, Slot, Password);
            }

            GUILayout.EndVertical();

            GUI.DragWindow();
        }

        private void ConnectToArchipelago(string server, string slot, string password)
        {
            Debug.Log($"Connecting to {server} with slot {slot}...");
        }
    }
}
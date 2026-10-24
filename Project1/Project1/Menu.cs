using UnityEngine;


namespace BaldiAP
{
    public class Menu: MonoBehaviour
    {
        public bool showMenu = false;
        public string server = "archipelago.gg:";
        public string slot = "";
        public string password = "";

        private Rect WindowRect => new Rect(350, 50, 400, 300);

        public void Update()
        {
            if (Main.ArchipelagoHandler.IsConnected)
            {
                showMenu = false;
                CursorController.Instance.DisableClick(false);
                CursorController.Instance.Hide(false);
                return;
            }
            MenuShow();
        }

        public void MenuShow()
        {
            if (!Input.GetKeyDown(KeyCode.F1)) return;
            showMenu = !showMenu;
            
            Cursor.visible = showMenu;
            Cursor.lockState = showMenu ? CursorLockMode.None : CursorLockMode.Locked;

            CursorController.Instance.DisableClick(showMenu);
            CursorController.Instance.Hide(showMenu);
        }

        public void OnGUI()
        {
            if (!showMenu) return;
            GUI.Window(0, WindowRect, DrawMenu, "Archipelago Connection");
        }

        private void DrawMenu(int id)
        {
            GUILayout.BeginVertical(GUILayout.Width(380));

            GUILayout.Label("Server:");
            server = GUILayout.TextField(server);

            GUILayout.Label("Slot:");
            slot = GUILayout.TextField(slot);

            GUILayout.Label("Password:");
            password = GUILayout.PasswordField(password, '*');

            GUILayout.Space(25);

            if (GUILayout.Button("Connect", GUILayout.Height(35)))
            {
                ConnectToArchipelago(server, slot, password);
            }

            GUILayout.EndVertical();

            GUI.DragWindow();
        }

        private void ConnectToArchipelago(string server, string slot, string password)
        {
            Main.ArchipelagoHandler.CreateSession(server, slot, password);
            Main.ArchipelagoHandler.Connect();
        }
    }
}
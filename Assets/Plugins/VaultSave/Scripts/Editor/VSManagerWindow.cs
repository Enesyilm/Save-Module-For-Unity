using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEditor;
using UnityEngine;
using VaultSave.AutoSave;
using VaultSave.SaveConfig;
using Directory = System.IO.Directory;
using File = System.IO.File;

namespace VaultSave.Editor
{
    public class VSManagerWindow : EditorWindow
    {
        public List<int> List = new();
        private Rect _settingsLayout;
        private VaultSaveDefaults _vaultSaveDefaults;

        [MenuItem("Tools/Vault Save")]
        static void OpenSaveManagerWindow()
        {
            VSManagerWindow vsManagerWindow = (VSManagerWindow)GetWindow(typeof(VSManagerWindow), false, "Vault Save");
            vsManagerWindow.minSize = new Vector2(300, 400);
            vsManagerWindow.maximized = true;
            vsManagerWindow.Show();
        }

        private void OnEnable()
        {
            if (_vaultSaveDefaults is null) GetVaultDefaultsData();
        }

        private void GetVaultDefaultsData()
        {
            _vaultSaveDefaults = Resources.Load<VaultSaveDefaults>("VaultSaveDefaults");
            EditorUtility.SetDirty(_vaultSaveDefaults);
        }

        private void DrawSettings()
        {
            GUILayout.BeginArea(_settingsLayout);
            GUILayout.BeginHorizontal();

            GUILayout.EndHorizontal();
            GUILayout.EndArea();
        }

        private void OnGUI()
        {
            EditorGUIUtility.labelWidth = 100;
            DrawLayouts();
            DrawSettings();
        }

        private void DrawLayouts()
        {
            var style = new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter };
            var header = new GUIStyle(GUI.skin.label)
            {
                fontSize = 20,
                alignment = TextAnchor.MiddleCenter
            };

            void RandomPassButton(GUIStyle guıStyle1)
            {
                EditorGUILayout.LabelField(
                    "*Caution changing password operation will be delete old encrypted save file");

                GUILayout.BeginHorizontal(guıStyle1, GUILayout.ExpandWidth(true));
                _vaultSaveDefaults.vsSystemData.Password =
                    EditorGUILayout.TextField("Password:", _vaultSaveDefaults.vsSystemData.Password);
                if (GUILayout.Button("Random Password"))
                {
                    _vaultSaveDefaults.vsSystemData.Password = GeneratePassword(32);
                    ClearDataPath();
                }

                GUILayout.EndHorizontal();
            }

            void DefineSettingsLayout()
            {
                _settingsLayout.x = 0;
                _settingsLayout.y = 0;
                _settingsLayout.width = Screen.width / 2;
                _settingsLayout.height = Screen.height / 2;
            }

            void EncryptToggle(GUIStyle style1)
            {
                EditorGUI.BeginChangeCheck();
                GUILayout.BeginHorizontal(style1, GUILayout.ExpandWidth(true));
                GUILayout.FlexibleSpace();
                _vaultSaveDefaults.vsSystemData.EncryptWithAes =
                    (Boolean)EditorGUILayout.Toggle("Encrypt With AES", _vaultSaveDefaults.vsSystemData.EncryptWithAes);

                GUILayout.FlexibleSpace();
                GUILayout.EndHorizontal();
            }

            void AutoSaveToggle(GUIStyle guıStyle)
            {
                GUILayout.BeginHorizontal(guıStyle, GUILayout.ExpandWidth(true));
                GUILayout.FlexibleSpace();
                _vaultSaveDefaults.vsSystemData.AutoSave =
                    (Boolean)EditorGUILayout.Toggle("Auto Save", _vaultSaveDefaults.vsSystemData.AutoSave);
                GUILayout.FlexibleSpace();
                GUILayout.EndHorizontal();
                GUILayout.BeginHorizontal(guıStyle, GUILayout.ExpandWidth(true));
                EditorGUILayout.LabelField("Auto Save On this LifeCycles: ");
                _vaultSaveDefaults.vsSystemData.vsAutoSaveTypes =
                    (VSAutoSaveTypes)EditorGUILayout.EnumFlagsField(_vaultSaveDefaults.vsSystemData.vsAutoSaveTypes);

                GUILayout.EndHorizontal();
            }

            void PrettyFormatToggle(GUIStyle guıStyle)
            {
                EditorGUI.BeginChangeCheck();
                GUILayout.BeginHorizontal(guıStyle, GUILayout.ExpandWidth(true));
                GUILayout.FlexibleSpace();
                _vaultSaveDefaults.vsSystemData.PrettyFormat =
                    (Boolean)EditorGUILayout.Toggle("Pretty Format", _vaultSaveDefaults.vsSystemData.PrettyFormat);
                GUILayout.FlexibleSpace();
                GUILayout.EndHorizontal();
            }


            DefineSettingsLayout();
            GUILayout.Label("Settings", header, GUILayout.ExpandWidth(true));
            AutoSaveToggle(style);
            EncryptToggle(style);
            PrettyFormatToggle(style);

            if (GUILayout.Button("Open Data Path"))
            {
                OpenDataPath();
            }

            if (GUILayout.Button("Clear Data Path"))
            {
                ClearDataPath();
            }


            RandomPassButton(style);
        }

        private void ClearDataPath()
        {
            if (Directory.Exists(_vaultSaveDefaults.vsSaveConfigData.GetDirectoryPath()))
            {
                DeleteDirectory(_vaultSaveDefaults.vsSaveConfigData.GetDirectoryPath());
            }
        }

        public static void DeleteDirectory(string target_dir)
        {
            string[] files = Directory.GetFiles(target_dir);
            string[] dirs = Directory.GetDirectories(target_dir);

            foreach (string file in files)
            {
                File.SetAttributes(file, FileAttributes.Normal);
                File.Delete(file);
            }

            foreach (string dir in dirs)
            {
                DeleteDirectory(dir);
            }

            Directory.Delete(target_dir, false);
        }

        private void OpenDataPath()
        {
            CreateDirectory();
            EditorUtility.RevealInFinder(_vaultSaveDefaults.vsSaveConfigData.GetDirectoryPath());
        }

        private void CreateDirectory()
        {
            if (!Directory.Exists(_vaultSaveDefaults.vsSaveConfigData.GetDirectoryPath()))
            {
                Directory.CreateDirectory(_vaultSaveDefaults.vsSaveConfigData.GetDirectoryPath());
            }
        }

        static string GeneratePassword(int length)
        {
            const string validChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            using (RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider())
            {
                byte[] data = new byte[length];
                crypto.GetBytes(data);
                StringBuilder result = new StringBuilder(length);
                foreach (byte b in data)
                {
                    result.Append(validChars[b % validChars.Length]);
                }

                return result.ToString();
            }
        }
    }
}
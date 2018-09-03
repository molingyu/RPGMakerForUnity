using System;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Assets.Editor
{
    public class RpgMakerWindow : OdinEditorWindow
    {
        private static Texture2D _RPGMakerLogo;

        private static string RPGMakerVersion = "0.1.0";

        private static GUIStyle sectionTitleLabelStyle;
        private static GUIStyle cardTitleStyle;
        private static GUIStyle logoTitleStyle;
        private static GUIStyle cardStylePadding;
        private static GUIStyle cardStyle;
        private static GUIStyle headerBtnStyle;

        public static Texture2D RPGMakerLogo
        {
            get
            {
                if (_RPGMakerLogo == null)
                {
                    _RPGMakerLogo = Resources.Load<Texture2D>("RPGMakerLogo");
                }

                return _RPGMakerLogo;
            }
        }

        [MenuItem("Tools/RPG Maker/Getting Started")]
        public static void ShowWindow()
        {
            GetWindow<RpgMakerWindow>().position = GUIHelper.GetEditorWindowRect().AlignCenter(785, 660);
        }

        protected override void Initialize()
        {

            this.WindowPadding = new Vector4(0, 0, 0, 0);
        }

        void OnGUI()
        {
            InitStyles();

            var rect = EditorGUILayout.BeginVertical();
            {
                if (EditorGUIUtility.isProSkin)
                {
                    EditorGUI.DrawRect(new Rect(0, rect.yMax, this.position.width, this.position.height), SirenixGUIStyles.DarkEditorBackground);
                }
                else
                {
                    EditorGUI.DrawRect(new Rect(0, 0, this.position.width, rect.yMax), SirenixGUIStyles.BoxBackgroundColor);
                }
                DrawHeader();
            }
            EditorGUILayout.EndVertical();
            SirenixEditorGUI.DrawBorders(rect, 0, 0, 0, 1, SirenixGUIStyles.BorderColor);
                        DrawBody();
            DrawFooter();
        }

        [ResponsiveButtonGroup(DefaultButtonSize = ButtonSizes.Medium)]
        private void CreateDatabase()
        {

        }

        [ResponsiveButtonGroup]
        private void Manual() { Application.OpenURL(""); }


        [ResponsiveButtonGroup]
        private void FAQ() { Application.OpenURL(""); }

        [ResponsiveButtonGroup]
        private void Roadmap() { Application.OpenURL(""); }

        [ResponsiveButtonGroup]
        private void Issues() { Application.OpenURL(""); }

        [ResponsiveButtonGroup]
        private void AssetStore() { Application.OpenURL(""); }

        private void InitStyles()
        {
            sectionTitleLabelStyle = sectionTitleLabelStyle ?? new GUIStyle(SirenixGUIStyles.SectionHeaderCentered)
            {
                fontSize = 17,
                margin = new RectOffset(0, 0, 10, 10),
            };

            cardTitleStyle = cardTitleStyle ?? new GUIStyle(SirenixGUIStyles.SectionHeader)
            {
                fontSize = 15,
                fontStyle = FontStyle.Bold,
                margin = new RectOffset(0, 0, 0, 4)
            };

            logoTitleStyle = logoTitleStyle ?? new GUIStyle(SirenixGUIStyles.SectionHeader)
            {
                fontSize = 23,
                padding = new RectOffset(20, 20, 0, 0),
                alignment = TextAnchor.MiddleLeft
            };

            cardStylePadding = cardStylePadding ?? new GUIStyle()
            {
                padding = new RectOffset(15, 15, 15, 15),
                stretchHeight = false
            };

            cardStyle = cardStyle ?? new GUIStyle("sv_iconselector_labelselection")
            {
                padding = new RectOffset(15, 15, 15, 15),
                margin = new RectOffset(0, 0, 0, 0),
                stretchHeight = false
            };
        }

        void DrawHeader()
        {
            var rect = GUILayoutUtility.GetRect(0, 70);
            GUI.Label(rect.AlignCenterY(45), new GUIContent(" PRG Maker For Unity", RPGMakerLogo), logoTitleStyle);


            var versionLabel = new GUIContent("PRG Maker For Unity " + RPGMakerVersion);
            var w = Mathf.Max(SirenixGUIStyles.CenteredGreyMiniLabel.CalcSize(versionLabel).x, 165);

            var rightRect = rect.AlignRight(w + 10);
            rightRect.x -= 10;
            rightRect.y += 8;
            rightRect.height = 17;
            rightRect.y += 15;
            if (Event.current.type == EventType.Repaint)
            {
                GUI.Label(rightRect, versionLabel, SirenixGUIStyles.CenteredGreyMiniLabel);
            }


            rightRect.y += rightRect.height + 4;
            if (GUI.Button(rightRect, "View Release Notes", SirenixGUIStyles.MiniButton))
            {
                Application.OpenURL("http://github.com/molingyu/RPGMakerForUnity");
            }

            SirenixEditorGUI.DrawHorizontalLineSeperator(rect.x, rect.y, rect.width);
            SirenixEditorGUI.DrawHorizontalLineSeperator(rect.x, rect.yMax, rect.width);
        }

        void DrawBody()
        {
        }

        internal void DrawFooter()
        {
            base.OnGUI();
        }


    }
}

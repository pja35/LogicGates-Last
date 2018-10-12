using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

using System;

#if UNITY_EDITOR
using UnityEditor;

namespace StatsAndInfo
{
    [CustomEditor(typeof(InfoGatherer))]
    public class InfoGathererInspector : Editor
    {

        InfoGatherer gatherer;

        Text fps, fpsMin, fpsMax, fpsAvg, gpu, gpuMem, cpu, cpuCores, cpuFreq, ram, ramAlloc, os, deviceModel;

        void OnEnable()
        {

            gatherer = (InfoGatherer)target;
            

            if (gatherer.texts == null)
            {
                gatherer.actions = new List<Action>();

                gatherer.texts = new Text[13];

                gatherer.dataAmount = 0;
                gatherer.totalFPS = 0;
            }


            fps = gatherer.texts[0];
            fpsMin = gatherer.texts[1];
            fpsMax = gatherer.texts[2];
            fpsAvg = gatherer.texts[3];
            gpu = gatherer.texts[4];
            gpuMem = gatherer.texts[5];
            cpu = gatherer.texts[6];
            cpuCores = gatherer.texts[7];
            cpuFreq = gatherer.texts[8];
            ram = gatherer.texts[9];
            ramAlloc = gatherer.texts[10];
            os = gatherer.texts[11];
            deviceModel = gatherer.texts[12];

        }



        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            EditorGUILayout.Space();

            EditorGUILayout.LabelField("UI Texts");

            EditorGUI.indentLevel++;

            fps = (Text)EditorGUILayout.ObjectField("FPS:", fps, typeof(Text), true);
            if (fps)
            {
                EditorGUI.indentLevel++;
                gatherer.fpsWarning = EditorGUILayout.Toggle("Low FPS Warning:", gatherer.fpsWarning);
                if (gatherer.fpsWarning)
                {
                    EditorGUI.indentLevel++;
                    gatherer.fpsWarningLimit = EditorGUILayout.IntField(new GUIContent("Warning Limit:", "For values lower than this, the color of the FPS text will be the color you set in the next field."), gatherer.fpsWarningLimit);
                    gatherer.fpsWarningColor = EditorGUILayout.ColorField("Warning Color:", gatherer.fpsWarningColor);
                    EditorGUI.indentLevel--;
                }
                EditorGUI.indentLevel--;
            }
            fpsMin = (Text)EditorGUILayout.ObjectField("FPS Minimum:", fpsMin, typeof(Text), true);
            fpsMax = (Text)EditorGUILayout.ObjectField("FPS Maximum:", fpsMax, typeof(Text), true);
            fpsAvg = (Text)EditorGUILayout.ObjectField("FPS Average:", fpsAvg, typeof(Text), true);
            gpu = (Text)EditorGUILayout.ObjectField("GPU:", gpu, typeof(Text), true);
            gpuMem = (Text)EditorGUILayout.ObjectField("GPU Memory:", gpuMem, typeof(Text), true);
            cpu = (Text)EditorGUILayout.ObjectField("CPU:", cpu, typeof(Text), true);
            cpuCores = (Text)EditorGUILayout.ObjectField("CPU Cores:", cpuCores, typeof(Text), true);
            cpuFreq = (Text)EditorGUILayout.ObjectField("CPU Frequency:", cpuFreq, typeof(Text), true);
            ram = (Text)EditorGUILayout.ObjectField("RAM Total:", ram, typeof(Text), true);
            ramAlloc = (Text)EditorGUILayout.ObjectField("RAM Allocated:", ramAlloc, typeof(Text), true);
            os = (Text)EditorGUILayout.ObjectField("Operating System:", os, typeof(Text), true);
            deviceModel = (Text)EditorGUILayout.ObjectField("Device Model:", deviceModel, typeof(Text), true);

            EditorGUI.indentLevel--;

            EditorGUILayout.Space();

            if (GUILayout.Button("Show"))
            {
                gatherer.Show();
            }

            if (GUILayout.Button("Hide"))
            {
                gatherer.Hide();
            }
            

            gatherer.texts[0] = fps;

            gatherer.texts[1] = fpsMin;


            gatherer.texts[2] = fpsMax;

            gatherer.texts[3] = fpsAvg;

            gatherer.texts[4] = gpu;

            gatherer.texts[5] = gpuMem;
            gatherer.texts[6] = cpu;
            gatherer.texts[7] = cpuCores;
            gatherer.texts[8] = cpuFreq;
            gatherer.texts[9] = ram;
            gatherer.texts[10] = ramAlloc;
            gatherer.texts[11] = os;
            gatherer.texts[12] = deviceModel;

            EditorUtility.SetDirty(target);

        }



    }

}

#endif

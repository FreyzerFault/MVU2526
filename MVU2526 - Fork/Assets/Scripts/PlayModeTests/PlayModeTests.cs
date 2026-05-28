using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace PlayModeTests
{
    public class PlayModeTests
    {
        private List<string> errorMessages = new();
        private List<string> warningMessages = new();
        
        [UnityTest]
        public IEnumerator NoesisScene_Stay1Second_NoErrorsOrWarnings()
        {
            LogAssert.ignoreFailingMessages = true;
            SceneData sceneData = new() { guid = "966fb10d10f8d7e4d986660edc1516c0" };
            SceneManager.LoadScene(sceneData.Path);
            yield return new WaitForSeconds(1);

            if (errorMessages.Count > 0 || warningMessages.Count > 0)
            {
                string message = errorMessages.Count > 0 
                    ? $"There are {errorMessages.Count} errors:\n{string.Join("\n\n", errorMessages)}" 
                    : $"There are {warningMessages.Count} warnings:\n{string.Join("\n\n", warningMessages)}";
                
                Assert.Fail(message);
            }
        }

        // Se ejecuta antes de cualquier test
        [SetUp]
        public void Setup() => Application.logMessageReceived += LogMessageReceived;
        
        // Se ejecuta al final de cada test
        [TearDown]
        public void TearDown()
        {
            errorMessages.Clear();
            warningMessages.Clear();
            Application.logMessageReceived -= LogMessageReceived;
        }

        private void LogMessageReceived(string condition, string stackTrace, LogType type)
        {
            switch (type)
            {
                case LogType.Assert:
                case LogType.Exception:
                case LogType.Error:
                    errorMessages.Add($"{condition}:\n{stackTrace}");
                    break;
                case LogType.Warning:
                    warningMessages.Add($"{condition}:\n{stackTrace}");
                    break;
            }
        }
    }
}

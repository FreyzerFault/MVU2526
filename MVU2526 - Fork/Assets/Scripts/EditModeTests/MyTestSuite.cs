using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace EditModeTests
{
    public class MyTestSuite
    {
        public static StableScenes stableScenes;
        public static List<SceneData> SceneDatas => stableScenes.stableScenesGuids;
        static MyTestSuite()
        {
            stableScenes = AssetDatabase.LoadAssetByGUID<StableScenes>(new GUID("8e278cc7704eb0744bd6c4302f7386fa"));
        }
        
        [Test]
        public void HealthWith15Points_Give5Damage_ResultIs10()
        {
            // Arrange
            Health health = new(15);
            
            // Act
            health.Damage(5);
            
            // Assert
            Assert.AreEqual(10, health.Value);
            
            // Esto es lo mismo
            Assert.That(health.Value, Is.EqualTo(10));
        }

        [Test]
        public void MyPrefabMyMenuInjectorComponent_ByDefinition_ContinueMessageIsFilled()
        {
            GameObject myPrefab = AssetDatabase.LoadAssetByGUID<GameObject>(new GUID("e93c9abe28ddf1b4e9a2820568945bd3"));
            MainMenuInjector injector = myPrefab.GetComponent<MainMenuInjector>();
            Assert.That(injector.continueMessage, Is.Not.Null);
        }

        [Test]
        public void HealthWith15Points_Heal10_ResultIs25()
        {
            Health health = new(15);
            health.Heal(10);
            Assert.That(health.Value, Is.EqualTo(25), "The Health Value must be 25");
        }

        [TestCaseSource(nameof(SceneDatas))]
        public void UIScene_ByDefinition_ThereIsOnlyOneCamera(SceneData sceneGuid)
        {
            string scenePath = AssetDatabase.GUIDToAssetPath(sceneGuid);
            EditorSceneManager.OpenScene(scenePath);
            Camera[] cameraList = Object.FindObjectsByType<Camera>();

            if (cameraList.Length != 1)
            {
                string message = cameraList.Length == 0
                    ? $"No hay cámaras en la escena {scenePath}" 
                    : $"Hay más de 1 cámara en la escena {scenePath}\n" +
                      $"Camera List:\n{string.Join("\n", cameraList.Select(c => $"    - {c.name}"))}";
                Assert.Fail(message);
            }
        }
    }
    
    
    public class Health
    {
        public float Value { get; private set; }

        public Health(int maxHealth) => Value = maxHealth;

        public void Damage(float amount) => Value -= amount;

        public void Heal(int amount) => Value += amount;
    }
}

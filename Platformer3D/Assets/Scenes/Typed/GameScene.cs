//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace IJunior.TypedScenes
{
    using System.Threading.Tasks;
    using UnityEngine.SceneManagement;
    using UnityEngine.ResourceManagement.ResourceProviders;
    
    
    public class GameScene : TypedScene
    {
        
        private const string _sceneName = "GameScene";
        
        public static System.Threading.Tasks.Task<UnityEngine.ResourceManagement.ResourceProviders.SceneInstance> LoadAsync(LoadSceneMode loadSceneMode = LoadSceneMode.Single)
        {
            return LoadScene(_sceneName, loadSceneMode);
        }
    }
}

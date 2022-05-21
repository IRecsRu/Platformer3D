


# [Platformer3D](https://github.com/IRecsRu/Platformer3D)
Платформер для мобильных устройств. Создан в рамках тестового задания. 
Реализовано перемещение персонажа.
Управление камерой - Cinemachine.
Сохранение прогресса.
Загрузка ресурсов через  Addressables.
Созданы базовые инструменты при помощи UnityEditor.

## Геймплей

![Change API Level to 4.0](https://i.ibb.co/gSHCbfB/bandicam-2022-05-21-18-49-10-714.jpg)

## Временные решения
Сейчас эта операция скрывается за экраном загрузки.
В данном фрагменте используется множество GetComponentsInChildren.
При дальнейшее доработки, весь фрагмент перейдет в UnityEditor, и все зависимости будут указаны.
```
public class LevelFactory  
{  
 private readonly AddressablesGameObjectLoader _loaderGameObject = new();  
 readonly ISaveLoadService _saveLoadService;  
  
 public LevelFactory(ISaveLoadService saveLoadService) =>  
  _saveLoadService = saveLoadService;  
  
 public async Task<Level> CreateLevel(string levelName)  
 {  
  GameObject levelObject = await _loaderGameObject.InstantiateGameObject(null, levelName);  
  Level level = levelObject.GetComponent<Level>();  
  
  foreach(SaveTrigger saveTrigger in levelObject.GetComponentsInChildren<SaveTrigger>())  
   saveTrigger.Construct(_saveLoadService);  
  
  foreach(LevelCompletionTrigger saveTrigger in levelObject.GetComponentsInChildren<LevelCompletionTrigger>())  
   saveTrigger.Construct(_saveLoadService, level);  
  
  SpawnPoint spawnPoint = levelObject.GetComponentInChildren<SpawnPoint>();  
  spawnPoint.Construct(_saveLoadService);  
  level.SetSpawnPoint(spawnPoint.transform);  
  
  levelObject.transform.position = Vector3.zero;  
  
  return levelObject.GetComponent<Level>();  
 }
```


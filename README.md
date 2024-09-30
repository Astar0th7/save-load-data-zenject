Service for saving and loading game data on Zenject framework

**Installation:**
Download and install the latest version of Zenject from the link - [https://github.com/modesttree/Zenject/releases](https://github.com/modesttree/Zenject/releases)
Download and install the save package from the link - [https://github.com/Astar0th7/save-load-data-zenject/releases](https://github.com/Astar0th7/save-load-data-zenject/releases)

**How to use:**
1. Register the preservation service in ProjectContext.
```csharp
public override void InstallBindings()
{
  Container
    .BindInterfacesAndSelfTo<SaveLoadListener>()
    .AsSingle();
}
```
2. Add the SaveLoadComposite component to the ProjectContext prefab.</br>
[![](https://i124.fastpic.org/big/2024/0930/37/885de0100aaf9a99a38125cad1230137.png)](https://i124.fastpic.org/big/2024/0930/37/885de0100aaf9a99a38125cad1230137.png)
3. Also add this component to all SceneContext's on the scene.

This is the main setting that is necessary for the service to work.

Examples:

Registering the preservation server itself
```csharp
public override void InstallBindings()
{
  Container
    .BindInterfacesAndSelfTo<SaveLoadListener>()
    .AsSingle();
  
  Container
    .BindInterfacesTo<JsonSaveLoadService>()
    .AsSingle();
} 
```

**Example call**
```csharp
public class Example : MonoBehaviour
{
  private ISaveLoadService _saveLoadService;
  
  [Inject]
  private void Construct(ISaveLoadService saveLoadService)
  {
    _saveLoadService = saveLoadService;
  }
  
  private void Update()
  {
    if (Input.GetKeyDown(KeyCode.S))
      _saveLoadService.Save();
  
    if (Input.GetKeyDown(KeyCode.L)) 
      _saveLoadService.Load();
  }
}
```

**Load example**
```csharp
public class ExampleLoad : MonoBehaviour, IReadListener
{
  public void Read(ProgressData data)
  {
    // Reading data
  }
}
```

**Example of saving**
```csharp
public class ExampleSave : MonoBehaviour, IWriteListener
{
  public void Read(ProgressData data)
  {
    // Reading data
  }
  
  public void Write(ProgressData data)
  {
    // Write data
  }
}
```

**Important**: In order for data to be loaded and saved, objects that implement these interfaces must be registered in DiContainer Zenject

**Example**:
```csharp
public class Installer : MonoInstaller
{
  [SerializeField] private ExampleLoad _load;
  [SerializeField] private ExampleSave _save;
  
  public override void InstallBindings()
  {
    Container
      .BindInterfacesAndSelfTo<SaveLoadListener>()
      .AsSingle();
    
    Container
      .BindInterfacesTo<JsonSaveLoadService>()
      .AsSingle();
    
    Container
      .BindInterfacesTo<ExampleLoad>()
      .FromInstance(_load)
      .AsCached();
    
    Container
      .BindInterfacesTo<ExampleSave>()
      .FromInstance(_save)
      .AsCached();
  }
}
```

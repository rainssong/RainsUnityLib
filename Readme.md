# RainsUnityLib

自创或收集工具类

## Mono

* MonoBehaviourExtension

```csharp
GetAddComponent<T>(); 获取或添加组件  
BindFieldsWithChildren：自动获取子对象
SetActivateWithChildren：修改所有子对象Active
Invoke：延迟执行，允许非mono调用
```

* RainMonoBehaviour：自动绑定子对象
* ValueMono：带有change事件的Mono包裹器，可以手动绑定UnityEvent，可以实时抛出changeevent

## Event

* EventDispatcher：实现事件冒泡机制

## IO

* AssetLoadManager：支持json格式批量加载
* RequestManager：加载任意文件
* FileCore：文件读取
* KeyboardEventManager：管理键盘响应

## MVVM

* ValueListener \<T\>带有change事件的包裹器，可以实时抛出changeevent
* Binder:搭配ViewBase使用，自动绑定解绑Model中的ValueListener
* ViewBase <T\> 其中Model中属性用ValueListener包装，用Binder绑定
* MVObserver，观察所有已经绑定的model和view，v只能绑定1个m，m可以绑定多个v

## UI

* Alert：警告窗
* Animator：提供UI动画效果

## Utils

* CsvUtil
* ColorUtil
* GameObjectPool：Mono对象池
* ReflectionCore：反射工具
* SafePlayerPrefs：加密Pref
* Snapshoter：截图

# TODO

* Alert UI
* SettingUI
* LanguageText可以随时修改JSON内容，增加一个Save按钮

# BUG

LanguageText 存在不同语言配置全部被加载的情况，然而实际运行中，只会使用一个配置

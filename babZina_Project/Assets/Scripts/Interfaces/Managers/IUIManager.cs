//this empty line for UTF-8 BOM header
using System;

public interface IUIManager
{
    event Action OnSceneReload;
    event Action OnStackChanged;
}

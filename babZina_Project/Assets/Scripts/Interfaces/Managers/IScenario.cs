//this empty line for UTF-8 BOM header
using System;

public interface IScenario
{
    event Action<int, int> OnWin;
    event Action OnTutorialWin;
    event Action OnLoose;
}

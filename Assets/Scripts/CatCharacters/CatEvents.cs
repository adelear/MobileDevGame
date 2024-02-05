using System;

public static class CatEvents
{
    public static event Action OnOwnedCatNumValueChanged;

    public static void InvokeOwnedCatNumValueChanged(int ownedCatNum)
    {
        OnOwnedCatNumValueChanged?.Invoke();
    }
}

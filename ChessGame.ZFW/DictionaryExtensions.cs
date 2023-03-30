namespace ChessGame.ZFW;

public static class DictionaryExtensions
{
    public static TValue? GetValue<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key, TValue? defaultValue = default)
    {
        return dict.TryGetValue(key, out var value) ? value : defaultValue;
    }
}

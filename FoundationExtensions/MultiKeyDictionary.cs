using System.Collections;

namespace FoundationExtensions;

public class MultiKeyDictionary<TKey, TValue> : IEnumerable <KeyValuePair<(TKey ShortForm, TKey LongForm), TValue>>
{
  private readonly Dictionary<TKey, TValue> _key1Dictionary = new();
  private readonly Dictionary<TKey, TValue> _key2Dictionary = new();

  public void Add(TKey key1, TKey key2, TValue value)
  {
    if (key1.Equals(key2))
      throw new ArgumentException("Key1 and Key2 cannot be the same.");

    _key1Dictionary[key1] = value;
    _key2Dictionary[key2] = value;
  }

  public bool TryGetValue(TKey key, out TValue value)
  {
    if (_key1Dictionary.TryGetValue(key, out value))
      return true;

    else if (_key2Dictionary.TryGetValue(key, out value))
      return true;

    return false;
  }

  public TValue GetValue(TKey key)
  {
    if (!_key1Dictionary.ContainsKey(key) && !_key2Dictionary.ContainsKey(key))
      throw new KeyNotFoundException($"Key {key} not found.");

    return _key1Dictionary.ContainsKey(key) ? _key1Dictionary[key] : _key2Dictionary[key];
  }

  public IEnumerator<KeyValuePair<(TKey ShortForm, TKey LongForm), TValue>> GetEnumerator()
  {
    foreach (var (key1, value) in _key1Dictionary)
    {
      yield return new KeyValuePair<(TKey ShortForm, TKey LongForm), TValue>((key1, _key2Dictionary.FirstOrDefault(kvp => kvp.Value.Equals(value)).Key), value);
    }
  }

  IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
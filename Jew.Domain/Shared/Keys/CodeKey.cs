using System.Collections;

namespace Jew.Domain.Shared.Keys;

public readonly record struct CodeKey
{
    private readonly int _length = 0;
    private readonly string _key = string.Empty;
    public string Key 
    {
        get => _key;
        init
        {
            ArgumentOutOfRangeException.ThrowIfNotEqual(value.Length, Length, nameof(Key));
            _key = value;
        }
    }
    public int Length
    {
        get => _length;
        init
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(value, nameof(Length));
            _length = value;
        }
    }

    public CodeKey(int length, string key)
    {
        Length = length;    
        Key = key;
    }

    /// <summary>
    /// Generate a new uplated Key.
    /// </summary>
    /// <param name="lastKey">Last iterated key.</param>
    /// <returns></returns>
    /// <exception cref="OverflowException">If reached the limit throws this exception.</exception>
    public static string GenerateKey(CodeKey lastKey)
    {
        char[] chars = lastKey.Key.ToLower().ToCharArray();

        for (int i = chars.Length - 1; i >= 0; i--)
        {
            if (chars[i] < 'z')
            {
                chars[i]++;
                return new string(chars);
            }

            chars[i] = 'a';
        }

        // Si todo era 'z'
        throw new OverflowException("Se alcanzó el límite");
    }

    public bool SameType(CodeKey key)
    => key.Length == Length;


}
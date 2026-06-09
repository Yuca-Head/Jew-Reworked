using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Numerics;
using System.Text.RegularExpressions;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Jew.Avalonia.Shared;

public partial class NumberInput<T>(string input, T value) : ObservableObject where T : INumber<T>
{
    public NumberInput() :this("0", T.Zero){}

    [ObservableProperty]
    private string input = input;

    [ObservableProperty]
    private T value = value;

    partial void OnInputChanging(string? oldValue, string newValue)
    {
        if (newValue is null)
        {
            Value = T.Zero;
            return;
        }

        if (newValue.IndexOf('.') != newValue.LastIndexOf('.'))
        {
            Input = oldValue;
            return;
        }

        bool isFloatingPoint =
            typeof(T) == typeof(decimal) ||
            typeof(T) == typeof(double) ||
            typeof(T) == typeof(float);

        if (!isFloatingPoint && newValue.Contains('.'))
        {
            Input = oldValue;
            return;
        }

        if (!T.TryParse(newValue, CultureInfo.InvariantCulture, out var number))
        {
            // Permitir "123." mientras el usuario sigue escribiendo
            if (isFloatingPoint && newValue.EndsWith('.'))
                return;

            Input = oldValue;
            return;
        }

        Value = number;
    }
    partial void OnValueChanged(T? oldValue, T newValue)
    {
        if(oldValue != newValue)
            Input = newValue.ToString() ?? "0"; 
    }
    

    public static bool operator ==(NumberInput<T> a, NumberInput<T> b)
    {
        if (ReferenceEquals(a, b))
            return true;

        if (a is null || b is null)
            return false;

        return a.Equals(b);
    }
    public static bool operator !=(NumberInput<T> a, NumberInput<T> b)
    => !(a == b);
    public bool Equals(NumberInput<T> other)
    => this.Input == other.Input && this.Value == other.Value;

    public override bool Equals([NotNullWhen(true)] object? obj)
    => obj is NumberInput<T> other && Equals(other);

    public override int GetHashCode()
    => HashCode.Combine(Input, Value);
    
}

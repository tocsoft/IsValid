namespace IsValid
{
    public interface IValidatableValue<T>
    {
        bool IsValueSet { get; }
        T Value { get; }
    }
}
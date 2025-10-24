using PurchasingSystem.Domain.Shared.Errors;

public class Result<TValue>
{
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;

    private readonly TValue? _value;
    public TValue Value => IsSuccess
        ? _value!
        : throw new InvalidOperationException("Não é possível acessar o valor de um resultado de falha.");

    private readonly List<Error> _errors;
    public IReadOnlyCollection<Error> Errors => _errors;

    public Error FirstError => IsFailure
        ? _errors.First()
        : throw new InvalidOperationException("Não é possível acessar o primeiro erro de um resultado de sucesso.");

    protected Result(TValue value)
    {
        IsSuccess = true;
        _value = value;
        _errors = new List<Error>();
    }

    protected Result(List<Error> errors)
    {
        if (errors == null || errors.Count == 0)
            throw new ArgumentException("Uma falha deve conter ao menos um erro.", nameof(errors));

        IsSuccess = false;
        _value = default;
        _errors = errors;
    }

    public static Result<TValue> Success(TValue value) => new(value);
    public static Result<TValue> Failure(Error error) => new(new List<Error> { error });
    public static Result<TValue> Failure(List<Error> errors) => new(errors);

    public static implicit operator Result<TValue>(TValue value) => Success(value);
    public static implicit operator Result<TValue>(Error error) => Failure(error);
}

public class Result : Result<Result.Unit>
{
    public struct Unit { };
    private Result() : base(new Unit()) { }
    private Result(List<Error> errors) : base(errors) { }
    public static Result Success() => new();
    public static new Result Failure(Error error) => new(new List<Error> { error });
    public static new Result Failure(List<Error> errors) => new(errors);
}
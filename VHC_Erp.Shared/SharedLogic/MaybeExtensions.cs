namespace VHC_Erp.Shared.SharedLogic;

public static class MaybeExtensions
{
    /// <summary>
    /// Turn a value in a Maybe Monad
    /// </summary>
    /// <param name="value">Nullable value to turn into a Maybe Monad</param>
    /// <typeparam name="T">Generic type that can be anything</typeparam>
    /// <returns>A Maybe Monad with the type T</returns>
    /// <example>
    /// <code>
    /// var user = await _context.Users.FirstAsync(u => u.Id == Id);
    /// var userMaybe = user.ToMaybe(); //can be null
    /// </code>
    /// </example>
    public static Maybe<T> ToMaybe<T>(this T? value)
        => value is null || value is "" || value is 0 ? Maybe<T>.None("Value does not exists", 404) : Maybe<T>.Some(value);
    /// <summary>
    /// Function is an extension method of Monad instances.
    /// Gets a Monad of type T as an input, applies a predicate in it, and output a Monad with the result of type U.
    /// </summary>
    /// <param name="maybe">Maybe to extend</param>
    /// <param name="predicate">Predicate that gets the current Monad as an input and</param>
    /// <param name="errorMessage">String of the error message if it returns empty(value null)</param>
    /// <param name="errorCode">Http error code</param>
    /// <typeparam name="T">Type of the Maybe Input</typeparam>
    /// <typeparam name="U">Type of the Maybe that will come as an output</typeparam>
    /// <returns>The Maybe Monad with the type U</returns>
    /// <example>
    /// <code>
    /// var newValue = userMaybe.Then(user => new UserDto(user.Name, user.Age, user.Email), "Failed on instantiating UserDto record");
    /// </code>
    /// </example>
    public static Maybe<U> Then<T, U>(this Maybe<T> maybe, Func<T, Maybe<U>> predicate, string errorMessage, int errorCode) 
        => !maybe.Exists ? Maybe<U>.None(string.Join("\n", maybe.Error!), maybe.HttpCode) : predicate(maybe.Value).Exists ? predicate(maybe.Value) : Maybe<U>.None(errorMessage, errorCode);
    /// <summary>
    /// Function is an extension method of Monad instances.
    /// Gets a Monad of type T as an input, applies a predicate in it, and output a Monad with the result of type U.
    /// </summary>
    /// <param name="maybe">Maybe to extend</param>
    /// <param name="predicate">Predicate that gets the current Monad as an input and</param>
    /// <param name="action">Action to perform in error case</param>
    /// <typeparam name="T">Type of the Maybe Input</typeparam>
    /// <typeparam name="U">Type of the Maybe that will come as an output</typeparam>
    /// <returns>The Maybe Monad with the type U</returns>
    /// <example>
    /// <code>
    /// var newValue = userMaybe.Then(user => new UserDto(user.Name, user.Age, user.Email), () => YourErrorHandler());
    /// </code>
    /// </example>
    public static async Task<Maybe<U>> Then<T, U>(this Maybe<T> maybe, Func<T, Task<Maybe<U>>> predicate, Action action)
    {
        var newMaybe = await predicate(maybe.Value);
        if (!maybe.Exists || !newMaybe.Exists)
            action();
        return !maybe.Exists ? Maybe<U>.None(string.Join("\n", maybe.Error!), maybe.HttpCode) :
            newMaybe.Exists ? newMaybe : Maybe<U>.None(default!, default!);
    }

    /// <summary>
    /// Function is an extension method of Monad instances.
    /// Gets a Monad of type (Task: T) as an input, applies a predicate in it, and output a Monad with the result of type (Task: U).
    /// </summary>
    /// <param name="maybeTask">The Maybe of the type T that is a Task to extend</param>
    /// <param name="predicate">An async predicate that gets the current Monad as an input and</param>
    /// <param name="errorMessage">String of the error message if it returns empty(value null)</param>
    /// <param name="errorCode">Http error code</param>
    /// <typeparam name="T">Type of the Maybe Input</typeparam>
    /// <typeparam name="U">Type of the Maybe that will come as an output</typeparam>
    /// <returns>The Maybe Monad with the type U</returns>
    /// <example>
    /// <code>
    /// var newValue = await userMaybeTask.Then(user => new UserDto(user.Name, user.Age, user.Email), "Failed on instantiating UserDto record");
    /// </code>
    /// </example>
    public static async Task<Maybe<U>> Then<T, U>(this Task<Maybe<T>> maybeTask, Func<T, Maybe<U>> predicate, string errorMessage, int errorCode)
    {
        var maybe = await maybeTask;
        return !maybe.Exists ? Maybe<U>.None(string.Join("\n", maybe.Error!), maybe.HttpCode) : predicate(maybe.Value).Exists ? predicate(maybe.Value) : Maybe<U>.None(errorMessage, errorCode);
    }

    /// <summary>
    /// Function is an extension method of Monad instances.
    /// Gets a Monad of type T as an input, applies an async predicate in it, and output a Monad with the result of type (Task: U).
    /// </summary>
    /// <param name="maybe">Maybe to extend</param>
    /// <param name="predicate">An async predicate that gets the current Monad as an input</param>
    /// <param name="errorMessage">String of the error message if it returns empty(value null)</param>
    /// <param name="errorCode">Http error code</param>
    /// <typeparam name="T">Type of the Maybe Input</typeparam>
    /// <typeparam name="U">Type of the Maybe that will come as an output</typeparam>
    /// <returns>The Maybe Monad with the type U as a Task</returns>
    /// <example>
    /// <code>
    /// var newValue = await userMaybe.Then(async user => await CreateUserDtoAsync(user), "Failed on mapping user to DTO");
    /// </code>
    /// </example>
    public static async Task<Maybe<U>> Then<T, U>(this Maybe<T> maybe, Func<T, Task<Maybe<U>>> predicate, string errorMessage, int errorCode) 
    {
        if (!maybe.Exists) return Maybe<U>.None(string.Join("\n", maybe.Error!), maybe.HttpCode);
        var result = await predicate(maybe.Value);
        return result.Exists ? result : Maybe<U>.None(errorMessage, errorCode);
    }

    /// <summary>
    /// Function is an extension method of Monad instances.
    /// Gets a Monad of type (Task: T) as an input, applies an async predicate in it, and output a Monad with the result of type (Task: U).
    /// </summary>
    /// <param name="maybeTask">Maybe Task to extend</param>
    /// <param name="predicate">An async predicate that gets the current Monad as an input</param>
    /// <param name="errorMessage">String of the error message if it returns empty(value null)</param>
    /// <param name="errorCode">Http error code</param>
    /// <typeparam name="T">Type of the Maybe Input</typeparam>
    /// <typeparam name="U">Type of the Maybe that will come as an output</typeparam>
    /// <returns>The Maybe Monad with the type U as a Task</returns>
    /// <example>
    /// <code>
    /// var newValue = await userMaybeTask.Then(async user => await CreateUserDtoAsync(user), "Failed on mapping user to DTO");
    /// </code>
    /// </example>
    public static async Task<Maybe<U>> Then<T, U>(this Task<Maybe<T>> maybeTask, Func<T, Task<Maybe<U>>> predicate, string errorMessage, int errorCode) 
    {
        var maybe = await maybeTask;
        if (!maybe.Exists) return Maybe<U>.None(string.Join("\n", maybe.Error!), maybe.HttpCode);
        var result = await predicate(maybe.Value);
        return result.Exists ? result : Maybe<U>.None(errorMessage, errorCode);
    }

    /// <summary>
    /// Ensures that a predicate is true for the value in the Maybe, otherwise returns Error.
    /// </summary>
    /// <param name="maybe">Maybe to extend</param>
    /// <param name="predicate">Predicate to check against the Maybe value</param>
    /// <param name="errorMessage">Error message to use if the predicate fails</param>
    /// <param name="errorCode">Http error code</param>
    /// <typeparam name="T">Type of the Maybe</typeparam>
    /// <returns>The original Maybe if the predicate is true, otherwise Error</returns>
    /// <example>
    /// <code>
    /// var validUser = userMaybe.Assert(user => user.Age >= 18, "User must be 18 or older");
    /// </code>
    /// </example>
    public static Maybe<T> Assert<T>(this Maybe<T> maybe, Func<T, bool> predicate, string errorMessage, int errorCode)
        => !maybe.Exists ? maybe : predicate(maybe.Value) ? maybe : Maybe<T>.None(errorMessage, errorCode);
    
    /// <summary>
    /// Ensures that a predicate is true for the value in the Maybe, otherwise returns Error.
    /// </summary>
    /// <param name="maybe">Maybe to extend</param>
    /// <param name="predicate">Predicate to check against the Maybe value</param>
    /// <param name="action">Action to perform in error case</param>
    /// <typeparam name="T">Type of the Maybe</typeparam>
    /// <returns>The original Maybe if the predicate is true, otherwise Error</returns>
    /// <example>
    /// <code>
    /// var validUser = userMaybe.Assert(user => user.Age >= 18, () => YourErrorHandler());
    /// </code>
    /// </example>
    public static Maybe<T> Assert<T>(this Maybe<T> maybe, Func<T, bool> predicate, Action action)
    {
        var isTrue = predicate(maybe.Value);
        if (!isTrue)
            action();
        return !maybe.Exists ? maybe : isTrue ? maybe : Maybe<T>.None(default!, default!);
    }

    /// <summary>
    /// Ensures that an async predicate is true for the value in the Maybe, otherwise returns Error.
    /// </summary>
    /// <param name="maybe">Maybe to extend</param>
    /// <param name="predicate">Async predicate to check against the Maybe value</param>
    /// <param name="errorMessage">Error message to use if the predicate fails</param>
    /// <param name="errorCode">Http error code</param>
    /// <typeparam name="T">Type of the Maybe</typeparam>
    /// <returns>The original Maybe if the predicate is true, otherwise Error</returns>
    /// <example>
    /// <code>
    /// var validUser = await userMaybe.Assert(async user => await IsUserValidAsync(user), "User is not valid");
    /// </code>
    /// </example>
    public static async Task<Maybe<T>> Assert<T>(this Maybe<T> maybe, Func<T, Task<bool>> predicate, string errorMessage, int errorCode)
        => !maybe.Exists ? maybe : await predicate(maybe.Value) ? maybe : Maybe<T>.None(errorMessage, errorCode);

    /// <summary>
    /// Ensures that an async predicate is true for the value in the Maybe Task, otherwise returns Error.
    /// </summary>
    /// <param name="maybeTask">Maybe Task to extend</param>
    /// <param name="predicate">Async predicate to check against the Maybe value</param>
    /// <param name="errorMessage">Error message to use if the predicate fails</param>
    /// <param name="errorCode">Http error code</param>
    /// <typeparam name="T">Type of the Maybe</typeparam>
    /// <returns>The original Maybe if the predicate is true, otherwise Error</returns>
    /// <example>
    /// <code>
    /// var validUser = await userMaybeTask.Assert(async user => await IsUserValidAsync(user), "User is not valid");
    /// </code>
    /// </example>
    public static async Task<Maybe<T>> Assert<T>(this Task<Maybe<T>> maybeTask, Func<T, Task<bool>> predicate, string errorMessage, int errorCode)
    {
        var maybe = await maybeTask;
        return !maybe.Exists ? maybe : await predicate(maybe.Value) ? maybe : Maybe<T>.None(errorMessage, errorCode);
    }
}
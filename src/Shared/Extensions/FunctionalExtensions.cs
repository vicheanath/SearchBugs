namespace Shared.Extensions;

/// <summary>
/// Contains extension methods for supporting some functional patterns.
/// </summary>
public static class FunctionalExtensions
{
    /// <summary>
    /// Performs the specified action and returns the same instance.
    /// </summary>
    /// <typeparam name="T">The instance type.</typeparam>
    /// <param name="instance">The instance.</param>
    /// <param name="action">The action to perform.</param>
    /// <returns>The same instance.</returns>
    public static T Tap<T>(this T instance, Action action)
    {
        action();

        return instance;
    }

    /// <summary>
    /// Performs the specified action with the current instance and returns the same instance.
    /// </summary>
    /// <typeparam name="T">The instance type.</typeparam>
    /// <param name="instance">The instance.</param>
    /// <param name="action">The action to perform.</param>
    /// <returns>The same instance.</returns>
    public static T Tap<T>(this T instance, Action<T> action)
    {
        action(instance);

        return instance;
    }

    /// <summary>
    /// Invokes the specified action for each element in the collection.
    /// </summary>
    /// <typeparam name="T">The collection type.</typeparam>
    /// <param name="collection">The collection.</param>
    /// <param name="action">The action to invoke for each element.</param>
    public static void ForEach<T>(this IEnumerable<T> collection, Action<T> action)
    {
        foreach (T element in collection)
        {
            action(element);
        }
    }

    /// <summary>
    /// Executes a try-catch-finally block with the specified actions.
    /// </summary>
    /// <param name="action">The action in the try block.</param>
    /// <param name="catchAction">The action in the catch block.</param>
    /// <param name="finallyAction">The action in the finally block.</param>
    public static void TryCatchFinally(Action action, Action<Exception> catchAction, Action finallyAction)
    {
        try
        {
            action();
        }
        catch (Exception exception)
        {
            catchAction(exception);
        }
        finally
        {
            finallyAction();
        }
    }

    /// <summary>
    /// Executes a try-catch-finally block with the specified actions.
    /// </summary>
    /// <param name="action">The action in the try block.</param>
    /// <param name="catchAction">The action in the catch block.</param>
    public static void TryCatch(Action action, Action<Exception> catchAction)
    {
        try
        {
            action();
        }
        catch (Exception exception)
        {
            catchAction(exception);
        }
    }
}

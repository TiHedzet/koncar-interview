namespace Koncar.Interview.Client.Presentation.Tui.Presenters;

using Koncar.Interview.Client.Presentation.Tui.Contracts.Views;
using Koncar.Interview.Client.Presentation.Tui.Internal;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

internal abstract class PresenterBase
{
    protected CancellationTokenSource _cts;
    protected readonly ILogger _logger;

    protected PresenterBase(ILogger logger)
    {
        _logger = logger;
        _cts = new();
    }

    protected async Task HandleEvent(
       [NotNull] Func<CancellationToken, Task> handler,
       ILoadableView? loader = null,
       [CallerArgumentExpression(nameof(handler))] string? handlerName = null)
    {
        try
        {
            CancellationTokenSource oldSource = Interlocked.Exchange(
                ref _cts,
                CancellationTokenProvider.Create());
            
            oldSource.Cancel();
            loader?.LoadingStarted();
            
            await handler.Invoke(_cts.Token);
        }
        catch (OperationCanceledException)
        {
            _logger.LogCancellation(handlerName!);
        }
        catch (Exception ex)
        {
            _logger.LogUnhandledError(ex);
        }
        finally
        {
            loader?.LoadingFinished();
        }
    }

    protected async Task HandleEvent<T>(
        [NotNull] Func<T, CancellationToken, Task> handler,
        T argument,
        ILoadableView? loader = null,
        [CallerArgumentExpression(nameof(handler))] string? handlerName = null)
    {
        try
        {
            CancellationTokenSource oldSource = Interlocked.Exchange(
                ref _cts,
                CancellationTokenProvider.Create());
            
            oldSource.Cancel();
            loader?.LoadingStarted();
            
            await handler.Invoke(argument, _cts.Token);
        }
        catch (OperationCanceledException)
        {
            _logger.LogCancellation(handlerName!);
        }
        catch (ApplicationException ex)
        {
            _logger.LogUnhandledError(ex);
        }
        finally
        {
            loader?.LoadingFinished();
        }
    }

    protected abstract void MapHandlers();
}

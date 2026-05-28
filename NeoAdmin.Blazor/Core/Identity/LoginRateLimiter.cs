using System.Collections.Concurrent;

namespace NeoAdmin.Blazor.Core.Identity;

/// <summary>
/// 按客户端 IP 限制登录失败次数（与 <c>api/login@Login</c> 行为一致）。
/// </summary>
public sealed class LoginRateLimiter
{
    private const int MaxFailedAttempts = 5;
    private static readonly TimeSpan Cooldown = TimeSpan.FromSeconds(60);

    private readonly ConcurrentDictionary<string, int> _attempts = new();

    public bool IsBlocked(string clientIp, out string? message)
    {
        message = null;
        if (_attempts.TryGetValue(clientIp, out int count) && count >= MaxFailedAttempts)
        {
            message = $"{clientIp} 操作频率过高，请稍后再试...";
            return true;
        }

        return false;
    }

    public int RecordFailure(string clientIp)
    {
        int count = _attempts.AddOrUpdate(clientIp, 1, (_, oldValue) => oldValue + 1);
        _ = Task.Run(async () =>
        {
            await Task.Delay(Cooldown);
            _attempts.TryRemove(clientIp, out _);
        });
        return count;
    }
}

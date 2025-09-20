namespace G_Task.Domain.Common;

/// <summary>
/// در صورت وجود نام کاربری و پسور
/// </summary>
public interface IUser
{
    string UserName { get; }
    string Password { get; }
}
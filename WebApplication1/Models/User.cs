using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

/// <summary>
/// Пользователь.
/// </summary>
public class User:IComparable<User>
{
    /// <summary>
    /// Имя пользователя.
    /// </summary>
    [Required]
    public string UserName
    {
        get;
        set;
    }

    /// <summary>
    /// Почта пользователя.
    /// </summary>
    [Required]
    public string Email
    {
        get;
        set;
    }

    /// <summary>
    /// Сравнение пользователей.
    /// </summary>
    /// <param name="other"> Другой пользователь. </param>
    /// <returns> Больший пользователь. </returns>
    public int CompareTo(User other)
    {
        if (other == null)
            return 1;

        else
            return this.Email.CompareTo(other.Email);
    }
}
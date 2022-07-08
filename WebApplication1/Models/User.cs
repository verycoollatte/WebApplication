using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

/// <summary>
/// ������������.
/// </summary>
public class User:IComparable<User>
{
    /// <summary>
    /// ��� ������������.
    /// </summary>
    [Required]
    public string UserName
    {
        get;
        set;
    }

    /// <summary>
    /// ����� ������������.
    /// </summary>
    [Required]
    public string Email
    {
        get;
        set;
    }

    /// <summary>
    /// ��������� �������������.
    /// </summary>
    /// <param name="other"> ������ ������������. </param>
    /// <returns> ������� ������������. </returns>
    public int CompareTo(User other)
    {
        if (other == null)
            return 1;

        else
            return this.Email.CompareTo(other.Email);
    }
}
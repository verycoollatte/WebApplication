using System;
using System.ComponentModel.DataAnnotations;


/// <summary>
/// ���������.
/// </summary>
public class TextMessage
{

    /// <summary>
    /// ���� ���������.
    /// </summary>
    [Required]
    public string Subject
    {
        get;
        set;
    }

    /// <summary>
    /// ����� ���������.
    /// </summary>
    [Required]
    public string Message
    {
        get;
        set;
    }

    /// <summary>
    /// E-mail �����������.
    /// </summary>
    [Required]
    public string SenderId
    {
        get;
        set;
    }


    /// <summary>
    /// E-mail ��������.
    /// </summary>
    [Required]
    public string ReceiverId
    {
        get;
        set;
    }

}
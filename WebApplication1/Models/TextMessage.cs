using System;
using System.ComponentModel.DataAnnotations;


/// <summary>
/// Сообщение.
/// </summary>
public class TextMessage
{

    /// <summary>
    /// Тема сообщения.
    /// </summary>
    [Required]
    public string Subject
    {
        get;
        set;
    }

    /// <summary>
    /// Текст сообщения.
    /// </summary>
    [Required]
    public string Message
    {
        get;
        set;
    }

    /// <summary>
    /// E-mail отправителя.
    /// </summary>
    [Required]
    public string SenderId
    {
        get;
        set;
    }


    /// <summary>
    /// E-mail адресата.
    /// </summary>
    [Required]
    public string ReceiverId
    {
        get;
        set;
    }

}
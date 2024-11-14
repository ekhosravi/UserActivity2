using System;
using System.Collections.Generic;
using UserActivity.Models.Models;

namespace UserActivity.Models;

public partial class UserMessage
{
    public int Id { get; set; }

    /// <summary>
    /// Foreign key to AspNetUsers.Id, identifies the user who sent the message.
    /// </summary>
    public int? SenderId { get; set; }

    /// <summary>
    /// Foreign key to AspNetUsers.Id, identifies the user who received the message.
    /// </summary>
    public int? ReceiverId { get; set; }

    public string? ContentMsg { get; set; }

    public int? MsgTypeId { get; set; }

    public DateTime? MsgDate { get; set; }

    /// <summary>
    /// Boolean indicating if the message was read (1) or unread (0)
    /// </summary>
    public bool? IsViewed { get; set; }

    public virtual UserMessagesType? MsgType { get; set; }

    public virtual AspNetUser? Receiver { get; set; }

    public virtual AspNetUser? Sender { get; set; }
}

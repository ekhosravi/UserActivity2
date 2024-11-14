using System;
using System.Collections.Generic;

namespace UserActivity.Models;

public partial class UserMessagesType
{
    public int Id { get; set; }

    /// <summary>
    /// Name of the interaction type (e.g., &apos;Like&apos;, &apos;Follow&apos;)
    /// </summary>
    public string? MsgType { get; set; }

    public virtual ICollection<UserMessage> UserMessages { get; set; } = new List<UserMessage>();
}

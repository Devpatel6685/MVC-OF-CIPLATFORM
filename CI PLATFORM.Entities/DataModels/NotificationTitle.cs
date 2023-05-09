using System;
using System.Collections.Generic;

namespace CI_PLATFORM.Entities.DataModels;

public partial class NotificationTitle
{
    public long NotificationId { get; set; }

    public string? Title { get; set; }

    public virtual ICollection<EnableUserStatus> EnableUserStatuses { get; } = new List<EnableUserStatus>();

    public virtual ICollection<MessageTable> MessageTables { get; } = new List<MessageTable>();
}

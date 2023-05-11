using System;
using System.Collections.Generic;

namespace MVC_OF_CI_PLATFORM.DataModels;

public partial class MessageTable
{
    public int MessageId { get; set; }

    public long? NotificationId { get; set; }

    public string? Message { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int Seen { get; set; }

    public string? Url { get; set; }

    public string? AvatarUser { get; set; }

    public virtual NotificationTitle? Notification { get; set; }

    public virtual ICollection<Userpermission> Userpermissions { get; } = new List<Userpermission>();
}

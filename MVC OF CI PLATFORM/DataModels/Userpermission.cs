using System;
using System.Collections.Generic;

namespace MVC_OF_CI_PLATFORM.DataModels;

public partial class Userpermission
{
    public int UserpermissionId { get; set; }

    public long? UserId { get; set; }

    public int? Status { get; set; }

    public int? MessageId { get; set; }

    public virtual MessageTable? Message { get; set; }

    public virtual User? User { get; set; }
}

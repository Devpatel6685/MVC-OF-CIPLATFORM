﻿using System;
using System.Collections.Generic;

namespace MVC_OF_CI_PLATFORM.DataModels;

public partial class MissionSkill
{
    public long MissionSkillId { get; set; }

    public int SkillId { get; set; }

    public long? MissionId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public virtual Mission? Mission { get; set; }

    public virtual Skill Skill { get; set; } = null!;
}

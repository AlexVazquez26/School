using System;
using System.Collections.Generic;

namespace AccesoDatos.Models;

public partial class Application
{
    public int AppId { get; set; }

    public string Name { get; set; } = null!;

    public DateTime? LastChangeDate { get; set; }

    public bool Certified { get; set; }

    public int Idapptype { get; set; }

    public int Idarchtype { get; set; }

    public int IdbusinessOwner { get; set; }

    public int Iddepartment { get; set; }

    public int Idlifecyclestage { get; set; }

    public string Description { get; set; } = null!;

    public string Businesscritically { get; set; } = null!;

    public string Technologystack { get; set; } = null!;

    public string Userbase { get; set; } = null!;

    public string Platform { get; set; } = null!;

    public string Status { get; set; } = null!;
}

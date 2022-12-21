using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetKingdomFN.Models;

public partial class ServiceOption
{
    public string Id { get; set; } = null!;

    public string? PetServiceId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public int? EstimatedCompletionTime { get; set; }

    public int? Status { get; set; }
    [NotMapped]
    public string PetServiceName { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public virtual PetService? PetService { get; set; }

    public virtual ICollection<ScheduleAvailable> ScheduleAvailables { get; } = new List<ScheduleAvailable>();

    public virtual ICollection<Schedule> Schedules { get; } = new List<Schedule>();

    public virtual ICollection<ServiceSellPrice> ServiceSellPrices { get; } = new List<ServiceSellPrice>();
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TaskManagement.Models
{
    public class Member
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MemberId { get; set; }

        [Required]
        [StringLength(100)]
        public string MemberName { get; set; }

        public virtual ICollection<TaskAssignment> TaskAssignments { get; set; }
    }

    public class AppTask
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TaskId { get; set; }

        [Required]
        [StringLength(150)]
        public string TaskName { get; set; }

        public double ExpectedHours { get; set; }

        public virtual ICollection<TaskAssignment> TaskAssignments { get; set; }
    }

    public class TaskAssignment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AssignmentId { get; set; }

        [ForeignKey("Member")]
        public int MemberId { get; set; }

        [ForeignKey("Task")]
        public int TaskId { get; set; }

        public double CompletedHours { get; set; }
        public double RemainingHours { get; set; }

        public TaskStatus Status { get; set; }

        public virtual Member Member { get; set; }
        public virtual AppTask Task { get; set; }
    }

    public enum TaskStatus
    {
        Proposed,
        Active,
        Resolved
    }
}
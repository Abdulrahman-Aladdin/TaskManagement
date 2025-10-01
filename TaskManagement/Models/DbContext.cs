using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TaskManagement.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() : base("DefaultConnection")
        {
        }

        public DbSet<Member> Members { get; set; }
        public DbSet<AppTask> Tasks { get; set; }
        public DbSet<TaskAssignment> TaskAssignments { get; set; }
    }

    public class AppDbInitializer : DropCreateDatabaseIfModelChanges<AppDbContext>
    {
        protected override void Seed(AppDbContext context)
        {
            var members = GetMembers();
            members.ForEach(m => context.Members.Add(m));
            context.SaveChanges();

            var tasks = GetTasks();
            tasks.ForEach(t => context.Tasks.Add(t));
            context.SaveChanges();

            var assignments = GetTaskAssignments(members, tasks);
            assignments.ForEach(a => context.TaskAssignments.Add(a));
            context.SaveChanges();
        }

        private List<Member> GetMembers()
        {
            return new List<Member>
            {
                new Member { MemberName = "Alice Johnson" },
                new Member { MemberName = "Bob Smith" },
                new Member { MemberName = "Charlie Brown" }
            };
        }

        private List<AppTask> GetTasks()
        {
            return new List<AppTask>
            {
                new AppTask { TaskName = "Build API", ExpectedHours = 40 },
                new AppTask { TaskName = "Frontend UI", ExpectedHours = 30 },
                new AppTask { TaskName = "Database Design", ExpectedHours = 20 }
            };
        }

        private List<TaskAssignment> GetTaskAssignments(List<Member> members, List<AppTask> tasks)
        {
            return new List<TaskAssignment>
            {
                new TaskAssignment
                {
                    MemberId = members[0].MemberId,
                    TaskId = tasks[0].TaskId,
                    CompletedHours = tasks[0].ExpectedHours,
                    RemainingHours = 0,
                    Status = TaskStatus.Resolved
                },
                new TaskAssignment
                {
                    MemberId = members[0].MemberId,
                    TaskId = tasks[1].TaskId,
                    CompletedHours = 10,
                    RemainingHours = 20,
                    Status = TaskStatus.Active
                },
                new TaskAssignment
                {
                    MemberId = members[0].MemberId,
                    TaskId = tasks[2].TaskId,
                    CompletedHours = 0,
                    RemainingHours = 20,
                    Status = TaskStatus.Proposed
                },

                new TaskAssignment
                {
                    MemberId = members[1].MemberId,
                    TaskId = tasks[0].TaskId,
                    CompletedHours = 40,
                    RemainingHours = 0,
                    Status = TaskStatus.Resolved
                },
                new TaskAssignment
                {
                    MemberId = members[1].MemberId,
                    TaskId = tasks[1].TaskId,
                    CompletedHours = 25,
                    RemainingHours = 5,
                    Status = TaskStatus.Active
                },
                new TaskAssignment
                {
                    MemberId = members[1].MemberId,
                    TaskId = tasks[2].TaskId,
                    CompletedHours = 0,
                    RemainingHours = 20,
                    Status = TaskStatus.Proposed
                },
                new TaskAssignment
                {
                    MemberId = members[2].MemberId,
                    TaskId = tasks[0].TaskId,
                    CompletedHours = 40,
                    RemainingHours = 0,
                    Status = TaskStatus.Resolved
                },
                new TaskAssignment
                {
                    MemberId = members[2].MemberId,
                    TaskId = tasks[1].TaskId,
                    CompletedHours = 30,
                    RemainingHours = 0,
                    Status = TaskStatus.Resolved
                },
                new TaskAssignment
                {
                    MemberId = members[2].MemberId,
                    TaskId = tasks[2].TaskId,
                    CompletedHours = 5,
                    RemainingHours = 15,
                    Status = TaskStatus.Active
                }
            };
        }
    }
}
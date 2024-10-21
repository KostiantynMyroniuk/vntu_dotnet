using Kuddn.TaskManager.Domain.Models.ENUMS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kuddn.TaskManager.Domain.Models
{
    public class WorkItem
    {
        
        public Guid Id { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime DueDate { get; set; }
        public Priority Priority { get; set; }
        public Complexity Complexity { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsComplited { get; set; }

        public string ToString()
        {
            string flag = (IsComplited) ? "Complited" : "In Process";
            return $"{Title}: due {DueDate:dd.MM.yyyy}, priority: {Priority.ToString().ToLower()}, {flag} \n";

        }

        public WorkItem Clone()
        {
            return new WorkItem()
            {
                Id = Guid.NewGuid(),
                Title = Title,
                Description = Description,
                Complexity = Complexity,
                CreationDate = CreationDate,
                DueDate = DueDate,
                Priority = Priority,
                IsComplited = IsComplited

            };
        }
    }
}

using Kuddn.TaskManager.DataAccess;
using Kuddn.TaskManager.DataAccess.Abstractions;
using Kuddn.TaskManager.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Kuddn.TaskManager.Domain.Logic
{
    public class SimpleTaskManager
    {

        private IWorkItemsRepository _workItemRepository;
        public SimpleTaskManager(IWorkItemsRepository workItemRepository)
        {
            _workItemRepository = workItemRepository;
        }

        public WorkItem[] CreatePlan()
        {
            var result = _workItemRepository.GetAll().ToList();
            result.Sort(CompareWorkItems);
            return result.Where(item => !item.IsComplited).ToArray();
        }

        public int CompareWorkItems(WorkItem workItem1, WorkItem workItem2)
        {
            var priority = workItem2.Priority.CompareTo(workItem1.Priority);
            if (priority != 0)
            {
                return priority;
            }

            var dueDate = workItem1.DueDate.CompareTo(workItem2.DueDate);
            if (dueDate != 0)
            {
                return dueDate;
            }

            return string.Compare(workItem1.Title, workItem2.Title, StringComparison.OrdinalIgnoreCase);
        }
    }
}

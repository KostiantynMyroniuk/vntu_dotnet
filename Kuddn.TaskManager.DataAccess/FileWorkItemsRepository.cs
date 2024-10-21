using Kuddn.TaskManager.DataAccess.Abstractions;
using Kuddn.TaskManager.Domain.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Kuddn.TaskManager.DataAccess
{
    public class FileWorkItemsRepository : IWorkItemsRepository
    {
        private const string FileName = "work-items.json";
        private List<WorkItem> _workItems;
        private readonly Dictionary<Guid, WorkItem> _workItemsDictionary;
        

        public FileWorkItemsRepository() 
        {
            _workItems = ReadFromFile();
            _workItemsDictionary = _workItems.ToDictionary(item => item.Id, item => item);
        }


        private List<WorkItem> ReadFromFile()
        {
            if (!(File.Exists(FileName))) {
                return new List<WorkItem>();
            }

            var json = File.ReadAllText(FileName);

            if (string.IsNullOrWhiteSpace(json))
            {
                return new List<WorkItem>();
            }

            return JsonConvert.DeserializeObject<List<WorkItem>>(json) ?? new List<WorkItem>();
        }

        public Guid Add(WorkItem workItem)
        {
            WorkItem workItemClone = workItem.Clone();
            _workItemsDictionary.Add(workItemClone.Id, workItemClone);
            SaveChanges();
            return workItemClone.Id;
        }

        public WorkItem Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public WorkItem[] GetAll()
        {

            return _workItemsDictionary.Values.ToArray();

        }

        public bool Remove(Guid id)
        {
            if (_workItemsDictionary.Remove(id))
            {
                SaveChanges();
                return true;
            }
            return false;

            
        }

        public void SaveChanges()
        {
            var array = _workItemsDictionary.Values.ToArray();
            var json = JsonConvert.SerializeObject(array, Formatting.Indented);
            File.WriteAllText(FileName, json);
        }

        public bool Update(WorkItem workItem)
        {
            throw new NotImplementedException();
        }
    }
}

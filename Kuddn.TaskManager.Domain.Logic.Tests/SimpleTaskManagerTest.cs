using Kuddn.TaskManager.DataAccess.Abstractions;
using Kuddn.TaskManager.Domain.Models.ENUMS;
using Kuddn.TaskManager.Domain.Models;
using Moq;


namespace Kuddn.TaskManager.Domain.Logic.Tests
{
    public class SimpleTaskManagerTest
    {
        [Fact]
        public void CreatePlanReturnsSortedWorkItems()
        {

            var mockRepository = new Mock<IWorkItemsRepository>();


            var workItems = new List<WorkItem>
            {
                new WorkItem { Title = "Task A", Priority = Priority.Medium, DueDate = DateTime.Now.AddDays(2), IsComplited = false },
                new WorkItem { Title = "Task B", Priority = Priority.High, DueDate = DateTime.Now.AddDays(1), IsComplited = false },
                new WorkItem { Title = "Task C", Priority = Priority.High, DueDate = DateTime.Now.AddDays(3), IsComplited = false }
            };


            mockRepository.Setup(repo => repo.GetAll()).Returns(workItems.ToArray());

            var planner = new SimpleTaskManager(mockRepository.Object);


            var result = planner.CreatePlan();


            Assert.Equal(3, result.Length);
            Assert.Equal("Task B", result[0].Title);
            Assert.Equal("Task C", result[1].Title);
            Assert.Equal("Task A", result[2].Title);
        }

        [Fact]
        public void CreatePlanWithOnlyRelevantWorkItems()
        {
            var mockRepository = new Mock<IWorkItemsRepository>();

            var workItems = new List<WorkItem>
            {
                new WorkItem { Title = "Task A", Priority = Priority.Medium, DueDate = DateTime.Now.AddDays(2), IsComplited = false },
                new WorkItem { Title = "Task B", Priority = Priority.High, DueDate = DateTime.Now.AddDays(1), IsComplited = true },
                new WorkItem { Title = "Task C", Priority = Priority.High, DueDate = DateTime.Now.AddDays(3), IsComplited = false }
            };

            mockRepository.Setup(repo => repo.GetAll()).Returns(workItems.ToArray());

            var planner = new SimpleTaskManager(mockRepository.Object);

            var result = planner.CreatePlan();

            Assert.Equal(2, result.Length);
            Assert.Contains(result, item => item.Title == "Task A");
            Assert.Contains(result, item => item.Title == "Task C");
        }

        [Fact]
        public void CreatePlanWithOutCompletedWorkItems()
        {
            var mockRepository = new Mock<IWorkItemsRepository>();

            var workItems = new List<WorkItem>
            {
                new WorkItem { Title = "Task A", Priority = Priority.Medium, DueDate = DateTime.Now.AddDays(2), IsComplited = true },
                new WorkItem { Title = "Task B", Priority = Priority.High, DueDate = DateTime.Now.AddDays(1), IsComplited = false },
                new WorkItem { Title = "Task C", Priority = Priority.High, DueDate = DateTime.Now.AddDays(3), IsComplited = false }
            };

            mockRepository.Setup(repo => repo.GetAll()).Returns(workItems.ToArray());

            var planner = new SimpleTaskManager(mockRepository.Object);

            var result = planner.CreatePlan();

            Assert.Equal(2, result.Length);
            Assert.DoesNotContain(result, item => item.Title == "Task A");
        }
    }
}
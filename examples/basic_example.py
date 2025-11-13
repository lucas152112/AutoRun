"""
Example: Basic Task Scheduling

This example demonstrates how to create and schedule basic tasks.
"""

from autorun.scheduler import Task, Scheduler
import logging

# Setup logging
logging.basicConfig(
    level=logging.INFO,
    format='%(asctime)s - %(name)s - %(levelname)s - %(message)s'
)


def example_task_1():
    """Example task that prints a message."""
    print("Example Task 1 is running!")
    logging.info("Task 1 executed successfully")


def example_task_2():
    """Example task that performs a simple calculation."""
    result = sum(range(100))
    print(f"Example Task 2 calculated: {result}")
    logging.info(f"Task 2 executed with result: {result}")


def main():
    """Main function to run the example."""
    # Create scheduler
    scheduler = Scheduler()
    
    # Create tasks
    task1 = Task(
        name="example_task_1",
        func=example_task_1,
        schedule="every 1 hour"
    )
    
    task2 = Task(
        name="example_task_2",
        func=example_task_2,
        schedule="every 30 minutes"
    )
    
    # Add tasks to scheduler
    scheduler.add_task(task1)
    scheduler.add_task(task2)
    
    # List all tasks
    print("\nScheduled Tasks:")
    for task in scheduler.list_tasks():
        print(f"  - {task}")
    
    # Execute tasks manually for demonstration
    print("\nExecuting tasks manually:")
    task1.execute()
    task2.execute()
    
    print("\nScheduler example completed!")


if __name__ == "__main__":
    main()

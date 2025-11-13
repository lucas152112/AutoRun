"""
Unit tests for the Task and Scheduler classes.
"""

import pytest
from datetime import datetime
from autorun.scheduler import Task, Scheduler


def sample_task_function():
    """Sample task function for testing."""
    return "Task executed"


class TestTask:
    """Test cases for Task class."""
    
    def test_task_creation(self):
        """Test creating a new task."""
        task = Task("test_task", sample_task_function, "daily")
        assert task.name == "test_task"
        assert task.schedule == "daily"
        assert task.enabled is True
        assert task.last_run is None
        
    def test_task_execution(self):
        """Test task execution."""
        task = Task("test_task", sample_task_function, "daily")
        result = task.execute()
        assert result is True
        assert task.last_run is not None
        assert isinstance(task.last_run, datetime)
        
    def test_task_execution_failure(self):
        """Test task execution with failure."""
        def failing_function():
            raise Exception("Test error")
            
        task = Task("failing_task", failing_function, "daily")
        result = task.execute()
        assert result is False


class TestScheduler:
    """Test cases for Scheduler class."""
    
    def test_scheduler_creation(self):
        """Test creating a new scheduler."""
        scheduler = Scheduler()
        assert scheduler.running is False
        assert len(scheduler.tasks) == 0
        
    def test_add_task(self):
        """Test adding a task to scheduler."""
        scheduler = Scheduler()
        task = Task("test_task", sample_task_function, "daily")
        scheduler.add_task(task)
        assert len(scheduler.tasks) == 1
        assert scheduler.tasks[0].name == "test_task"
        
    def test_remove_task(self):
        """Test removing a task from scheduler."""
        scheduler = Scheduler()
        task = Task("test_task", sample_task_function, "daily")
        scheduler.add_task(task)
        
        result = scheduler.remove_task("test_task")
        assert result is True
        assert len(scheduler.tasks) == 0
        
        result = scheduler.remove_task("nonexistent")
        assert result is False
        
    def test_get_task(self):
        """Test getting a task by name."""
        scheduler = Scheduler()
        task = Task("test_task", sample_task_function, "daily")
        scheduler.add_task(task)
        
        found_task = scheduler.get_task("test_task")
        assert found_task is not None
        assert found_task.name == "test_task"
        
        not_found = scheduler.get_task("nonexistent")
        assert not_found is None
        
    def test_list_tasks(self):
        """Test listing all tasks."""
        scheduler = Scheduler()
        task1 = Task("task1", sample_task_function, "daily")
        task2 = Task("task2", sample_task_function, "hourly")
        
        scheduler.add_task(task1)
        scheduler.add_task(task2)
        
        tasks = scheduler.list_tasks()
        assert len(tasks) == 2
        assert tasks[0].name == "task1"
        assert tasks[1].name == "task2"
        
    def test_start_stop(self):
        """Test starting and stopping the scheduler."""
        scheduler = Scheduler()
        assert scheduler.running is False
        
        scheduler.start()
        assert scheduler.running is True
        
        scheduler.stop()
        assert scheduler.running is False

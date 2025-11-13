"""
Task Scheduler Module

This module handles the scheduling and execution of automated tasks.
"""

import logging
from datetime import datetime
from typing import List, Dict, Callable, Optional

logger = logging.getLogger(__name__)


class Task:
    """Represents a scheduled task."""
    
    def __init__(self, name: str, func: Callable, schedule: str, enabled: bool = True):
        """
        Initialize a task.
        
        Args:
            name: Task name
            func: Function to execute
            schedule: Schedule in cron-like format or time interval
            enabled: Whether the task is enabled
        """
        self.name = name
        self.func = func
        self.schedule = schedule
        self.enabled = enabled
        self.last_run: Optional[datetime] = None
        self.next_run: Optional[datetime] = None
        
    def execute(self) -> bool:
        """
        Execute the task.
        
        Returns:
            True if execution was successful, False otherwise
        """
        try:
            logger.info(f"Executing task: {self.name}")
            self.func()
            self.last_run = datetime.now()
            return True
        except Exception as e:
            logger.error(f"Error executing task {self.name}: {e}")
            return False
    
    def __repr__(self) -> str:
        return f"Task(name={self.name}, schedule={self.schedule}, enabled={self.enabled})"


class Scheduler:
    """Main scheduler for managing and executing tasks."""
    
    def __init__(self):
        """Initialize the scheduler."""
        self.tasks: List[Task] = []
        self.running = False
        
    def add_task(self, task: Task) -> None:
        """
        Add a task to the scheduler.
        
        Args:
            task: Task to add
        """
        self.tasks.append(task)
        logger.info(f"Added task: {task.name}")
        
    def remove_task(self, task_name: str) -> bool:
        """
        Remove a task from the scheduler.
        
        Args:
            task_name: Name of the task to remove
            
        Returns:
            True if task was removed, False if not found
        """
        for i, task in enumerate(self.tasks):
            if task.name == task_name:
                self.tasks.pop(i)
                logger.info(f"Removed task: {task_name}")
                return True
        logger.warning(f"Task not found: {task_name}")
        return False
        
    def get_task(self, task_name: str) -> Optional[Task]:
        """
        Get a task by name.
        
        Args:
            task_name: Name of the task
            
        Returns:
            Task if found, None otherwise
        """
        for task in self.tasks:
            if task.name == task_name:
                return task
        return None
        
    def list_tasks(self) -> List[Task]:
        """
        Get all tasks.
        
        Returns:
            List of all tasks
        """
        return self.tasks.copy()
        
    def start(self) -> None:
        """Start the scheduler."""
        self.running = True
        logger.info("Scheduler started")
        
    def stop(self) -> None:
        """Stop the scheduler."""
        self.running = False
        logger.info("Scheduler stopped")

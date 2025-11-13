# AutoRun Documentation

## Table of Contents

1. [Introduction](#introduction)
2. [Installation](#installation)
3. [Core Concepts](#core-concepts)
4. [API Reference](#api-reference)
5. [Examples](#examples)

## Introduction

AutoRun is a Windows Task Scheduler alternative designed to provide reliable and flexible task automation.

## Installation

### Requirements

- Python 3.8 or higher
- Windows Operating System

### Install from Source

```bash
git clone https://github.com/lucas152112/AutoRun.git
cd AutoRun
pip install -e .
```

## Core Concepts

### Tasks

A Task represents a scheduled job that needs to be executed. Each task has:
- **name**: Unique identifier for the task
- **func**: The function to execute
- **schedule**: When to run the task
- **enabled**: Whether the task is active

### Scheduler

The Scheduler manages all tasks and handles their execution. It provides:
- Task registration and removal
- Task execution management
- State management (start/stop)

### Configuration

Configuration is managed through JSON files and provides:
- Logging settings
- Execution parameters
- Retry logic

## API Reference

### Task Class

```python
class Task:
    def __init__(self, name: str, func: Callable, schedule: str, enabled: bool = True)
    def execute(self) -> bool
```

### Scheduler Class

```python
class Scheduler:
    def __init__(self)
    def add_task(self, task: Task) -> None
    def remove_task(self, task_name: str) -> bool
    def get_task(self, task_name: str) -> Optional[Task]
    def list_tasks(self) -> List[Task]
    def start(self) -> None
    def stop(self) -> None
```

### Config Class

```python
class Config:
    def __init__(self, config_path: Optional[str] = None)
    def load(self) -> None
    def save(self) -> None
    def get(self, key: str, default: Any = None) -> Any
    def set(self, key: str, value: Any) -> None
```

## Examples

See the `examples/` directory for practical usage examples.

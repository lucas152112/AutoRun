# AutoRun

Windows內建的排程無法正常使用,重新開發一個可以使用的

A Windows Task Scheduler alternative that provides reliable task scheduling and automation.

## Features

- Simple and intuitive task scheduling
- Flexible scheduling options
- Easy configuration management
- Comprehensive logging
- Extensible architecture

## Installation

### From Source

```bash
git clone https://github.com/lucas152112/AutoRun.git
cd AutoRun
pip install -e .
```

### Development Installation

```bash
pip install -e ".[dev]"
```

## Quick Start

### Basic Usage

```python
from autorun.scheduler import Task, Scheduler

# Define a task function
def my_task():
    print("Task is running!")

# Create a scheduler
scheduler = Scheduler()

# Create and add a task
task = Task(name="my_task", func=my_task, schedule="daily")
scheduler.add_task(task)

# Start the scheduler
scheduler.start()
```

### Running from Command Line

```bash
autorun
```

## Configuration

AutoRun uses a JSON configuration file located at `config/config.json`. You can customize:

- `log_level`: Logging level (DEBUG, INFO, WARNING, ERROR)
- `log_file`: Path to log file
- `check_interval`: Interval in seconds to check for scheduled tasks
- `max_retries`: Maximum number of retries for failed tasks
- `retry_delay`: Delay in seconds between retries

Example configuration:

```json
{
    "log_level": "INFO",
    "log_file": "logs/autorun.log",
    "check_interval": 60,
    "max_retries": 3,
    "retry_delay": 300
}
```

## Project Structure

```
AutoRun/
├── autorun/           # Main package
│   ├── __init__.py    # Package initialization
│   ├── main.py        # Entry point
│   ├── scheduler.py   # Task scheduler
│   └── config.py      # Configuration management
├── tests/             # Unit tests
├── docs/              # Documentation
├── examples/          # Example scripts
├── config/            # Configuration files
└── README.md          # This file
```

## Development

### Running Tests

```bash
pytest
```

### Running Tests with Coverage

```bash
pytest --cov=autorun --cov-report=html
```

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

## License

MIT License

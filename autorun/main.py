"""
Main entry point for AutoRun application.
"""

import sys
import logging
from pathlib import Path

from autorun.config import Config
from autorun.scheduler import Scheduler

# Setup logging
logging.basicConfig(
    level=logging.INFO,
    format='%(asctime)s - %(name)s - %(levelname)s - %(message)s'
)

logger = logging.getLogger(__name__)


def main():
    """Main function to run the AutoRun scheduler."""
    logger.info("Starting AutoRun...")
    
    # Load configuration
    config = Config()
    
    # Configure logging based on config
    log_level = getattr(logging, config.get('log_level', 'INFO'))
    logging.getLogger().setLevel(log_level)
    
    # Create log directory if needed
    log_file = config.get('log_file')
    if log_file:
        log_path = Path(log_file)
        log_path.parent.mkdir(parents=True, exist_ok=True)
        
        file_handler = logging.FileHandler(log_file, encoding='utf-8')
        file_handler.setFormatter(
            logging.Formatter('%(asctime)s - %(name)s - %(levelname)s - %(message)s')
        )
        logging.getLogger().addHandler(file_handler)
    
    # Initialize scheduler
    scheduler = Scheduler()
    
    logger.info("AutoRun initialized successfully")
    logger.info("No tasks configured. Add tasks to get started.")
    
    return 0


if __name__ == "__main__":
    sys.exit(main())

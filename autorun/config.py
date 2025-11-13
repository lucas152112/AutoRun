"""
Configuration Module

This module handles configuration loading and management.
"""

import json
import logging
from pathlib import Path
from typing import Dict, Any, Optional

logger = logging.getLogger(__name__)


class Config:
    """Configuration manager for AutoRun."""
    
    DEFAULT_CONFIG = {
        "log_level": "INFO",
        "log_file": "logs/autorun.log",
        "check_interval": 60,
        "max_retries": 3,
        "retry_delay": 300
    }
    
    def __init__(self, config_path: Optional[str] = None):
        """
        Initialize configuration.
        
        Args:
            config_path: Path to configuration file (optional)
        """
        self.config_path = config_path or "config/config.json"
        self.config: Dict[str, Any] = self.DEFAULT_CONFIG.copy()
        self.load()
        
    def load(self) -> None:
        """Load configuration from file."""
        path = Path(self.config_path)
        if path.exists():
            try:
                with open(path, 'r', encoding='utf-8') as f:
                    user_config = json.load(f)
                    self.config.update(user_config)
                logger.info(f"Loaded configuration from {self.config_path}")
            except Exception as e:
                logger.error(f"Error loading configuration: {e}")
        else:
            logger.info("Using default configuration")
            
    def save(self) -> None:
        """Save configuration to file."""
        path = Path(self.config_path)
        path.parent.mkdir(parents=True, exist_ok=True)
        try:
            with open(path, 'w', encoding='utf-8') as f:
                json.dump(self.config, f, indent=4, ensure_ascii=False)
            logger.info(f"Saved configuration to {self.config_path}")
        except Exception as e:
            logger.error(f"Error saving configuration: {e}")
            
    def get(self, key: str, default: Any = None) -> Any:
        """
        Get a configuration value.
        
        Args:
            key: Configuration key
            default: Default value if key not found
            
        Returns:
            Configuration value
        """
        return self.config.get(key, default)
        
    def set(self, key: str, value: Any) -> None:
        """
        Set a configuration value.
        
        Args:
            key: Configuration key
            value: Configuration value
        """
        self.config[key] = value
        
    def __getitem__(self, key: str) -> Any:
        """Get configuration value using dict-like access."""
        return self.config[key]
        
    def __setitem__(self, key: str, value: Any) -> None:
        """Set configuration value using dict-like access."""
        self.config[key] = value

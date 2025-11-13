"""
Unit tests for the Config class.
"""

import pytest
import json
import tempfile
from pathlib import Path
from autorun.config import Config


class TestConfig:
    """Test cases for Config class."""
    
    def test_config_default(self):
        """Test configuration with defaults."""
        config = Config(config_path="nonexistent.json")
        assert config.get("log_level") == "INFO"
        assert config.get("check_interval") == 60
        
    def test_config_get_set(self):
        """Test getting and setting configuration values."""
        config = Config(config_path="nonexistent.json")
        
        config.set("custom_key", "custom_value")
        assert config.get("custom_key") == "custom_value"
        
        config["another_key"] = 123
        assert config["another_key"] == 123
        
    def test_config_save_load(self):
        """Test saving and loading configuration."""
        with tempfile.TemporaryDirectory() as tmpdir:
            config_path = Path(tmpdir) / "test_config.json"
            
            # Create and save config
            config1 = Config(config_path=str(config_path))
            config1.set("test_key", "test_value")
            config1.save()
            
            # Load config in new instance
            config2 = Config(config_path=str(config_path))
            assert config2.get("test_key") == "test_value"
            
    def test_config_default_values(self):
        """Test that default configuration values are set."""
        config = Config(config_path="nonexistent.json")
        
        assert "log_level" in config.config
        assert "log_file" in config.config
        assert "check_interval" in config.config
        assert "max_retries" in config.config
        assert "retry_delay" in config.config

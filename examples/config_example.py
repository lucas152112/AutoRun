"""
Example: Configuration Management

This example demonstrates how to work with configuration.
"""

from autorun.config import Config
import logging

logging.basicConfig(level=logging.INFO)


def main():
    """Main function to demonstrate configuration usage."""
    # Create config with default path
    config = Config()
    
    print("Default Configuration:")
    print(f"  Log Level: {config.get('log_level')}")
    print(f"  Log File: {config.get('log_file')}")
    print(f"  Check Interval: {config.get('check_interval')}")
    
    # Modify configuration
    config.set('custom_setting', 'custom_value')
    config['another_setting'] = 42
    
    print("\nModified Configuration:")
    print(f"  Custom Setting: {config.get('custom_setting')}")
    print(f"  Another Setting: {config['another_setting']}")
    
    # Save configuration
    config.save()
    print(f"\nConfiguration saved to: {config.config_path}")
    
    # Load configuration in a new instance
    config2 = Config()
    print("\nReloaded Configuration:")
    print(f"  Custom Setting: {config2.get('custom_setting')}")
    print(f"  Another Setting: {config2.get('another_setting')}")


if __name__ == "__main__":
    main()
